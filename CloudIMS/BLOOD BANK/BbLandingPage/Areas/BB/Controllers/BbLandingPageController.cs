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

namespace BloodBank.Controllers
{
    [Area("bb")]
    [Authorize]
    public class BbLandingPageController : AppController
    {
        public BbLandingPageController(AppDbContext dbContext, ILogger<BbLandingPageController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/")]
        [Route("[area]/bb-landing-page")]
        [Route("[area]/bb-landing-page/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}