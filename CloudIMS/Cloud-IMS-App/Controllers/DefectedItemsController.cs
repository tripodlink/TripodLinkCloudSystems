using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]
    public class DefectedItems : AppController
    {

        public DefectedItems(AppDbContext dbContext, ILogger<DefectedItems> logger)
            : base(dbContext, logger)
        {
            this.dbContext = dbContext;

        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getLotNumber(String iid)
        {
            try
            {

                var lotnumbers = (from inventoryin_trx_detail in dbContext.InventoryInTrxDetails
                                  where inventoryin_trx_detail.ItemID == iid
                                  orderby inventoryin_trx_detail.LotNumber
                                  select new
                                  {
                                      lotNumber = inventoryin_trx_detail.LotNumber
                                  });
                return Json(lotnumbers.ToList());

            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }

        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getUnit(String iid)
        {
            try
            {
                var unit = (from itemMasterUnit in dbContext.itemMasterUnits
                            join ic in dbContext.UnitCodes on itemMasterUnit.itemMasterUnitUnit equals ic.Code
                            where itemMasterUnit.ID == iid
                            select new
                            {
                                unit = itemMasterUnit.ID,
                                unitName = ic.Description,
                                unitConversion = itemMasterUnit.itemMasterUnitConversion
                            }).Distinct();
                return Json(unit.ToList());
            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }



        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getName(String iid)
        {
            try
            {
                var name = (from itemMaster in dbContext.ItemMasters
                            where itemMaster.ID == iid
                            select new
                            {
                                itemName = itemMaster.ItemName
                            }).First();

                string finalName = name.itemName;
                return Json(finalName);

            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getTransactionNumbers(String iid, String lotNum, String  stat) {

            try
            {
                if (stat == "IN")
                {
                    var trx_no = (from invInTrxDtls in dbContext.InventoryInTrxDetails
                                 where invInTrxDtls.ItemID == iid && invInTrxDtls.LotNumber == lotNum
                                 select new
                                 {
                                     transactionNo = invInTrxDtls.TransactionNo
                                 });
                    return Json(trx_no.ToList());
                }
                else if (stat == "OUT")
                {
                    var trx_no = (from invOutTrxDtls in dbContext.InventoryOutTrxDetails
                                 join invInTrxDtls in dbContext.InventoryInTrxDetails on invOutTrxDtls.In_TrxNo equals invInTrxDtls.TransactionNo
                                 where invOutTrxDtls.ItemID == iid && invInTrxDtls.LotNumber == lotNum
                                 select new
                                 {
                                     transactionNo = invOutTrxDtls.TransactionNo
                                 });
                    return Json(trx_no.ToList());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult getFinalResult(String iid, String lotNum, String stat)
        {
            try
            {
               if(stat == "IN")
                {
                    var final = (from invInTrxHd in dbContext.InventoryInTrxHeaders
                                 join invInTrxDtls in dbContext.InventoryInTrxDetails on invInTrxHd.TransactionNo equals invInTrxDtls.TransactionNo
                                 join supplier in dbContext.Suppliers on invInTrxHd.Supplier equals supplier.ID
                                 where invInTrxDtls.ItemID == iid && invInTrxDtls.LotNumber == lotNum
                                 select new
                                 {
                                     transactionNo = invInTrxHd.TransactionNo,
                                     transactionDate = invInTrxHd.TransactionDate,
                                     poNumber = invInTrxHd.PONumber,
                                     invoiceNo = invInTrxHd.InvoiceNo,
                                     receivedDate = invInTrxHd.ReceivedDate,
                                     receivedBy = invInTrxHd.ReceivedBy,
                                     supplier = supplier.Name,
                                     referenceNo = invInTrxHd.ReferenceNo,
                                     documnetNo = invInTrxHd.DocumnetNo
                                 });
                    return Json(final.ToList());
                }
                else if(stat == "OUT")
                {
                    var final = (from invOutTrxHd in dbContext.InventoryOutTrxHeaders
                                 join invOutTrxDtls in dbContext.InventoryOutTrxDetails on invOutTrxHd.TransactionNo equals invOutTrxDtls.TransactionNo
                                 join invInTrxHd in dbContext.InventoryInTrxHeaders on invOutTrxDtls.In_TrxNo equals invInTrxHd.TransactionNo
                                 join invInTrxDtls in dbContext.InventoryInTrxDetails on invInTrxHd.TransactionNo equals invInTrxDtls.TransactionNo
                                 join supplier in dbContext.Suppliers on invInTrxHd.Supplier equals supplier.ID
                                 where invInTrxDtls.ItemID == iid && invInTrxDtls.LotNumber == lotNum
                                 select new
                                 {
                                     transactionNo = invOutTrxHd.TransactionNo,
                                     transactionDate = invOutTrxHd.TransactionDate,
                                     poNumber = invInTrxHd.PONumber,
                                     invoiceNo = invInTrxHd.InvoiceNo,
                                     receivedDate = invOutTrxHd.IssuedDate,
                                     receivedBy = invOutTrxHd.ReceivedBy,
                                     supplier = supplier.Name,
                                     referenceNo = invOutTrxHd.ReferenceNo,
                                     documnetNo = invInTrxHd.DocumnetNo
                                 });
                    return Json(final.ToList());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult fillAll(String def_trx_no, String stat)
        {
            try
            {
                if(stat == "IN")
                {
                    var final = (from dfctItems in dbContext.DefectedItemsModel
                                 join uc in dbContext.UnitCodes on dfctItems.ItemUnit equals uc.Description
                                 join imu in dbContext.itemMasterUnits on uc.Code equals imu.itemMasterUnitUnit
                                 join im in dbContext.ItemMasters on dfctItems.ItemID equals im.ID
                                 join invInTrxHd in dbContext.InventoryInTrxHeaders on dfctItems.TransactionNo equals invInTrxHd.TransactionNo
                                 join supplier in dbContext.Suppliers on invInTrxHd.Supplier equals supplier.ID
                                 where dfctItems.DefectTransactionNo == def_trx_no
                                 select new
                                 {
                                     status = dfctItems.Status,
                                     itemName = im.ItemName,
                                     itemId = dfctItems.ItemID,
                                     lotNo = dfctItems.LotNumber,
                                     itemUnit = dfctItems.ItemUnit,
                                     itemUnitConversion = imu.itemMasterUnitConversion,
                                     quantity = dfctItems.Quantity,
                                     remarks = dfctItems.Remarks,
                                     transactionNo = dfctItems.TransactionNo,

                                     transactionDate = invInTrxHd.TransactionDate,
                                     poNo = invInTrxHd.PONumber,
                                     invNo = invInTrxHd.InvoiceNo,
                                     receivedDate = invInTrxHd.ReceivedDate,
                                     receivedBy = invInTrxHd.ReceivedBy,
                                     supplierName = supplier.Name,
                                     refNo = invInTrxHd.ReferenceNo,
                                     documentNo = invInTrxHd.DocumnetNo
                                 });
                    return Json(final.ToList());
                    
                }
                else if(stat == "OUT")
                {

                    var final = (from dfctItems in dbContext.DefectedItemsModel
                                 join im in dbContext.ItemMasters on dfctItems.ItemID equals im.ID
                                 join uc in dbContext.UnitCodes on dfctItems.ItemUnit equals uc.Description
                                 join imu in dbContext.itemMasterUnits on uc.Code equals imu.itemMasterUnitUnit
                                 join invOutTrxHd in dbContext.InventoryOutTrxHeaders on dfctItems.TransactionNo equals invOutTrxHd.TransactionNo
                                 join invOutTrxDt in dbContext.InventoryOutTrxDetails on invOutTrxHd.TransactionNo equals invOutTrxDt.TransactionNo
                                 join invInTrxHd in dbContext.InventoryInTrxHeaders on invOutTrxDt.In_TrxNo equals invInTrxHd.TransactionNo
                                 join supplier in dbContext.Suppliers on invInTrxHd.Supplier equals supplier.ID
                                 where dfctItems.DefectTransactionNo == def_trx_no
                                 select new
                                 {
                                     status = dfctItems.Status,
                                     itemId = dfctItems.ItemID,
                                     itemName = im.ItemName,
                                     lotNo = dfctItems.LotNumber,
                                     itemUnit = dfctItems.ItemUnit,
                                     itemUnitConversion = imu.itemMasterUnitConversion,
                                     quantity = dfctItems.Quantity,
                                     remarks = dfctItems.Remarks,
                                     transactionNo = dfctItems.TransactionNo,

                                     transactionDate = invOutTrxHd.TransactionDate,
                                     poNo = invInTrxHd.PONumber,
                                     invNo = invInTrxHd.InvoiceNo,
                                     receivedDate = invOutTrxHd.IssuedDate,
                                     receivedBy = invOutTrxHd.IssuedBy,
                                     supplierName = supplier.Name,
                                     refNo = invOutTrxHd.ReferenceNo,
                                     documentNo = invInTrxHd.DocumnetNo
                                 });
                    return Json(final.ToList());
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
        }


        [Route("[action]")]
        [HttpGet]
        public IActionResult getDefectedItemsToList(String itemName)
        {
            try
            {
                if (itemName != "ALL")
                {
                    var dfToList = (from di in dbContext.DefectedItemsModel
                                    join im in dbContext.ItemMasters on di.ItemID equals im.ID
                                    where im.ItemName.Contains("%" + itemName + "%")
                                    orderby im.ItemName 
                                    
                                    select new
                                    {
                                        defTransactionNo = di.DefectTransactionNo,
                                        itemName = im.ItemName,
                                        lotNumber = di.LotNumber,
                                        transactionNo = di.TransactionNo,
                                        transactionDate = di.TransactionDate,
                                        quantity = di.Quantity,
                                        status = di.Status

                });
                    return Json(dfToList.ToList());
                }
                else
                {
                    var dfToList = (from di in dbContext.DefectedItemsModel
                                    join im in dbContext.ItemMasters on di.ItemID equals im.ID
                                    select new
                                    {
                                        defTransactionNo = di.DefectTransactionNo,
                                        itemName = im.ItemName,
                                        lotNumber = di.LotNumber,
                                        transactionNo = di.TransactionNo,
                                        transactionDate = di.TransactionDate,
                                        quantity = di.Quantity,
                                        status = di.Status
                                    });
                    return Json(dfToList.ToList());

                }
            }
            catch (Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }

        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult AddDefectedItem([FromBody] DefectedItemsModel dfI)
        {
            var defected = new DefectedItemsModel();
            try
            {
                DefectedItemsModel dim = new DefectedItemsModel()
                {
                    DefectTransactionNo = dfI.DefectTransactionNo,
                    ItemID = dfI.ItemID,
                    LotNumber = dfI.LotNumber,
                    TransactionNo = dfI.TransactionNo,
                    TransactionDate = dfI.TransactionDate,
                    Status = dfI.Status,
                    ItemUnit = dfI.ItemUnit,
                    Quantity = dfI.Quantity,
                    Remarks = dfI.Remarks

                };
                dbContext.DefectedItemsModel.Add(dfI);
                dbContext.SaveChanges();
                return Ok(defected);
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
                    mcon.CommandText = "select setAuto_number('INVDEFECT')";
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

        [Route("")]
        [Route("[action]")]
        public IEnumerable<DefectedItemsModel> Index()
        {
            var defectedItems = dbContext.DefectedItemsModel.ToList();
            return defectedItems;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult updateDefectedItem([FromBody] DefectedItemsModel defectedItems)
        {
            try
            {
                DefectedItemsModel df = dbContext.DefectedItemsModel.Find(defectedItems.DefectTransactionNo);

                if (df != null)
                {
                    df.LotNumber = defectedItems.LotNumber;
                    df.ItemUnit = defectedItems.ItemUnit;
                    df.Quantity = defectedItems.Quantity;
                    df.Remarks = defectedItems.Remarks;
                    df.TransactionNo = defectedItems.TransactionNo;


                    dbContext.SaveChanges();

                    return Ok(df);
                }
                else
                {
                    throw new Exception("Cannot update.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult deleteDefectedItem(string trx_def)
        {
            try
            {
                DefectedItemsModel def = dbContext.DefectedItemsModel.Find(trx_def);

                if (def != null)
                {
                    dbContext.DefectedItemsModel.Remove(def);
                    dbContext.SaveChanges();

                    return Json(trx_def);
                }
                else
                {
                    throw new Exception("Cannot delete");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getRemarks()
        {
            try
            {
                var dfToList = (from di in dbContext.DefectedItemsModel
                                select new
                                {
                                    remarks = di.Remarks

                                }).Distinct();
                return Json(dfToList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult getItems()
        {
            try
            {
                var dfToList = (from itd in dbContext.InventoryInTrxDetails
                                join im in dbContext.ItemMasters on itd.ItemID equals im.ID
                                select new
                                {
                                    itemId = im.ID,
                                    itemName = im.ItemName

                                });
                return Json(dfToList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }




    }
}
