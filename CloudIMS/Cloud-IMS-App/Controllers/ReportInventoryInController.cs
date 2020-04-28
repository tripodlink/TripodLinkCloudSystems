using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Clauses;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]
    public class ReportInventoryInController : AppController
    {
        

        public ReportInventoryInController(AppDbContext dbContext, ILogger<InventoryInController> logger)
             : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult GetReportInventoryIN(string ItemID, string ItemUNIT, string supplierID, DateTime fromDT,DateTime toDT)
        {
            try
            {
                var from_dt = fromDT.ToString("yyyy-MM-dd");
                var to_dt = toDT.ToString("yyyy-MM-dd");



                var rptinvin = from inv_in_trxdtl in dbContext.InventoryInTrxDetails
                               join inv_in_trxhdr in dbContext.InventoryInTrxHeaders on inv_in_trxdtl.TransactionNo equals inv_in_trxhdr.TransactionNo
                               join im in dbContext.ItemMasters on inv_in_trxdtl.ItemID equals im.ID
                               join uc in dbContext.UnitCodes on inv_in_trxdtl.Unit equals uc.Code
                               join sup in dbContext.Suppliers on inv_in_trxhdr.Supplier equals sup.ID
                               where (inv_in_trxhdr.TransactionDate >= DateTime.Parse(from_dt) && inv_in_trxhdr.TransactionDate <= DateTime.Parse(to_dt))
                               
                            
                               select new
                               {
                                   itemID = inv_in_trxdtl.ItemID,
                                   itemunitID = inv_in_trxdtl.Unit,
                                   supplierID = inv_in_trxhdr.Supplier,

                                   trxno = inv_in_trxdtl.TransactionNo,
                                   trxdate = inv_in_trxhdr.TransactionDate,
                                   itemname = im.ItemName,
                                   itemunit = uc.Description,
                                   qty = inv_in_trxdtl.Quantity,
                                   lotno = inv_in_trxdtl.LotNumber,
                                   supplier = sup.Name

                               };
                if (ItemID != "%")
                {
                    rptinvin = rptinvin.Where(imid => imid.itemID.Contains(ItemID));
                }
                if (ItemUNIT !="%")
                {
                    rptinvin = rptinvin.Where(imunit => imunit.itemunitID.Contains(ItemUNIT));
                }
                if (supplierID != "%")
                {
                    rptinvin = rptinvin.Where(supID => supID.supplierID.Contains(supplierID));
                }

                if (rptinvin != null)
                {
                    return Json(rptinvin.ToList()); ;
                }
                else
                {

                    throw new Exception($"User Not found with a user ID of ''.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(GetErrorMessage(e));
            }
           
        }
    }
}

