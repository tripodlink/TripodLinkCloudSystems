using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.ResultManagement.ApResultEntry.Controllers
{
    [Area("ap")]
    [Folder("result-management")]
    [Authorize]
    public class ApResultEntryController : Controller
    {
        [Route("[area]/[folder]/ap-result-entry")]
        [Route("[area]/[folder]/ap-result-entry/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}