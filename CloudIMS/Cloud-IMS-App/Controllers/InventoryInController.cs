using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]
    public class InventoryInController : AppController
    {
        

        public InventoryInController(AppDbContext dbContext, ILogger<InventoryInController> logger)
             : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }


        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<InventoryInTrxHeader> GetInventoryInTrxHeader()
        {
            var inv_in_trx_header = dbContext.InventoryInTrxHeaders.ToList();
            return inv_in_trx_header;
        }


        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<InventoryInTrxDetail> GetInventoryInTrxDetails()
        {
            var inv_in_trx_details = dbContext.InventoryInTrxDetails.ToList();
            return inv_in_trx_details;
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Add_Trx_Header([FromBody] InventoryInTrxHeader InvInTrxHeader)
        {
          
                try
                {


                InventoryInTrxHeader invtoadd = new InventoryInTrxHeader()
                    {

                        TransactionNo = InvInTrxHeader.TransactionNo,
                        TransactionDate = InvInTrxHeader.TransactionDate,
                        ReceivedDate = InvInTrxHeader.ReceivedDate,
                        ReceivedBy = InvInTrxHeader.ReceivedBy,
                        PONumber = InvInTrxHeader.PONumber,
                        InvoiceNo = InvInTrxHeader.InvoiceNo,
                        ReferenceNo = InvInTrxHeader.ReferenceNo,
                        DocumnetNo = InvInTrxHeader.DocumnetNo,
                        Supplier = InvInTrxHeader.Supplier,
                        Remarks = InvInTrxHeader.Remarks,


                    };

                    dbContext.InventoryInTrxHeaders.Add(invtoadd);
                    dbContext.SaveChanges();


                  
                    return Json(InvInTrxHeader);
                }
                catch (Exception ex)
                {
                  
                    return BadRequest(GetErrorMessage(ex));
                }
            }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add_Trx_Detail([FromBody] InventoryInTrxDetail InvInTrxDetail)
        {

            try
            {
                InventoryInTrxDetail invtoadd = new InventoryInTrxDetail()
                {

                    TransactionNo = InvInTrxDetail.TransactionNo,
                    ItemID = InvInTrxDetail.ItemID,
                    Unit = InvInTrxDetail.Unit,
                    Quantity = InvInTrxDetail.Quantity,
                    LotNumber = InvInTrxDetail.LotNumber,
                    ExpirationDate = InvInTrxDetail.ExpirationDate,
                    Count = InvInTrxDetail.Count,
                    RemainigCount = InvInTrxDetail.RemainigCount,
                };

                dbContext.InventoryInTrxDetails.Add(invtoadd);
                dbContext.SaveChanges();



                return Json(InvInTrxDetail);
            }
            catch (Exception ex)
            {

                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update_Trx_Header([FromBody] InventoryInTrxHeader InvInTrxHeader)
        {

            try
            {


                InventoryInTrxHeader invtrxlist = new InventoryInTrxHeader()
                {

                    TransactionNo = InvInTrxHeader.TransactionNo,
                    TransactionDate = InvInTrxHeader.TransactionDate,
                    ReceivedDate = InvInTrxHeader.ReceivedDate,
                    ReceivedBy = InvInTrxHeader.ReceivedBy,
                    PONumber = InvInTrxHeader.PONumber,
                    InvoiceNo = InvInTrxHeader.InvoiceNo,
                    ReferenceNo = InvInTrxHeader.ReferenceNo,
                    DocumnetNo = InvInTrxHeader.DocumnetNo,
                    Supplier = InvInTrxHeader.Supplier,
                    Remarks = InvInTrxHeader.Remarks,


                };
                if(invtrxlist != null)
                {
                    dbContext.InventoryInTrxHeaders.Update(invtrxlist);
                    dbContext.SaveChanges();
                    return Json(InvInTrxHeader);
                }
                else
                {
                    throw new Exception($"Transaction Not found with transaction no. of '{invtrxlist.TransactionNo}'.");
                }
             
            }
            catch (Exception ex)
            {

                return BadRequest(GetErrorMessage(ex));
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Update_Trx_Detail([FromBody] InventoryInTrxDetail InvInTrxDetail)
        {

            try
            {
                InventoryInTrxDetail invtrxlist = new InventoryInTrxDetail()
                {

                    TransactionNo = InvInTrxDetail.TransactionNo,
                    ItemID = InvInTrxDetail.ItemID,
                    Unit = InvInTrxDetail.Unit,
                    Quantity = InvInTrxDetail.Quantity,
                    LotNumber = InvInTrxDetail.LotNumber,
                    ExpirationDate = InvInTrxDetail.ExpirationDate,
                    Count = InvInTrxDetail.Count,
                    RemainigCount = InvInTrxDetail.RemainigCount,
                };

                if (invtrxlist != null)
                {
                    dbContext.InventoryInTrxDetails.Update(invtrxlist);
                    dbContext.SaveChanges();
                    return Json(InvInTrxDetail);
                }
                else
                {
                    throw new Exception($"Transaction Not found with transaction no. of '{invtrxlist.TransactionNo}'.");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public IActionResult Delete_Trx_Header(string trxno)
        {
            try
            {
                InventoryInTrxHeader invtrxhdr = dbContext.InventoryInTrxHeaders.Find(trxno);
                if (invtrxhdr != null)
                {

                    dbContext.InventoryInTrxHeaders.Remove(invtrxhdr);
                    dbContext.SaveChanges();

                    return Json(trxno);
                }
                else
                {
                    throw new Exception($"Transaction Not found with a user transaction number of '{trxno}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult Delete_Trx_Detail(string trxno)
        {
            try
            {
                InventoryInTrxDetail invtrxdtl = dbContext.InventoryInTrxDetails.Find(trxno);
                if (invtrxdtl != null)
                {

                    dbContext.InventoryInTrxDetails.Remove(invtrxdtl);
                    dbContext.SaveChanges();

                    return Json(trxno);
                }
                else
                {
                    throw new Exception($"Transaction Not found with a user transaction number of '{trxno}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult GetUnitCodeFromItem(string id)
        {
            try
            {
                var itemMasterUnitJoinUnitCode = from ItemMasterUnit in dbContext.itemMasterUnits
                                                 join UnitCode in dbContext.UnitCodes on ItemMasterUnit.itemMasterUnitUnit
                                                 equals UnitCode.Code
                                                 where ItemMasterUnit.ID == id
                                                 select new
                                                 {
                                                     ItemMasterUnit = ItemMasterUnit.ID,
                                                     UnitCode = UnitCode.Code,
                                                     UnitDescription = UnitCode.Description,
                                                     ConversionFactor = ItemMasterUnit.itemMasterUnitConversion
                                                 };


                if (itemMasterUnitJoinUnitCode != null)
                {
                    return Json(itemMasterUnitJoinUnitCode.ToList()); ;
                }
                else
                {

                    throw new Exception($"User Not found with a user ID of '{id}'.");
                }

            }
            catch(Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult GetConversionFactorFromItemMasterUnit(string id,string idunit)
        {
            try
            {
                var itemMasterUnitJoinUnitCode = from ItemMasterUnit in dbContext.itemMasterUnits
                                                 join Itemmaster in dbContext.ItemMasters on ItemMasterUnit.ID equals Itemmaster.ID
                                                 join UnitCode in dbContext.UnitCodes on ItemMasterUnit.itemMasterUnitUnit equals UnitCode.Code
                                                 where Itemmaster.ID == id
                                                 where ItemMasterUnit.itemMasterUnitUnit == idunit
                                                 select new
                                                 {
                                                     id = ItemMasterUnit.ID,
                                                     itemMasterUnitUnit = UnitCode.Code,
                                                     description = UnitCode.Description,
                                                     itemMasterUnitConversion = ItemMasterUnit.itemMasterUnitConversion
                                                 };


                if (itemMasterUnitJoinUnitCode != null)
                {
                    return Json(itemMasterUnitJoinUnitCode.ToList()); ;
                }
                else
                {

                    throw new Exception($"ItemID Not found with a ItemID of '{id}'.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(GetErrorMessage(e));
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
                    mcon.CommandText = "select setAuto_number('INVIN')";
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
        [HttpGet]
        public IActionResult GetTrxListInventoryIn()
        {
            try
            {
                var trxListInvIn = from InvInTrxDtl in dbContext.InventoryInTrxDetails
                                                 join InvInTrxHdr in dbContext.InventoryInTrxHeaders on InvInTrxDtl.TransactionNo equals InvInTrxHdr.TransactionNo
                                                 join im in dbContext.ItemMasters on InvInTrxDtl.ItemID equals im.ID
                                                 join uc in dbContext.UnitCodes on InvInTrxDtl.Unit equals uc.Code
                                                 join sup in dbContext.Suppliers on InvInTrxHdr.Supplier equals sup.ID

                                   select new
                                                 {
                                                    transactionNo = InvInTrxDtl.TransactionNo,
                                                    itemID = InvInTrxDtl.ItemID,
                                                    itemName = im.ItemName,
                                                    unit = InvInTrxDtl.Unit,
                                                    unitName = uc.Description,
                                                    quantity = InvInTrxDtl.Quantity,
                                                    lotNumber = InvInTrxDtl.LotNumber,
                                                    expirationDate = InvInTrxDtl.ExpirationDate,
                                                    count = InvInTrxDtl.Count,
                                                    remainigCount = InvInTrxDtl.RemainigCount,
                                                    transactionDate = InvInTrxHdr.TransactionDate,
                                                    receivedDate = InvInTrxHdr.ReceivedDate,
                                                    receivedBy = InvInTrxHdr.ReceivedBy,
                                                    poNumber = InvInTrxHdr.PONumber,
                                                    invoiceNo = InvInTrxHdr.InvoiceNo,
                                                    referenceNo = InvInTrxHdr.ReferenceNo,
                                                    documnetNo = InvInTrxHdr.DocumnetNo, 
                                                    supplierId = InvInTrxHdr.Supplier,
                                                    supplierName = sup.Name,
                                                    remarks = InvInTrxHdr.Remarks
                                                 };


                 return Json(trxListInvIn.ToList());

            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }

        }
    }
}

