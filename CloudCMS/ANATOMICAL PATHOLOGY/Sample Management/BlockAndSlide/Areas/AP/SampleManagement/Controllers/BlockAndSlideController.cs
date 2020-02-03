using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.SampleManagement.BlockAndSlide.Controllers
{
    [Area("ap")]
    [Folder("sample-management")]
    [Authorize]
    public class BlockAndSlideController : Controller
    {
        [Route("[area]/[folder]/block-and-slide")]
        [Route("[area]/[folder]/block-and-slide/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}