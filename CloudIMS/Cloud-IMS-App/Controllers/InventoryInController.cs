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
                        TransactionDate = DateTime.Now,
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
                                                 join Itemmaster in dbContext.ItemMasters on ItemMasterUnit.ID 
                                                 equals Itemmaster.ID
                                                 join UnitCode in dbContext.UnitCodes on ItemMasterUnit.itemMasterUnitUnit
                                                 equals UnitCode.Code
                                                 where Itemmaster.ID == id
                                                 where ItemMasterUnit.itemMasterUnitUnit == idunit
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
    }
}

