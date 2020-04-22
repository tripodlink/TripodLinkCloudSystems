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
    public class InventoryOutDetailController : AppController
    {   
        public InventoryOutDetailController(AppDbContext dbContext, ILogger<InventoryOutDetailController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<InventoryOutTrxDetail> Index()
        {
            var inventOutDetail = dbContext.InventoryOutTrxDetails.ToList();
            return inventOutDetail;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] InventoryOutTrxDetail inventoryOutDetail)
        {
            try
            {
                dbContext.InventoryOutTrxDetails.Add(inventoryOutDetail);
                dbContext.SaveChanges();
                return Ok(inventoryOutDetail);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findTrxNum(string itemID, string unit)
        {
            try
            {
                var lotNumber = dbContext.InventoryInTrxDetails.Where(data => data.ItemID == itemID).Where(data => data.Unit == unit);
                if (lotNumber != null)
                {
                    return Ok(lotNumber);
                }
                else
                {
                    return Json(lotNumber);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findRemainingCount(string itemID, string unit,string lotNum)
        {
            try
            {
                var remainingCount = dbContext.InventoryInTrxDetails
                    .Where(data => data.ItemID == itemID)
                    .Where(data => data.Unit == unit)
                    .Where(data => data.LotNumber == lotNum);
                if (remainingCount != null)
                {
                    return Ok(remainingCount);
                }
                else
                {
                    return Json(remainingCount);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        [HttpDelete]
        public IActionResult DeleteAll(string trxNum)
        {
            try
            {
                IEnumerable<InventoryOutTrxDetail> invDeleteAll = dbContext.InventoryOutTrxDetails.Where(data => data.TransactionNo == trxNum).ToList();


                if (invDeleteAll != null)
                {
                    dbContext.InventoryOutTrxDetails.RemoveRange(invDeleteAll);
                    dbContext.SaveChanges();

                    return Json(trxNum);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{trxNum}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult JoinINVtoItemMaster()
        {
            try
            {
                var Join = (from invInDetail in dbContext.InventoryInTrxDetails
                           join ItemMaster in dbContext.ItemMasters on invInDetail.ItemID equals ItemMaster.ID
                           select new
                           {
                               itemID = invInDetail.ItemID,
                               itemName = ItemMaster.ItemName,
                           }).Distinct();
                if (Join != null)
                {
                    return Json(Join.ToList()); ;
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
