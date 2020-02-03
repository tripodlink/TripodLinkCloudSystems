using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.ResultManagement.PapSmearResultEntry.Controllers
{
    [Area("ap")]
    [Folder("result-management")]
    [Authorize]
    public class PapSmearResultEntryController : Controller
    {
        [Route("[area]/[folder]/pap-smear-result-entry")]
        [Route("[area]/[folder]/pap-smear-result-entry/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}