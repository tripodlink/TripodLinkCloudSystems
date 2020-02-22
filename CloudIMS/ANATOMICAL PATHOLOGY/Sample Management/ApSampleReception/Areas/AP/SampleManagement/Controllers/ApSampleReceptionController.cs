using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApSampleReception.ViewModels;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.SampleManagement.ApSampleReception.Controllers
{
    [Area("ap")]
    [Folder("sample-management")]
    [Authorize]
    public class ApSampleReceptionController : AppController
    {
        public ApSampleReceptionController(AppDbContext dbContext, ILogger<ApSampleReceptionController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/sample-reception")]
        [Route("[area]/[folder]/sample-reception/index")]
        public IActionResult Index()
        {
            var model = new  ApSampleReceptionViewModel(HttpContext);

            return View("index", model);
        }

    }
}