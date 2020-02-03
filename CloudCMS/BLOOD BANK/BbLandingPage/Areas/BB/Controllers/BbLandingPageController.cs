using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodBank.Controllers
{
    [Area("bb")]
    [Authorize]
    public class BbLandingPageController : Controller
    {
        [Route("[area]/")]
        [Route("[area]/bb-landing-page")]
        [Route("[area]/bb-landing-page/index")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("[area]/bb-landing-page/testing")]
        public IActionResult Testing()
        {
            return View();
        }
    }
}