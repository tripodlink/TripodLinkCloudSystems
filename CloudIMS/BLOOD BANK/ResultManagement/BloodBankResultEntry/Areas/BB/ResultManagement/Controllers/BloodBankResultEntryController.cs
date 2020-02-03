using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodBank.ResultManagement.ResultEntry.Controllers
{
    [Area("bb")]
    [Folder("result-management")]
    [Authorize]
    public class BloodBankResultEntryController : Controller
    {
        [Route("[area]/[folder]/result-entry")]
        [Route("[area]/[folder]/result-entry/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}