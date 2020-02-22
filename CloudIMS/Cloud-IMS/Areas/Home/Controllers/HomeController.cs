using CloudIms.Areas.Home.Models;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace CloudIms.Areas.Home.Controllers
{
    [Area("home")]
    [Folder("home")]
    public class HomeController : AppController
    {
        public string ReturnUrl { get; private set; }

        public HomeController(AppDbContext dbContext , ILogger<HomeController> logger)
            :base(dbContext, logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// The login page.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult Login(String returnUrl = null)
        {
            if (IsAuthenticated())
            {
                return RedirectToAction("landing-page", "Main", new {Area = "home", Folder = "home"});
            }
            else
            {
                return View();
            }
        }



        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(String userID, String userPassword, String moduleID)
        {

            try
            {
                UserAccount user = DbContext.UserAccounts.Find(userID);

                if (user != null)
                {
                    if (user.Password == userPassword)
                    {
                        List<ProgramMenu> menus = DbContext.ProgramMenus.Where(p => p.ProgramFolderID == "IVM").ToList();

                        var menuStr = "";
                        foreach (var menu in menus)
                        {
                            if (menuStr == "")
                            {
                                menuStr = "id^" + menu.ID + "|" + "name^" + menu.Name + "|";
                            }
                            else
                            {
                                menuStr += "~id^" + menu.ID + "|" + "name^" + menu.Name + "|";
                            }
                        }

                        var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name, userID),
                                new Claim("UserID", userID),
                                new Claim("UUID", Guid.NewGuid().ToString()),
                                new Claim("SideBarMenu", menuStr),
                            };

                        var identity = new ClaimsIdentity(claims, "CloudIms");
                        var principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal,
                            new AuthenticationProperties()
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                                AllowRefresh = true
                            }
                            ); ;

                        if (moduleID==null)
                        {
                            return RedirectToAction("landing-page", "main", new { Area = "home" });
                        }

                        return RedirectToAction("", "", new { Area = moduleID });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("InvalidLogin");
        }

        [AllowAnonymous]
        public IActionResult Logout() {
          HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Home");
        }



        /// <summary>
        /// Return page when user login fails.
        /// </summary>
        /// <returns></returns>
        public IActionResult InvalidLogin() {
            return View();
        }      


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}