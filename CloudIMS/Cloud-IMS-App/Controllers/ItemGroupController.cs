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
    public class ItemGroupController : AppController
    {   
        public ItemGroupController(AppDbContext dbContext, ILogger<ItemGroupController>logger)
            :base(dbContext,logger)
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
        [HttpPost]
        public IActionResult Add([FromBody] ItemGroup itemg)
        {
            var itemgroup = new ItemGroup();
            try
            {
                dbContext.ItemGroups.Add(itemg);
                dbContext.SaveChanges();
                return Ok(itemgroup);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] ItemGroup itemg)
        {
            try
            {
                ItemGroup itemGroup = dbContext.ItemGroups.Find(itemg.ID);

                if (itemGroup != null)
                {
                    itemGroup.ID = itemg.ID;
                    itemGroup.ItemGroupName = itemg.ItemGroupName;

                    dbContext.ItemGroups.Update(itemGroup);
                    dbContext.SaveChanges();

                    return Ok(itemg);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{itemg.ID}'.");
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
                ItemGroup itemg = dbContext.ItemGroups.Find(id);

                if (itemg != null)
                {
                    dbContext.ItemGroups.Remove(itemg);
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
