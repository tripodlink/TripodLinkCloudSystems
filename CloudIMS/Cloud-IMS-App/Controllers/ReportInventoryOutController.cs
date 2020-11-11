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
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

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
        public class ReportTally {
            public string ItemID { get; set; }
            public string ItemName { get; set; }
            public string SupplierName { get; set; }
            public string DateInventoryIn { get; set; }
            public string DateInventoryOut { get; set; }
            public string InvoiceNumber { get; set; }
            public string PONumber { get; set; }
            public string LotNumber { get; set; }
            public string ReceivedBy { get; set; }
            public string Department { get; set; }
            public string ItemUnit { get; set; }
            public string ItemInventoryIn { get; set; }
            public string ItemInventoryOut { get; set; }
            public string ItemDefect { get; set; }
        }

        [Route("[action]")]
        public IActionResult GetTallyReport()
        {
            List<ReportTally> list = new List<ReportTally>();
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var mcom = dbContext.Database.GetDbConnection().CreateCommand();
                    mcom.CommandText = @"select distinct t.item_id as ItemID,im.im_item_name as ItemName,s.sup_name as SupplierName,date(ith.rcvd_date) as DateInventoryIn, 
                                        date(oth.itoh_issued_date) as DateInventoryOut,ipl.inv as InvoiceNumber, ipl.po as PONumber,ipl.ln as LotNumber,ua.ua_user_name as 
                                        ReceivedBy, oth.itoh_remarks as Department, uc.uc_description as ItemUnit , itd.quantity as ItemInventoryIn, otd.itoh_quantity as 
                                        ItemInventoryOut, t.defect as ItemDefect from (select itd.trxno as transaction_no, itd.item_id as item_id, unit as item_unit, 
                                        ith.rcvd_by as rcvd_by, di.quantity as defect, itd.quantity as quantity from inventoryin_trx_detail as itd left join inventoryin_trx_header
                                        ith on itd.trxno = ith.trxno left join defected_items di on itd.trxno = di.trxno union all select otd.itoh_trxno as transaction_no, 
                                        otd.itoh_item_id as item_id, itoh_unit as item_unit, oth.itoh_rcvd_by as rcvd_by, di.quantity as defect, otd.itoh_quantity as quantity 
                                        from inventoryout_trx_detail as otd left join inventoryout_trx_header oth on otd.itoh_trxno = oth.itoh_trxno left join defected_items
                                         di on otd.itoh_trxno = di.trxno) as t left join item_master im on t.item_id = im.im_id left join supplier s on im.im_supp = s.sup_id
                                        left join inventoryin_trx_header ith on t.transaction_no = ith.trxno left join inventoryout_trx_header oth on t.transaction_no = 
                                        oth.itoh_trxno left join inventoryin_trx_detail itd on t.transaction_no = itd.trxno left join inventoryout_trx_detail otd on 
                                        t.transaction_no = otd.itoh_trxno left join user_account ua on t.rcvd_by = ua.ua_user_id left join unit_code uc on t.item_unit = 
                                        uc.uc_code left join (select test.TransactionNumber as tn,test.InvoiceNumber as inv,test.PONumber as po,test.LotNumber as ln from 
                                        (select ith.trxno as TransactionNumber , ith.invoice_number as InvoiceNumber, ith.po_number as PONumber, itd.lotno as LotNumber  from 
                                        inventoryin_trx_header as ith left join inventoryin_trx_detail itd on ith.trxno = itd.trxno union all select oth.itoh_trxno as 
                                        TransactionNumber , ith.invoice_number as InvoiceNumber, ith.po_number as PONumber, itd.lotno as LotNumber from inventoryout_trx_header 
                                        as oth left join inventoryout_trx_detail otd on oth.itoh_trxno = otd.itoh_trxno left join inventoryin_trx_header ith on otd.itoh_in_trxno 
                                        = ith.trxno left join inventoryin_trx_detail itd on ith.trxno = itd.trxno) as test) ipl on t.transaction_no = ipl.tn";
                    var returnVal = mcom.ExecuteReader();
                    try
                    {
                        while (returnVal.Read())
                        {
                            ReportTally report = new ReportTally();
                            report.ItemID = !returnVal.IsDBNull(0) ? returnVal.GetString(0).ToString():"" ;
                            report.ItemName = !returnVal.IsDBNull(1) ? returnVal.GetString(1).ToString():"";
                            report.SupplierName = !returnVal.IsDBNull(2) ? returnVal.GetString(2).ToString(): "";
                            report.DateInventoryIn = !returnVal.IsDBNull(3) ? returnVal.GetDateTime(3).ToString("MMM dd, yyyy") : "";
                            report.DateInventoryOut = !returnVal.IsDBNull(4) ? returnVal.GetDateTime(4).ToString("MMM dd, yyyy") : "";
                            report.InvoiceNumber = !returnVal.IsDBNull(5) ? returnVal.GetString(5).ToString() : "";
                            report.PONumber = !returnVal.IsDBNull(6) ? returnVal.GetString(6).ToString() : "";
                            report.LotNumber = !returnVal.IsDBNull(7) ? returnVal.GetString(7).ToString() : "";
                            report.ReceivedBy = !returnVal.IsDBNull(8) ? returnVal.GetString(8).ToString() : "";
                            report.Department = !returnVal.IsDBNull(9) ? returnVal.GetString(9).ToString() : "";
                            report.ItemUnit = !returnVal.IsDBNull(10) ? returnVal.GetString(10).ToString() : "";
                            report.ItemInventoryIn = !returnVal.IsDBNull(11)?returnVal.GetDouble(11).ToString() : "";
                            report.ItemInventoryOut = !returnVal.IsDBNull(12)?returnVal.GetDouble(12).ToString() : "";
                            report.ItemDefect = !returnVal.IsDBNull(13)?returnVal.GetDouble(13).ToString() : "";
                            list.Add(report);
                        }
                    }
                    catch(Exception e)
                    {}

                    returnVal.Close();
                    return Json(list);
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    return BadRequest(GetErrorMessage(ex));
                }
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
