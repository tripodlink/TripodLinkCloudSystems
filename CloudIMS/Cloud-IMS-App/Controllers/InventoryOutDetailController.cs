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
            var inventOutDetial = dbContext.InventoryOutTrxDetails.ToList();
            return inventOutDetial;
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
    }
   
}
