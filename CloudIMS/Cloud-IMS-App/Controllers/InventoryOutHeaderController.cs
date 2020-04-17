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
    }
   
}
