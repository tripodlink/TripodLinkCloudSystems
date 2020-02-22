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

namespace AnatomicalPathology.SampleManagement.BlockAndSlide.Controllers
{
    [Area("ap")]
    [Folder("sample-management")]
    [Authorize]
    public class BlockAndSlideController : AppController
    {
        public BlockAndSlideController(AppDbContext dbContext, ILogger<BlockAndSlideController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/block-and-slide")]
        [Route("[area]/[folder]/block-and-slide/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}