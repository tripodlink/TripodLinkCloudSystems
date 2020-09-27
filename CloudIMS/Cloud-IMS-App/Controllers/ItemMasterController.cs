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
    public class ItemMasterController : AppController
    {   
        public ItemMasterController(AppDbContext dbContext, ILogger<ItemMasterController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<ItemMaster> Index()
        {
                var itemM = dbContext.ItemMasters.ToList();
                return itemM;
        }
        [Route("[action]")]
        public IActionResult JoinAllDic()
        {
            try
            {
                var getAllDic = from itemMaster in dbContext.ItemMasters
                                join itemGroup in dbContext.ItemGroups on itemMaster.ItemGroup equals itemGroup.ID
                                join unitCode in dbContext.UnitCodes on itemMaster.Unit equals unitCode.Code
                                join supplierData in dbContext.Suppliers on itemMaster.Supplier equals supplierData.ID
                                join manufactData in dbContext.Manufacturers on itemMaster.Manufacturer equals manufactData.ID
                                select new
                                {
                                    itemMasterID = itemMaster.ID,
                                    itemGroupID = itemGroup.ID,
                                    itemgroupname = itemGroup.ItemGroupName,
                                    ItemName = itemMaster.ItemName,
                                    code = unitCode.Code,
                                    description = unitCode.Description,
                                    minimumStockLevel = itemMaster.MinimumStockLevel,
                                    suppID = supplierData.ID,
                                    SuppName = supplierData.Name,
                                    mauFactID = manufactData.ID,
                                    ManufactName = manufactData.ManufactName,

                                };
                if (getAllDic != null)
                {
                    return Json(getAllDic.ToList()); ;
                }
                else
                {

                    throw new Exception($"No Data Found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] ItemMaster itemMaster)
        {
            var itemM = new ItemMaster();
            try
            {
                dbContext.ItemMasters.Add(itemMaster);
                dbContext.SaveChanges();
                return Ok(itemM);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] ItemMaster itemMaster)
        {
            try
            {
                ItemMaster itemM = dbContext.ItemMasters.Find(itemMaster.ID);

                if (itemM != null)
                {
                    itemM.ItemGroup = itemMaster.ItemGroup;
                    itemM.ItemName = itemMaster.ItemName;
                    itemM.Unit = itemMaster.Unit;
                    itemM.MinimumStockLevel = itemMaster.MinimumStockLevel;
                    itemM.Supplier = itemMaster.Supplier;
                    itemM.Manufacturer = itemMaster.Manufacturer;
        

                //    dbContext.ItemMasters.Update(itemMaster);
                    dbContext.SaveChanges();

                    return Ok(itemM);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{itemM.ID}'.");
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
                ItemMaster itemM = dbContext.ItemMasters.Find(id);

                if (itemM != null)
                {
                    dbContext.ItemMasters.Remove(itemM);
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
