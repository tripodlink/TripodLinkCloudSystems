using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleDatabaseIdentityAuthentication.Data;
using SampleDatabaseIdentityAuthentication.Models;

namespace SampleDatabaseIdentityAuthentication.Controllers
{
    public class HomeController : AppController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult LandingPage()
        {
            return View();
        }

        public IActionResult Login()
        {
            if (IsAuthenticated()) { 
                return RedirectToAction("LandingPage");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public IActionResult Login(String companyid, String userid, String password)
        {

            try
            {
                //AppDbContext dbcontext = AppDbContext.CreateInstance(companyid);

                AppDbContext dbcontext = CreateDbContext(companyid);

                UserAccount user = dbcontext.UserAccounts.Find(userid);

                if (user != null)
                {
                    if (user.Password == password)
                    {
                        var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name, userid),
                                new Claim("CompanyID", companyid),
                                new Claim("UUID", Guid.NewGuid().ToString()),
                            };

                        var identity = new ClaimsIdentity(claims, "CloudCms");
                        //var identity = new ClaimsIdentity(claims);
                        var principal = new ClaimsPrincipal(identity);

                        HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            principal,
                            new AuthenticationProperties()
                            {
                                IsPersistent = true,
                                ExpiresUtc = DateTimeOffset.Now.AddMinutes(2),
                                AllowRefresh = true
                            }
                            ); ;

                        return RedirectToAction("LandingPage");
                    }
                    else
                    {
                        return RedirectToAction("InvalidLogin");
                    }
                }
                else
                {
                    return RedirectToAction("InvalidLogin");
                }

            }
            catch (Exception ex)
            {
                return RedirectToAction("InvalidLogin");
            }
        }

        private bool IsAuthenticated() {

            var identity = HttpContext.User.Identity;
            
            return identity.IsAuthenticated;
        }

        public IActionResult InvalidLogin()
        {
            return View();
        }

        [Authorize]
        public IActionResult Register()
        {
            var identity = HttpContext.User.Identity;

            if (identity != null)
            {
               var claims = HttpContext.User.Claims.ToList();
                foreach (var claim in claims)
                {
                    if (claim.Type == "CompanyID")
                    {
                        ViewData["CompanyID"] = claim.Value;

                        break;
                    }
                }
                
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(String companyid, String userid, String password)
        {
            //AppDbContext dbcontext = AppDbContext.CreateInstance(companyid);
            AppDbContext dbcontext = CreateDbContext();
            UserAccount user = new UserAccount() {
                UserID = userid,
                UserName = userid,
                Password = password
            };

            dbcontext.Add(user);
            dbcontext.SaveChanges();

            return View("Register");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> LogOut()
        {
           await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
