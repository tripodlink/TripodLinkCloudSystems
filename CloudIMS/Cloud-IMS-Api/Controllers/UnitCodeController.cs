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
    [ApiController]
    [Route("[controller]")]
    public class UnitCodeController : ControllerBase
    {
        private AppDbContext dbContext;

        public UnitCodeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<UnitCode> Index()
        {
            var unitcodes = dbContext.UnitCodes.ToList();
            return unitcodes;
        }
    }
}
