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
    public class SupplierController : ControllerBase
    {
        private AppDbContext dbContext;

        public SupplierController(AppDbContext dbContext)
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
    }
}
