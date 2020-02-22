using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudImsCommon.Models;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using ApLandingPage.ViewModels;

namespace AnatomicalPathology.Controllers
{
    [Area("ap")]
    [Authorize]
    public class ApLandingPageController : AppController
    {
        public ApLandingPageController(AppDbContext dbContext, ILogger<ApLandingPageController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/")]
        [Route("[area]/ap-landing-page")]
        [Route("[area]/ap-landing-page/index")]
        public IActionResult Index()
        {
            IEnumerable<Claim> claims = HttpContext.User.Claims;
            String user_id = claims.First(x => x.Type == "UserID").Value;

            UserAccount user = DbContext.UserAccounts.Find(user_id);

            List<ProgramMenu> menus = DbContext.ProgramMenus.Where(p => p.ModuleRouteAttribute == "ap").ToList();
                      
            var model = new ApLandingPageUserAccountFoldersViewModel(HttpContext);
            model.UserAccount = user;
            model.Menus = menus;

            return View("index", model);
        }

    }
}