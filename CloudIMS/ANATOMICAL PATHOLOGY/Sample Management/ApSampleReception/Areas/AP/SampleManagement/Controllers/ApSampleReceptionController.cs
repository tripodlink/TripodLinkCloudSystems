using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.SampleManagement.ApSampleReception.Controllers
{
    [Area("ap")]
    [Folder("sample-management")]
    [Authorize]
    public class ApSampleReceptionController : Controller
    {
        [Route("[area]/[folder]/sample-reception")]
        [Route("[area]/[folder]/sample-reception/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}