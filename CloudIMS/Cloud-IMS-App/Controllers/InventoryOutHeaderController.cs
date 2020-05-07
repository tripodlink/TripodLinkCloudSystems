using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]
    public class InventoryOutHeaderController : AppController
    {
        public InventoryOutHeaderController(AppDbContext dbContext, ILogger<InventoryOutHeaderController> logger)
            : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<InventoryOutTrxHeader> Index()
        {
            var invenOut = dbContext.InventoryOutTrxHeaders.ToList();
            return invenOut;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] InventoryOutTrxHeader inventoryOut)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.InventoryOutTrxHeaders.Add(inventoryOut);
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return Ok(inventoryOut);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
            }
        }

        [Route("[action]")]
        public IActionResult findLotNum(string itemID, string unit)
        {
            try
            {
                var JoinInvtoITMU = (from invInDetail in dbContext.InventoryInTrxDetails
                                     join ItemMasterUnit in dbContext.itemMasterUnits on invInDetail.ItemID equals ItemMasterUnit.ID
                                     where invInDetail.ItemID == itemID && ItemMasterUnit.itemMasterUnitUnit == unit
                                     && invInDetail.RemainigCount != 0
                                     select new
                                     {
                                         itemID = invInDetail.ItemID,
                                         lotNumber = invInDetail.LotNumber,
                                         itemMasterUnitUnit = ItemMasterUnit.itemMasterUnitUnit,
                                         transactionNo = invInDetail.TransactionNo
                                     });
                if (JoinInvtoITMU != null)
                {
                    return Json(JoinInvtoITMU.ToList()); ;
                }
                else
                {

                    throw new Exception($"No Data Found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findTrxNum(string trxNUm)
        {
            try
            {
                var trxNumList = dbContext.InventoryOutTrxHeaders.Where(data => data.TransactionNo == trxNUm).ToList();
                if (trxNumList != null)
                {
                    return Ok(trxNumList);
                }
                else
                {
                    return Ok(trxNumList);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findPendingTrx()
        {
            try
            {
                var trxNumList = dbContext.InventoryOutTrxHeaders.Where(data => data.Status == "P").ToList();
                if (trxNumList != null)
                {
                    return Json(trxNumList);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public IActionResult DeleteAllTrx(string trxNum)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    InventoryOutTrxHeader allTrxHeader = dbContext.InventoryOutTrxHeaders.Find(trxNum);

                    if (allTrxHeader != null)
                    {
                        dbContext.InventoryOutTrxHeaders.Remove(allTrxHeader);
                        dbContext.SaveChanges();
                        transaction.Commit();
                        return Json(trxNum);
                    }
                    else
                    {
                        throw new Exception($"User Not found with a user ID of '{trxNum}'.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult UpdatePendingTrx([FromBody] InventoryOutTrxHeader invOutTrx)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    InventoryOutTrxHeader findTrxNum = dbContext.InventoryOutTrxHeaders.Find(invOutTrx.TransactionNo);

                    if (findTrxNum != null)
                    {

                        findTrxNum.Status = invOutTrx.Status;
                        dbContext.InventoryOutTrxHeaders.Update(findTrxNum);
                        dbContext.SaveChanges();
                        transaction.Commit();
                        return Json(findTrxNum);
                    }
                    else
                    {
                        throw new Exception($"User Not found with a user ID of '{invOutTrx.TransactionNo}'.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
            }
        }
        [Route("[action]")]
        public IActionResult getTrxNumFunction()
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var mcon = dbContext.Database.GetDbConnection().CreateCommand();
                    mcon.CommandText = "select setAuto_number('INVOUT')";
                    var returnVal = mcon.ExecuteScalar();
                    transaction.Commit();
                
                    return Json(returnVal);
                    
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
            }

        }
    }
}
