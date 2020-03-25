using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{

    [Route("api/[controller]")]   
    public class UnitCodeController : Controller
    {   
        private AppDbContext dbContext;

        public UnitCodeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<UnitCode> Index()
        {
            var unitcodes = dbContext.UnitCodes.ToList();
            return unitcodes;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] UnitCode unit)
        {
            var unitcode = new UnitCode();
            try
            {
               
                dbContext.UnitCodes.Add(unit);
                dbContext.SaveChanges();
                return Ok(unitcode);
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.InnerException != null) {
                    message += "\r\n Message: " + ex.InnerException.Message;
                }

                return BadRequest(message);
            }
        }
    }
   
}
