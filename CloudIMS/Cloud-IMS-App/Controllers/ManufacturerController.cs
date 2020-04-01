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
    public class ManufacturerController : AppController
    {
        

        public ManufacturerController(AppDbContext dbContext, ILogger<SupplierController> logger)
             : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }


        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Manufacturer> Index()
        {
            var manufacturers = dbContext.Manufacturers.ToList();
            return manufacturers;
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] Manufacturer manufacturer)
        {
            var manufacturers = new Manufacturer();
            try
            {

                dbContext.Manufacturers.Add(manufacturer);
                dbContext.SaveChanges();
                return Json(manufacturers);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] Manufacturer manufact)
        {
            try
            {
                Manufacturer manufacturer = dbContext.Manufacturers.Find(manufact.ID);

                if (manufacturer != null)
                {
                    manufacturer.ID = manufact.ID;
                    manufacturer.ManufactName = manufact.ManufactName;

                    dbContext.Manufacturers.Update(manufacturer);
                    dbContext.SaveChanges();

                    return Json(manufact);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{manufact.ID}'.");
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
                Manufacturer manufact = dbContext.Manufacturers.Find(id);
                if (manufact != null)
                {

                    dbContext.Manufacturers.Remove(manufact);
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
