using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudImsCommon.Extensions;
using CloudImsCommon.Database;
using Microsoft.Extensions.Logging;

namespace CloudIms.Areas.Home.Controllers
{
    [Area("home")]
    [Folder("home")]

    [Authorize]
    public class MainController : AppController    {

        public MainController(AppDbContext dbContext, ILogger<MainController> logger)
            : base(dbContext, logger)
        {
        }

        [ActionName("landing-page")]
        public IActionResult LandingPage()
        {
            return View("LandingPage");
        }
    }
}