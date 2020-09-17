using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]   
    public class InventoryOutDetailController : AppController
    {   
        public InventoryOutDetailController(AppDbContext dbContext, ILogger<InventoryOutDetailController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<InventoryOutTrxDetail> Index()
        {
            var inventOutDetail = dbContext.InventoryOutTrxDetails.ToList();
            return inventOutDetail;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] InventoryOutTrxDetail inventoryOutDetail)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    dbContext.InventoryOutTrxDetails.Add(inventoryOutDetail);
                    dbContext.SaveChanges();
                    transaction.Commit();
                    return Json(inventoryOutDetail);
            }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
            }
        }
        [Route("[action]")]
        public IActionResult UpdateINVInRemainingCount(string trxNum, string minCount)
        {  
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                try
                {
                    InventoryInTrxDetail invd = dbContext.InventoryInTrxDetails.Find(trxNum);

                    if (invd != null)
                    {
                        invd.RemainigCount = int.Parse(minCount);
                        dbContext.InventoryInTrxDetails.Update(invd);
                        dbContext.SaveChanges();
                        transaction.Commit();
                        return Json(trxNum);

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
            }
        
                 
        [Route("[action]")]
        public IActionResult findTrxNum(string itemID, string unit)
        {
            try
            {
                var lotNumber = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID).Where(data => data.Unit == unit);
                if (lotNumber != null)
                {
                    return Ok(lotNumber);
                }
                else
                {
                    return Json(lotNumber);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findRemainingCount(string trxID, string itemID,string lotNum)
        {
            try
            {
                var remainingCount = dbContext.InventoryInTrxDetails
                    .Where(data => data.TransactionNo == trxID)
                    .Where(data => data.ItemID == itemID)
                    .Where(data => data.LotNumber == lotNum);
                if (remainingCount != null)
                {
                    return Ok(remainingCount);
                }
                else
                {
                    return Ok(remainingCount);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult DeleteAll(string trxNum)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    IEnumerable<InventoryOutTrxDetail> invDeleteAll = dbContext.InventoryOutTrxDetails.Where(data => data.TransactionNo == trxNum).ToList();


                    if (invDeleteAll != null)
                    {
                        dbContext.InventoryOutTrxDetails.RemoveRange(invDeleteAll);
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
        public IActionResult JoinINVtoItemMaster()
        {
            try
            {
                var Join = (from invInDetail in dbContext.InventoryInTrxDetails
                           join ItemMaster in dbContext.ItemMasters on invInDetail.ItemID equals ItemMaster.ID
                           select new
                           {
                               itemID = invInDetail.ItemID,
                               itemName = ItemMaster.ItemName,
                           }).Distinct();
                if (Join != null)
                {
                    return Json(Join.ToList()); ;
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
        public IActionResult getAllTrx(string trxNum)
        {
            try
            {
                var JoinInvtoITMU = (from invOutDetail in dbContext.InventoryOutTrxDetails
                                     join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                     join itemUnits in dbContext.UnitCodes on invOutDetail.Unit equals itemUnits.Code
                                     join invInTrxDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInTrxDetail.TransactionNo
                                     join invOutHeader in dbContext.InventoryOutTrxHeaders on invOutDetail.TransactionNo equals invOutHeader.TransactionNo
                                     join depList in dbContext.Departments on invOutDetail.Remarks equals depList.ID
                                     join itemMasterUnits in dbContext.itemMasterUnits on new { key1 = invOutDetail.ItemID, key2 = invOutDetail.Unit } equals new { key1 = itemMasterUnits.ID, key2 = itemMasterUnits.itemMasterUnitUnit }
                                     where invOutDetail.TransactionNo == trxNum
                                     select new
                                     {
                                         transactionNo = invOutDetail.TransactionNo,
                                         itemID = invOutDetail.ItemID,
                                         ItemName = itemMaster.ItemName,
                                         unit = invOutDetail.Unit,
                                         description = itemUnits.Description,
                                         in_TrxNo = invOutDetail.In_TrxNo,
                                         lotNumber = invInTrxDetail.LotNumber,
                                         quantity = invOutDetail.Quantity,
                                         remarks = depList.DepartmentName,
                                         remarksID = invOutDetail.Remarks,
                                         minCount = invOutDetail.MinCount,
                                         expirationDate = invInTrxDetail.ExpirationDate,
                                         remainigCount = invInTrxDetail.RemainigCount,
                                         transactionDate = invOutHeader.TransactionDate,
                                         itemMasterUnitConversion = itemMasterUnits.itemMasterUnitConversion,
                                         department = invOutHeader.Department,
                                         referenceNo = invOutHeader.ReferenceNo,
                                         headremarks = invOutHeader.Remarks

                                     }).ToList() ;
                if (JoinInvtoITMU.Count != 0)
                {
                    return Json(JoinInvtoITMU); ;
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
    }
   
}
