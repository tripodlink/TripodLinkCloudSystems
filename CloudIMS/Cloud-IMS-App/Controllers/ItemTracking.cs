using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.ResultOperators.Internal;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]   
    public class ItemTrackingController : AppController
    {   
        public ItemTrackingController(AppDbContext dbContext, ILogger<ItemGroupController>logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<ItemTracking> Index()
        {
            var itemTracking = dbContext.itemTrackings.ToList();
            return itemTracking;
        }

        [Route("[action]")]
        public IActionResult GetItemData(string ItemID, string lotNum)
        {
            try
            {
                var trxNumList = (from trxNum in dbContext.itemTrackings
                                  join depList in dbContext.Departments on trxNum.Location equals depList.ID
                                  where trxNum.ItemID == ItemID && trxNum.LotNo == lotNum
                                  select new
                                  {
                                      transactionNo = trxNum.TransactionNo,
                                      itemID = trxNum.ItemID,
                                      lotNo = trxNum.LotNo,
                                      dateUpdated = trxNum.DateUpdated,
                                      locationID = trxNum.Location,   
                                      location =depList.DepartmentName

                                  }).OrderByDescending(x => x.dateUpdated);
                    
                    //dbContext.itemTrackings.Where(data => data.ItemID == ItemID)
                    //                                    .Where(data => data.LotNo == lotNum).ToList();
                if (trxNumList != null)
                {
                    return Json(trxNumList.ToList());
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
        public IActionResult GetCurrentLocation(string ItemID, string lotNum)
        {
            try
            {
                var trxNumList = (from depList in dbContext.Departments
                                  join itmTrcking in dbContext.itemTrackings on depList.ID equals itmTrcking.Location
                                  where itmTrcking.ItemID == ItemID && itmTrcking.LotNo == lotNum 
                                  select new
                                  {
                                      dateUpdated = itmTrcking.DateUpdated,
                                      departmentDescription = depList.DepartmentName
                                  }

                                   ).OrderByDescending(x => x.dateUpdated).First();
                    
                    //dbContext.itemTrackings.Where(data => data.ItemID == ItemID)
                    //                                    .Where(data => data.LotNo == lotNum)
                    //                                    .Select(x => new { x.Location, x.DateUpdated}).OrderByDescending(x => x.DateUpdated)
                    //                                    .First()
                    //                                    ;
                if (trxNumList != null)
                {
                    return Ok(trxNumList);
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
        [HttpPost]
        public IActionResult UpdateLocation([FromBody] ItemTracking itemTracking)
        {
            var ItemTrack = new ItemTracking();
            try
            {
                dbContext.itemTrackings.Add(itemTracking);
                dbContext.SaveChanges();
                return Ok(ItemTrack);
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
                    ItemTracking Itracking = dbContext.itemTrackings.Find(trxNum);

                    if (Itracking != null)
                    {
                        dbContext.itemTrackings.Remove(Itracking);
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
        public IActionResult findLotNum(string itemID)
        {
            try
            {
                var JoinInvtoITMU = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID)
                    .Select(data => new { data.LotNumber, data.ItemID }).Distinct();
                                                       
                                    
                if (JoinInvtoITMU != null)
                {
                    return Json(JoinInvtoITMU.ToList());
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
        public IActionResult getItemSumCount(string itemID, string LotNum)
        {
            try
            {
                var JoinInvtoITMU = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID)
                                                                    .Where(data => data.LotNumber == LotNum)
                                                                    .Select(data => data.RemainigCount).Sum();


                if (JoinInvtoITMU != 0)
                {
                    return Ok(JoinInvtoITMU);
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
        public IActionResult getItemUnit(string itemID, string LotNum)
        {
            try
            {
                var JoinInvtoITMU = (from invIn in dbContext.InventoryInTrxDetails
                                     join unitCodes in dbContext.UnitCodes on invIn.Unit equals unitCodes.Code
                                     where invIn.ItemID == itemID && invIn.LotNumber == LotNum
                                    
                                     select new
                                     {
                                        itemUnit = unitCodes.Description
                                     }).First();


                if (JoinInvtoITMU != null)
                {
                    return Ok(JoinInvtoITMU);
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
        }  [Route("[action]")]
        public IActionResult getItemMinimumLimit(string itemID)
        {
            try
            {
                var MinimumLimit = dbContext.ItemMasters.Where(itmMaster => itmMaster.ID == itemID)
                                                         .Select(itmMaster => itmMaster.MinimumStockLevel);


                if (MinimumLimit != null)
                {
                    return Ok(MinimumLimit);
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
        public IActionResult getTrxNumFunction()
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var mcon = dbContext.Database.GetDbConnection().CreateCommand();
                    mcon.CommandText = "select setAuto_number('INVIT')";
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

        [Route("[action]")]
        public IActionResult GenerateAuditTrail(string itemID, string lotNum)
        {

            try
            {
                var getAuditTrail = (from invInDtl in dbContext.InventoryInTrxDetails
                                     join invInHdr in dbContext.InventoryInTrxHeaders on invInDtl.TransactionNo equals invInHdr.TransactionNo into intoInvInHdr
                                     from fromInvInHdr in intoInvInHdr.DefaultIfEmpty()
                                     join itemMaster in dbContext.ItemMasters on invInDtl.ItemID equals itemMaster.ID into intoItemMaster
                                     from fromItemMaster in intoItemMaster.DefaultIfEmpty()
                                     join invOutDtl in dbContext.InventoryOutTrxDetails on invInDtl.TransactionNo equals invOutDtl.In_TrxNo into intoInvOutDtl
                                     from fromInvOutDtl in intoInvOutDtl.DefaultIfEmpty()
                                     join invOutHdr in dbContext.InventoryOutTrxHeaders on fromInvOutDtl.TransactionNo equals invOutHdr.TransactionNo into intoInvOutHdr
                                     from fromInvOutHdr in intoInvOutHdr.DefaultIfEmpty()
                                     join supp in dbContext.Suppliers on fromInvInHdr.Supplier equals supp.ID into intoSupp
                                     from fromSupp in intoSupp.DefaultIfEmpty()
                                     join userAccout in dbContext.UserAccounts on fromInvInHdr.ReceivedBy equals userAccout.UserID into intoUserAccounts
                                     from fromUserAccounts in intoUserAccounts.DefaultIfEmpty()
                                     join dep in dbContext.Departments on fromInvOutDtl.Remarks equals dep.ID into intoDepartment
                                     from fromDepartment in intoDepartment.DefaultIfEmpty()
                                     join itemUnit in dbContext.itemMasterUnits on fromItemMaster.ID equals itemUnit.ID into intoItemUnit
                                     from fromItemUnit in intoItemUnit.DefaultIfEmpty()
                                     join unitCode in dbContext.UnitCodes on fromItemUnit.ID equals unitCode.Code into intoUnitCode
                                     from fromUnitCode in intoUnitCode.DefaultIfEmpty()
                                     join defect in dbContext.DefectedItemsModel on new { key1 = invInDtl.TransactionNo, key2 = invInDtl.LotNumber } equals new { key1 = defect.TransactionNo, key2 = defect.LotNumber }
                                     into intoItemDefect
                                     from fromDefect in intoItemDefect.DefaultIfEmpty()
                                     where invInDtl.ItemID == itemID && invInDtl.LotNumber == lotNum
                                     select new
                                     {
                                         InTrasactionNo = invInDtl.TransactionNo != "" ? invInDtl.TransactionNo : "",
                                         ItemID = invInDtl.ItemID != "" ? invInDtl.ItemID : "",
                                         ItemName = fromItemMaster.ItemName != "" ? fromItemMaster.ItemName : "",
                                         SupplierName = fromSupp.Name != "" ? fromSupp.Name : "",
                                         DateInventoryIn = fromInvInHdr.TransactionDate,
                                         InvoiceNumber = fromInvInHdr.InvoiceNo != "" ? fromInvInHdr.InvoiceNo : "",
                                         PONumber = fromInvInHdr.PONumber != "" ? fromInvInHdr.PONumber : "",
                                         LotNumber = invInDtl.LotNumber != "" ? invInDtl.LotNumber : "",
                                         ReceivedBy = fromUserAccounts.UserName != "" ? fromUserAccounts.UserName : "",
                                         ItemRemainingCount = invInDtl.RemainigCount > 0 ? invInDtl.RemainigCount : 0,
                                         InventoryIn = invInDtl.Count > 0 ? invInDtl.Count : 0,
                                         OutTransactionNo = fromInvOutHdr.TransactionNo != "" ? fromInvOutHdr.TransactionNo : "",
                                         DateInventoryOut = fromInvOutHdr.TransactionDate != null ? fromInvOutHdr.TransactionDate : (DateTime?)null,
                                         Department = fromDepartment.DepartmentName != "" ? fromDepartment.DepartmentName : "",
                                         RequestedBy = fromInvOutHdr.ReceivedBy != "" ? fromInvOutHdr.ReceivedBy : "",
                                         IssuedBy = fromInvOutHdr.IssuedBy != "" ? fromInvOutHdr.IssuedBy : "",
                                         ItemUnit = fromUnitCode.Description != "" ? fromUnitCode.Description : "",
                                         InventoryOut = fromInvOutDtl.Quantity > 0 ? fromInvOutDtl.Quantity : 0,
                                         ItemDefect = fromDefect.Quantity > 0 ? fromDefect.Quantity : 0
                                     }).OrderBy(tally => tally.DateInventoryIn);

                return Json(getAuditTrail.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
   
}
