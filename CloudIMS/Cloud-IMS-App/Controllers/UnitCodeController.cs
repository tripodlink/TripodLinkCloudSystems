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
    public class UnitCodeController : AppController
    {   
      
        public UnitCodeController(AppDbContext dbContext, ILogger<UnitCodeController>logger)
            :base(dbContext,logger)
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
                return BadRequest(GetErrorMessage(ex));
            }
         }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] UnitCode unitCode)
        {
            try
            {
                UnitCode unitC = dbContext.UnitCodes.Find(unitCode.Code);

                if (unitC != null)
                {
                    unitC.Code = unitCode.Code;
                    unitC.Description = unitCode.Description;
                    unitC.ShortDescription = unitCode.ShortDescription;
                 

                    dbContext.UnitCodes.Update(unitC);
                    dbContext.SaveChanges();

                    return Ok(unitC);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{unitC.Code}'.");
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
                UnitCode unitC = dbContext.UnitCodes.Find(id);

                if (unitC != null)
                {
                    dbContext.UnitCodes.Remove(unitC);
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
