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
               //IEnumerable<ItemMasterUnit> imuList =  dbContext.itemMasterUnits.Where(imu => imu.ID == id).ToList();

               // //var x= dbContext.itemMasterUnits.Where(imu => imu.ID == id).ToList().Join<>

                var itemMu = dbContext.itemMasterUnits.Where(data => data.ID == id).ToList();

                var itemMasterUnitJoinUnitCode = from ItemMasterUnit in dbContext.itemMasterUnits
                                                 join UnitCode in dbContext.UnitCodes on ItemMasterUnit.itemMasterUnitUnit
                                                 equals UnitCode.Code
                                                 where ItemMasterUnit.ID == id
                                                 select new { ItemMasterUnit = ItemMasterUnit.ID, 
                                                     UnitCode = UnitCode.Code, 
                                                     UnitDescription = UnitCode.Description, 
                                                     ConversionFactor = ItemMasterUnit.itemMasterUnitConversion };


                if (itemMasterUnitJoinUnitCode != null)
                {
                    return Json(itemMasterUnitJoinUnitCode.ToList());;
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
            try
            {
                dbContext.itemMasterUnits.Add(itemMasterUnit);
                dbContext.SaveChanges();
                return Ok(itemMasterUnit);
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
                ItemMasterUnit itemMu = dbContext.itemMasterUnits.Find(itemMasterUnit.ID,itemMasterUnit.itemMasterUnitUnit);

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
        public IActionResult Delete(string id,string unit)
        {
            try
            {
                ItemMasterUnit itemMu = dbContext.itemMasterUnits.Find(id, unit);

                if (itemMu != null)
                {
                    dbContext.itemMasterUnits.Remove(itemMu);
                    dbContext.SaveChanges();

                    return Json(id);
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
        [HttpDelete]
        public IActionResult DeleteAll(string id)
        {
            try
            {
                IEnumerable<ItemMasterUnit> itemMulist = dbContext.itemMasterUnits.Where(data => data.ID == id).ToList();


                if (itemMulist != null)
                {  
                    dbContext.itemMasterUnits.RemoveRange(itemMulist);
                    dbContext.SaveChanges();

                    return Json(id);
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
