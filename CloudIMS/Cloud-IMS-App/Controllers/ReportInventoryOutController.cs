using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]
    public class ReportInventoryOutController : AppController
    {
        public ReportInventoryOutController(AppDbContext dbContext, ILogger<ReportInventoryOutController> logger)
            : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<ItemGroup> Index()
        {
            var itemgroup = dbContext.ItemGroups.ToList();
            return itemgroup;
        }

        [Route("[action]")]
        public IActionResult FindReport(string trxNum, string itemName, string itemUnit, string department,
            DateTime dateFrom, DateTime dateTo, string reportType)
        {

            try
            {
                var dateFromC = dateFrom.ToString("yyyy-MM-dd 00:00:00");
                var dateToC = dateTo.ToString("yyyy-MM-dd 23:59:59");
                if (reportType == "TransactionDate")
                {

                    var rptTrxDate = (from invOutHeader in dbContext.InventoryOutTrxHeaders
                                      join departments in dbContext.Departments on invOutHeader.Department equals departments.ID
                                      join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                      join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                      join itemMasterUnit in dbContext.itemMasterUnits on invOutDetail.Unit equals itemMasterUnit.itemMasterUnitUnit
                                      join unitCode in dbContext.UnitCodes on invOutDetail.Unit equals unitCode.Code
                                      join invInDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInDetail.TransactionNo
                                      where (invOutHeader.TransactionDate >= DateTime.Parse(dateFromC) && invOutHeader.TransactionDate <= DateTime.Parse(dateToC))
                                      select new
                                      {
                                          headerTransactionNo = invOutHeader.TransactionNo,
                                          transactionDate = invOutHeader.TransactionDate,
                                          issuedBy = invOutHeader.IssuedBy,
                                          issuedDate = invOutHeader.IssuedDate,
                                          receivedBy = invOutHeader.ReceivedBy,
                                          department = invOutHeader.Department,
                                          departmentName = departments.DepartmentName,
                                          referenceNo = invOutHeader.ReferenceNo,
                                          headerRemarks = invOutHeader.Remarks,
                                          itemName = itemMaster.ItemName,
                                          itemID = itemMaster.ID,
                                          unit = invOutDetail.Unit,
                                          itemMasterUnitUnit = itemMasterUnit.itemMasterUnitUnit,
                                          description = unitCode.Description,
                                          lotNumber = invInDetail.LotNumber,
                                          quantity = invOutDetail.Quantity,
                                          detailRemarks = invOutDetail.Remarks
                                      }).Distinct();
                    if (rptTrxDate != null)
                    {
                        if (trxNum != null)
                        {
                            rptTrxDate = rptTrxDate.Where(invOutTrx => invOutTrx.headerTransactionNo.Contains(trxNum));
                        }
                        if (itemName != "%")
                        {
                            rptTrxDate = rptTrxDate.Where(invOutName => invOutName.itemID.Contains(itemName));
                        }
                        if (itemUnit != "%")
                        {
                            rptTrxDate = rptTrxDate.Where(invOutUnit => invOutUnit.unit.Contains(itemUnit));
                        }
                        if (department != "%")
                        {
                            rptTrxDate = rptTrxDate.Where(invOutUnit => invOutUnit.department.Contains(department));
                        }
                        return Json(rptTrxDate.ToList());
                    }
                }
                else if (reportType == "IssuedDate")
                {
                    var rptIsseudDate = (from invOutHeader in dbContext.InventoryOutTrxHeaders
                                         join departments in dbContext.Departments on invOutHeader.Department equals departments.ID
                                         join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                         join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                         join itemMasterUnit in dbContext.itemMasterUnits on invOutDetail.Unit equals itemMasterUnit.itemMasterUnitUnit
                                         join unitCode in dbContext.UnitCodes on invOutDetail.Unit equals unitCode.Code
                                         join invInDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInDetail.TransactionNo
                                         where (invOutHeader.IssuedDate >= DateTime.Parse(dateFromC) && invOutHeader.IssuedDate <= DateTime.Parse(dateToC))
                                         select new
                                         {
                                             headerTransactionNo = invOutHeader.TransactionNo,
                                             transactionDate = invOutHeader.TransactionDate,
                                             issueDate = invOutHeader.IssuedDate,
                                             issuedBy = invOutHeader.IssuedBy,
                                             receivedBy = invOutHeader.ReceivedBy,
                                             department = invOutHeader.Department,
                                             departmentName = departments.DepartmentName,
                                             referenceNo = invOutHeader.ReferenceNo,
                                             headerRemarks = invOutHeader.Remarks,
                                             itemName = itemMaster.ItemName,
                                             itemID = itemMaster.ID,
                                             unit = invOutDetail.Unit,
                                             itemMasterUnitUnit = itemMasterUnit.itemMasterUnitUnit,
                                             description = unitCode.Description,
                                             lotNumber = invInDetail.LotNumber,
                                             quantity = invOutDetail.Quantity,
                                             detailRemarks = invOutDetail.Remarks
                                         }).Distinct();

                    if (rptIsseudDate != null)
                    {
                        if (trxNum != null)
                        {
                            rptIsseudDate = rptIsseudDate.Where(invOutTrx => invOutTrx.headerTransactionNo.Contains(trxNum));
                        }
                        if (itemName != "%")
                        {
                            rptIsseudDate = rptIsseudDate.Where(invOutName => invOutName.itemID.Contains(itemName));
                        }
                        if (itemUnit != "%")
                        {
                            rptIsseudDate = rptIsseudDate.Where(invOutUnit => invOutUnit.unit.Contains(itemUnit));
                        }
                        if (department != "%")
                        {
                            rptIsseudDate = rptIsseudDate.Where(invOutUnit => invOutUnit.department.Contains(department));
                        }
                        return Json(rptIsseudDate.ToList());
                    }
                    else
                    {
                        throw new Exception($"No Data Found.");
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
 
        [Route("[action]")]
        public IActionResult GenerateTallyReport()
        {

            try
            {
                var getTallyReports = (from invInDtl in dbContext.InventoryInTrxDetails
                                       join invInHdr in dbContext.InventoryInTrxHeaders on invInDtl.TransactionNo equals invInHdr.TransactionNo into intoInvInHdr
                                       from fromInvInHdr in intoInvInHdr.DefaultIfEmpty()
                                       join itemMaster in dbContext.ItemMasters on invInDtl.ItemID equals itemMaster.ID into intoItemMaster
                                       from fromItemMaster in intoItemMaster.DefaultIfEmpty()
                                       join invOutDtl in dbContext.InventoryOutTrxDetails on invInDtl.TransactionNo equals invOutDtl.In_TrxNo into intoInvOutDtl
                                       from fromInvOutDtl in intoInvOutDtl.DefaultIfEmpty()
                                       join supp in dbContext.Suppliers on fromInvInHdr.Supplier equals supp.ID into intoSupp
                                       from fromSupp in intoSupp.DefaultIfEmpty()
                                       join userAccout in dbContext.UserAccounts on fromInvInHdr.ReceivedBy equals userAccout.UserID into intoUserAccounts
                                       from fromUserAccounts in intoUserAccounts.DefaultIfEmpty()
                                       join dep in dbContext.Departments on fromInvOutDtl.Remarks equals dep.ID into intoDepartment
                                       from fromDepartment in intoDepartment.DefaultIfEmpty()
                                       join itemUnit in dbContext.itemMasterUnits on fromItemMaster.Unit equals itemUnit.itemMasterUnitUnit into intoItemUnit
                                       from fromItemUnit in intoItemUnit.DefaultIfEmpty()
                                       join unitCode in dbContext.UnitCodes on fromItemUnit.itemMasterUnitUnit equals unitCode.Code into intoUnitCode
                                       from fromUnitCode in intoUnitCode.DefaultIfEmpty()
                                       join defect in dbContext.DefectedItemsModel on new { key1 = invInDtl.TransactionNo, key2 = invInDtl.LotNumber } equals new { key1 = defect.TransactionNo, key2 = defect.LotNumber }
                                       into intoItemDefect
                                       from fromDefect in intoItemDefect.DefaultIfEmpty()
                                        group new { invInDtl, fromInvInHdr, fromItemMaster, fromInvOutDtl, fromSupp , fromUserAccounts , fromDepartment , fromItemUnit , fromUnitCode , fromDefect } by new
                                        {
                                            ItemID = invInDtl.ItemID != "" ? invInDtl.ItemID: "",
                                            ItemName = fromItemMaster.ItemName != "" ? fromItemMaster.ItemName: "",
                                            SupplierName = fromSupp.Name != "" ? fromSupp.Name : "",
                                            DateInventoryIn = fromInvInHdr.TransactionDate,
                                            InvoiceNumber = fromInvInHdr.InvoiceNo != "" ? fromInvInHdr.InvoiceNo: "",
                                            PONumber = fromInvInHdr.PONumber != "" ? fromInvInHdr.PONumber: "",
                                            LotNumber = invInDtl.LotNumber != "" ? invInDtl.LotNumber: "",
                                            InTransactionNumber = fromInvOutDtl.In_TrxNo != "" ? fromInvOutDtl.In_TrxNo: "",
                                            RecievedBy = fromUserAccounts.UserName != "" ? fromUserAccounts.UserName: "",
                                            Department= fromDepartment.DepartmentName != "" ? fromDepartment.DepartmentName: "",
                                            ItemUnit = fromUnitCode.Description != "" ? fromUnitCode.Description: "",
                                            unitID = fromUnitCode.Code != "" ? fromUnitCode.Code: ""
                                        } into tallyGroup
                                       select new
                                       {

                                           ItemID = tallyGroup.Key.ItemID != "" ? tallyGroup.Key.ItemID : "",
                                           ItemName = tallyGroup.Key.ItemName != "" ? tallyGroup.Key.ItemName : "",
                                           SupplierName = tallyGroup.Key.SupplierName != "" ? tallyGroup.Key.SupplierName : "",
                                           DateInventoryIn = tallyGroup.Key.DateInventoryIn,
                                           InvoiceNumber = tallyGroup.Key.InvoiceNumber != "" ? tallyGroup.Key.InvoiceNumber : "",
                                           PONumber = tallyGroup.Key.PONumber != "" ? tallyGroup.Key.PONumber : "",
                                           LotNumber = tallyGroup.Key.LotNumber != "" ? tallyGroup.Key.LotNumber : "",
                                           InTransactionNumber = tallyGroup.Key.InTransactionNumber != "" ? tallyGroup.Key.InTransactionNumber : "",
                                           RecievedBy = tallyGroup.Key.RecievedBy != "" ? tallyGroup.Key.RecievedBy : "",
                                           Department = tallyGroup.Key.Department != "" ? tallyGroup.Key.Department : "",
                                           ItemUnit = tallyGroup.Key.ItemUnit != "" ? tallyGroup.Key.ItemUnit : "",
                                           unitID = tallyGroup.Key.unitID != "" ? tallyGroup.Key.unitID: "",
                                          
                                       }).Distinct().OrderBy(tally => tally.ItemID);

                return Json(getTallyReports.ToList());
            }
          
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult GetItemTotalCount(string itemID, string lotNum)
        {
            try
            {
                var itemTotalCount = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID)
                                                                    .Where(data => data.LotNumber == lotNum)
                                                                    .Sum(data => data.RemainigCount);


                if (itemTotalCount > 0)
                {
                    return Ok(itemTotalCount);
                }
                else
                {

                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult GetItemInventoryIn(string itemID, string lotNum)
        {
            try
            {
                var itemTotalCount = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID)
                                                                    .Where(data => data.LotNumber == lotNum)
                                                                    .Sum(data => data.Count);


                if (itemTotalCount > 0)
                {
                    return Ok(itemTotalCount);
                }
                else
                {

                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult GetItemInventoryOut(string itemID, string InTrxNo)
        {
            try
            {
                var itemTotalCount = dbContext.InventoryOutTrxDetails.Where(data => data.ItemID == itemID)
                                                                    .Where(data => data.In_TrxNo == InTrxNo)
                                                                    .Sum(data => data.Quantity);


                if (itemTotalCount > 0)
                {
                    return Ok(itemTotalCount);
                }
                else
                {

                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult GetItemDefect(string itemID, string lotNum)
        {
            try
            {
                var itemTotalCount = dbContext.DefectedItemsModel.Where(data => data.ItemID == itemID)
                                                                    .Where(data => data.LotNumber == lotNum)
                                                                    .Sum(data => data.Quantity);


                if (itemTotalCount > 0)
                {
                    return Ok(itemTotalCount);
                }
                else
                {

                    return Ok(0);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
}
