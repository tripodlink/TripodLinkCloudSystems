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
                var dateFromC = dateFrom.ToString("yyyy-MM-dd");
                var dateToC = dateTo.ToString("yyyy-MM-dd");
                if (reportType == "TransactionDate")
                {
               
                var rptTrxDate = (from invOutHeader in dbContext.InventoryOutTrxHeaders
                                                 join departments in dbContext.Departments on invOutHeader.Department equals departments.ID
                                                 join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                                 join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                                 join itemMasterUnit in dbContext.itemMasterUnits on invOutDetail.Unit equals itemMasterUnit.itemMasterUnitUnit
                                                 join unitCode in dbContext.UnitCodes on invOutDetail.Unit equals unitCode.Code
                                                 join invInDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInDetail.TransactionNo
                                                 where invOutHeader.TransactionDate >= DateTime.Parse(dateFromC) && invOutHeader.TransactionDate <= DateTime.Parse(dateToC)
                                                  select new {
                                                     HeaderTransactionNo = invOutHeader.TransactionNo,
                                                     transactionDate = invOutHeader.TransactionDate,
                                                     issuedBy = invOutHeader.IssuedBy,
                                                     receivedBy = invOutHeader.ReceivedBy,
                                                     department = invOutHeader.Department,
                                                     departmentName = departments.DepartmentName,
                                                     referenceNo = invOutHeader.ReferenceNo,
                                                     HeaderRemarks = invOutHeader.Remarks,
                                                     itemID = invOutDetail.ItemID,
                                                     itemName = itemMaster.ItemName,
                                                     unit = invOutDetail.Unit,
                                                     itemMasterUnitUnit = itemMasterUnit.itemMasterUnitUnit,
                                                     description = unitCode.Description,
                                                     in_TrxNo = invOutDetail.In_TrxNo,
                                                     lotNumber = invInDetail.LotNumber,
                                                     quantity = invOutDetail.Quantity,
                                                     DetailRemarks = invOutDetail.Remarks}).Distinct();
                    if (rptTrxDate != null)
                    {
                        if (trxNum != null)
                        {
                            rptTrxDate = rptTrxDate.Where(invOutTrx => invOutTrx.HeaderTransactionNo.Contains(trxNum));
                        }
                        if (itemName != "%")
                        {
                            rptTrxDate = rptTrxDate.Where(invOutName => invOutName.itemName.Contains(itemName));
                        }
                        if (itemUnit != "%")
                        {
                            rptTrxDate = rptTrxDate.Where(invOutUnit => invOutUnit.unit.Contains(department));
                        }
                        return Json(rptTrxDate.ToList());
                    }
                    else{
                        var rptIsseudDate = (from invOutHeader in dbContext.InventoryOutTrxHeaders
                                          join departments in dbContext.Departments on invOutHeader.Department equals departments.ID
                                          join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                          join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                          join itemMasterUnit in dbContext.itemMasterUnits on invOutDetail.Unit equals itemMasterUnit.itemMasterUnitUnit
                                          join unitCode in dbContext.UnitCodes on invOutDetail.Unit equals unitCode.Code
                                          join invInDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInDetail.TransactionNo
                                          where invOutHeader.IssuedDate >= DateTime.Parse(dateFromC) && invOutHeader.IssuedDate <= DateTime.Parse(dateToC)
                                          select new
                                          {
                                              HeaderTransactionNo = invOutHeader.TransactionNo,
                                              transactionDate = invOutHeader.TransactionDate,
                                              issuedBy = invOutHeader.IssuedBy,
                                              receivedBy = invOutHeader.ReceivedBy,
                                              department = invOutHeader.Department,
                                              departmentName = departments.DepartmentName,
                                              referenceNo = invOutHeader.ReferenceNo,
                                              HeaderRemarks = invOutHeader.Remarks,
                                              itemID = invOutDetail.ItemID,
                                              itemName = itemMaster.ItemName,
                                              unit = invOutDetail.Unit,
                                              itemMasterUnitUnit = itemMasterUnit.itemMasterUnitUnit,
                                              description = unitCode.Description,
                                              in_TrxNo = invOutDetail.In_TrxNo,
                                              lotNumber = invInDetail.LotNumber,
                                              quantity = invOutDetail.Quantity,
                                              DetailRemarks = invOutDetail.Remarks
                                          }).Distinct();
                        if (rptIsseudDate != null)
                        {
                            if (trxNum != null)
                            {
                                rptIsseudDate = rptIsseudDate.Where(invOutTrx => invOutTrx.HeaderTransactionNo.Contains(trxNum));
                            }
                            if (itemName != "%")
                            {
                                rptIsseudDate = rptIsseudDate.Where(invOutName => invOutName.itemName.Contains(itemName));
                            }
                            if (itemUnit != "%")
                            {
                                rptIsseudDate = rptIsseudDate.Where(invOutUnit => invOutUnit.unit.Contains(department));
                            }
                            return Json(rptIsseudDate.ToList());
                        }
                    }
                    return Ok();
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
