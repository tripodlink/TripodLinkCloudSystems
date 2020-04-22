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
    public class InventoryOutHeaderController : AppController
    {   
        public InventoryOutHeaderController(AppDbContext dbContext, ILogger<InventoryOutHeaderController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<InventoryOutTrxHeader> Index()
        {
            var invenOut = dbContext.InventoryOutTrxHeaders.ToList();
            return invenOut;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] InventoryOutTrxHeader inventoryOut)
        {
            try
            {
                dbContext.InventoryOutTrxHeaders.Add(inventoryOut);
                dbContext.SaveChanges();
                return Ok(inventoryOut);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult findLotNum(string itemID, string unit)
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
        public IActionResult findTrxNum(string trxNUm)
        {
            try
            {
                var trxNumList = dbContext.InventoryOutTrxHeaders.Where(data => data.TransactionNo == trxNUm).ToList();
                if (trxNumList != null)
                {
                    return Ok(trxNumList);
                }
                else
                {
                    return Ok(trxNumList); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        public IActionResult findPendingTrx()
        {
            try
            {
                var trxNumList = dbContext.InventoryOutTrxHeaders.Where(data => data.Status == "P").ToList();
                if (trxNumList != null)
                {
                    return Json(trxNumList);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public IActionResult DeleteAllTrx(string trxNum)
        {
            try
            {
                InventoryOutTrxHeader allTrxHeader = dbContext.InventoryOutTrxHeaders.Find(trxNum);

                if (allTrxHeader != null)
                {
                    dbContext.InventoryOutTrxHeaders.Remove(allTrxHeader);
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
        [HttpPost]
        public IActionResult UpdatePendingTrx([FromBody] InventoryOutTrxHeader invOutTrx)
        {
            try
            {
                InventoryOutTrxHeader findTrxNum = dbContext.InventoryOutTrxHeaders.Find(invOutTrx.TransactionNo);

                if (findTrxNum != null)
                {

                    findTrxNum.Status = invOutTrx.Status;
                    dbContext.InventoryOutTrxHeaders.Update(findTrxNum);
                    dbContext.SaveChanges();

                    return Json(findTrxNum);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{invOutTrx.TransactionNo}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
   
}
