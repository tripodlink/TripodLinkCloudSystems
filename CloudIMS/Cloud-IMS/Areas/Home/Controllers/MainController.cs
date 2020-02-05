using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudImsCommon.Extensions;

namespace CloudCms.Areas.Home.Controllers
{
    [Area("home")]
    [Folder("home")]

    [Authorize]
    public class MainController : AppTenantController    {

        [ActionName("landing-page")]
        public IActionResult LandingPage()
        {
            return View("LandingPage");
        }
    }
}