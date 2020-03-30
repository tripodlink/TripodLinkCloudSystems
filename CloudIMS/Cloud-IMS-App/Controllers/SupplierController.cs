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
    public class SupplierController : AppController
    {
        

        public SupplierController(AppDbContext dbContext, ILogger<SupplierController> logger)
             : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }


        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Supplier> Index()
        {
            var supplier = dbContext.Suppliers.ToList();
            return supplier;
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] Supplier supplier)
        {
            var suppliers = new Supplier();
            try
            {

                dbContext.Suppliers.Add(supplier);
                dbContext.SaveChanges();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                var message = ex.Message;

                if (ex.InnerException != null)
                {
                    message += "\r\n Message: " + ex.InnerException.Message;
                }

                return BadRequest(message);
            }
        }

        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] Supplier supId)
        {
            try
            {
                Supplier supplier = dbContext.Suppliers.Find(supId.ID);

                if (supplier != null)
                {
                    supplier.ID = supId.ID;
                    supplier.Name = supId.Name;

                    dbContext.Suppliers.Update(supplier);
                    dbContext.SaveChanges();

                    return Ok(supId);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{supId.ID}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [Route("[action]")]
        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(string id)
        {
            Supplier supplier = dbContext.Suppliers.Find(id);
            if (supplier == null)
            {
                return NotFound();
            }

            dbContext.Suppliers.Remove(supplier);
            dbContext.SaveChanges();

            return Ok(id);
        }
    }
}
