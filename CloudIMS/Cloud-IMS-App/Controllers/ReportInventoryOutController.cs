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
        public IActionResult FindReport(string trxNum, string itemName, string itemUnit, string depart, string trxDateFrom, string trxDateTo)
        {
               
            try
            {
                var dateFrom = DateTime.Parse(trxDateFrom);
                var dateTo = DateTime.Parse(trxDateTo);
                var itemMasterUnitJoinUnitCode = from invOutHeader in dbContext.InventoryOutTrxHeaders
                                                 join department in dbContext.Departments on invOutHeader.Department equals department.ID
                                                 join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                                 join itemMaster in dbContext.ItemMasters on invOutDetail.ItemID equals itemMaster.ID
                                                 join itemMasterUnit in dbContext.itemMasterUnits on invOutDetail.Unit equals itemMasterUnit.itemMasterUnitUnit
                                                 join unitCode in dbContext.UnitCodes on invOutDetail.Unit equals unitCode.Code
                                                 join invInDetail in dbContext.InventoryInTrxDetails on invOutDetail.In_TrxNo equals invInDetail.TransactionNo
                                                 where invOutHeader.TransactionNo == trxNum
                                                 && invOutDetail.ItemID == itemName && invOutDetail.Unit == itemUnit
                                                 && invOutHeader.Department == depart
                                                 && (invOutHeader.TransactionDate >= dateFrom && invOutHeader.TransactionDate <= dateTo)
                                                 select new {
                                                     HeaderTransactionNo = invOutHeader.TransactionNo,
                                                     transactionDate = invOutHeader.TransactionDate,
                                                     issuedBy = invOutHeader.IssuedBy,
                                                     receivedBy = invOutHeader.ReceivedBy,
                                                     department = invOutHeader.Department,
                                                     departmentName = department.DepartmentName,
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

                                                 };
                                                 
                if (itemMasterUnitJoinUnitCode != null)
                {
                    return Json(itemMasterUnitJoinUnitCode.ToList()); ;
                }
                else
                {

                    throw new Exception($"User Not found with a user ID of '{""}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
   
}
