using System;
using System.Collections.Generic;
using System.Linq;
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
                                       join invInHdr in dbContext.InventoryInTrxHeaders on invInDtl.TransactionNo equals invInHdr.TransactionNo
                                       join itemMaster in dbContext.ItemMasters on invInDtl.ItemID equals itemMaster.ID
                                       join invOutDtl in dbContext.InventoryOutTrxDetails on invInDtl.TransactionNo equals invOutDtl.In_TrxNo
                                       join supp in dbContext.Suppliers on invInHdr.Supplier equals supp.ID
                                       join dep in dbContext.Departments on invOutDtl.Remarks equals dep.ID
                                       join itemUnit in dbContext.itemMasterUnits on invInDtl.ItemID equals itemUnit.ID 
                                       join unitCode in dbContext.UnitCodes on itemUnit.itemMasterUnitUnit equals unitCode.Code
                                       join defect in dbContext.DefectedItemsModel on new { key1 = invInDtl.TransactionNo, key2 = invInDtl.LotNumber } equals new { key1 = defect.TransactionNo, key2 = defect.LotNumber }
                                       select new
                                       {
                                           ItemID = invInDtl.ItemID,
                                           ItemName = itemMaster.ItemName,
                                           SupplierName = supp.Name,
                                           DateInventoryIn = invInHdr.TransactionDate,
                                           InvoiceNumber = invInHdr.InvoiceNo,
                                           PONumber = invInHdr.PONumber,
                                           LotNumber = invInDtl.LotNumber,
                                           RecievedBy = invInHdr.ReceivedBy,
                                           Department = dep.DepartmentName,
                                           ItemUnit = unitCode.Description,
                                           ItemRemainingCount = invInDtl.RemainigCount,
                                           InventoryIn = invInDtl.Quantity,
                                           InventoryOut = invOutDtl.Quantity,
                                           ItemDefect = defect.Quantity
                                       }).ToList();

                return Json(getTallyReports);
            }
          
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
}
