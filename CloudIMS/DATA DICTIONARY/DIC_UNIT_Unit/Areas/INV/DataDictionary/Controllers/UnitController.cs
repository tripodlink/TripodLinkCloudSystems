using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudIms.Areas.DataDictionary.Controllers
{
    [Area("inv")]
    [Folder("data-dictionary")]
    [Authorize]
    public class UnitController : AppController
    {
        public UnitController(AppDbContext dbContext, ILogger<UnitController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/unit")]
        [Route("[area]/[folder]/unit/index")]
        public IActionResult Index()
        {
            return View("");
        }

    }
}