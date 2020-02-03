using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SampleCookieAuthentication.Models;

namespace SampleCookieAuthentication.Controllers
{
    public class HomeController : Controller
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

        public IActionResult Authenticate()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Florante"),
                new Claim(ClaimTypes.Email, "reguis.florante@gmail.com"),
                new Claim("CompanyID", "TGSC"),
                new Claim("UUID", Guid.NewGuid().ToString()),
            };

            var identity = new ClaimsIdentity(claims, "CloudCms");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.SignInAsync(principal);

            return RedirectToAction("Index");
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index");
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
