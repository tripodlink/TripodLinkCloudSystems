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
    public class ItemMasterUnitController : AppController
    {   
        public ItemMasterUnitController(AppDbContext dbContext, ILogger<ItemMasterUnitController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<ItemMasterUnit> Index()
        {
                var itemMu = dbContext.itemMasterUnits.ToList();
                return itemMu;
        }
        [Route("[action]")]
        public IEnumerable<string> DistinctIndex()
        {
            var itemMu = dbContext.itemMasterUnits.Select(data => data.ID).Distinct().ToList();
            return itemMu;
        }


        [Route("[action]")]
        public IActionResult FindID(string id)
        {
            var itemMulist = dbContext.itemMasterUnits.ToList();
            try
            {
                var itemMu = dbContext.itemMasterUnits.Where(data => data.ID == id).ToList();

                if (itemMu != null)
                {
                    return Ok(itemMu);
                }
                else
                {

                    throw new Exception($"User Not found with a user ID of '{id}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] ItemMasterUnit itemMasterUnit)
        {
            var itemMu = new ItemMasterUnit();
            try
            {
                dbContext.itemMasterUnits.Add(itemMasterUnit);
                dbContext.SaveChanges();
                return Ok(itemMu);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] ItemMasterUnit itemMasterUnit)
        {
            try
            {
                ItemMasterUnit itemMu = dbContext.itemMasterUnits.Find(itemMasterUnit.ID);

                if (itemMu != null)
                {
                    itemMu.itemMasterUnitUnit = itemMasterUnit.itemMasterUnitUnit;
                    itemMu.itemMasterUnitConversion = itemMasterUnit.itemMasterUnitConversion;
                //    dbContext.ItemMasters.Update(itemMaster);
                    dbContext.SaveChanges();

                    return Ok(itemMu);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{itemMu.ID}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                ItemMasterUnit itemMu = dbContext.itemMasterUnits.Find(id);

                if (itemMu != null)
                {
                    dbContext.itemMasterUnits.Remove(itemMu);
                    dbContext.SaveChanges();

                    return Ok(id);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{id}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
   
}
