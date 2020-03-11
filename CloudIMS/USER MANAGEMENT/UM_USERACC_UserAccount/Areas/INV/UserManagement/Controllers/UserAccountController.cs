using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using CloudImsCommon.Routing;
using DataDictionary.Unit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudIms.Areas.UserAccount.Controllers
{
    [Area("inv")]
    [Folder("user-management")]
    [Authorize]
    public class UserAccountController : AppController
    {
        public UserAccountController(AppDbContext dbContext, ILogger<UserAccountController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/user-account")]
        [Route("[area]/[folder]/user-account/index")]
        public IActionResult Index()
        {


            var model = new UserAccountViewModel(HttpContext);
            model.ProgramMenus = DbContext.ProgramMenus.ToList();

            return View("index", model);
        }

    }
}