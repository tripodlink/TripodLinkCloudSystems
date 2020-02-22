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

namespace AnatomicalPathology.ResultManagement.PapSmearResultEntry.Controllers
{
    [Area("ap")]
    [Folder("result-management")]
    [Authorize]
    public class PapSmearResultEntryController : AppController
    {
        public PapSmearResultEntryController(AppDbContext dbContext, ILogger<PapSmearResultEntryController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/pap-smear-result-entry")]
        [Route("[area]/[folder]/pap-smear-result-entry/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}