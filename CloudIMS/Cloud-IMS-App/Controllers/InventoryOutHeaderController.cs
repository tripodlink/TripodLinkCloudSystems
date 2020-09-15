using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
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
                    var ivnList = new InventoryOutTrxHeader();

                    ivnList.TransactionNo = inventoryOut.TransactionNo;
                    ivnList.TransactionDate = inventoryOut.TransactionDate;
                    ivnList.IssuedBy = "";
                    var dateFromC = "01/01/0001 00:00:00";
                    ivnList.IssuedDate = DateTime.Parse(dateFromC);
                    ivnList.ReceivedBy = inventoryOut.ReceivedBy;
                    ivnList.Department = inventoryOut.Department;
                    ivnList.ReferenceNo = inventoryOut.ReferenceNo;
                    ivnList.Remarks = inventoryOut.Remarks;
                    ivnList.Status = inventoryOut.Status;
                    
                    dbContext.InventoryOutTrxHeaders.Add(ivnList);
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
        public IActionResult getPendingTrx()
        {
            try
            {
                var result = (from invOutHeaders in dbContext.InventoryOutTrxHeaders
                              join depList in dbContext.Departments on invOutHeaders.Department equals depList.ID
                              where invOutHeaders.Status == "P"
                                     select new
                                     {
                                         transactionNo = invOutHeaders.TransactionNo,
                                         transactionDate = invOutHeaders.TransactionDate,
                                         issuedDate = invOutHeaders.IssuedDate,
                                         department = depList.DepartmentName,
                                         referenceNo = invOutHeaders.ReferenceNo
                                     });
                if (result != null)
                {
                    return Json(result.ToList()); ;
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
        public IActionResult getIssuedTrx()
        {
            try
            {
                var result = (from invOutHeaders in dbContext.InventoryOutTrxHeaders
                              join depList in dbContext.Departments on invOutHeaders.Department equals depList.ID
                              where invOutHeaders.Status == "I"
                              select new
                              {
                                  transactionNo = invOutHeaders.TransactionNo,
                                  transactionDate = invOutHeaders.TransactionDate,
                                  issuedDate = invOutHeaders.IssuedDate,
                                  department = depList.DepartmentName,
                                  referenceNo = invOutHeaders.ReferenceNo
                              });
                if (result != null)
                {
                    return Json(result.ToList()); ;
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
        public IActionResult findTrxNum(string trxNum, string lotNum)
        {
            try
            {
                var trxNumList = dbContext.InventoryOutTrxHeaders.Where(data => data.TransactionNo == trxNum).ToList();
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
        public Boolean GetIfApprover(string userID)
        {
            var GetApprover = (from userAccount in dbContext.UserAccounts
                               join userAccountGroup in dbContext.UserAccountGroups on userAccount.UserID
                               equals userAccountGroup.UserAccountID
                               join userGroup in dbContext.UserGroups on userAccountGroup.UserGroupID
                               equals userGroup.ID
                               where userAccount.UserID == userID
                               select new
                               {
                                   isApprover = userGroup.IsApprover
                               }
                               ).ToList();


            var result = true;

            GetApprover.ForEach((arr) =>
            {
                if (arr.isApprover)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            });
            return result;

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
                        findTrxNum.TransactionNo = invOutTrx.TransactionNo;
                        findTrxNum.IssuedBy = invOutTrx.IssuedBy;
                        findTrxNum.IssuedDate = invOutTrx.IssuedDate;
                        findTrxNum.Department = invOutTrx.Department;
                        findTrxNum.ReferenceNo = invOutTrx.ReferenceNo;
                        findTrxNum.Remarks = invOutTrx.Remarks;
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
