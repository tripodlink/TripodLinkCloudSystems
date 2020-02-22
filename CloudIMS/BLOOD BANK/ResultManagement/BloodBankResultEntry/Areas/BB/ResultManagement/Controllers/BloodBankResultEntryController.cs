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

namespace BloodBank.ResultManagement.ResultEntry.Controllers
{
    [Area("bb")]
    [Folder("result-management")]
    [Authorize]
    public class BloodBankResultEntryController : AppController
    {
        public BloodBankResultEntryController(AppDbContext dbContext, ILogger<BloodBankResultEntryController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/result-entry")]
        [Route("[area]/[folder]/result-entry/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}