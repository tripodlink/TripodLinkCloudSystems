using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudImsCommon.Database;
using CloudImsCommon.Models;
using CloudImsCommon.Extensions;

namespace CloudImsCommon.Debug.Controllers
{
    [Area("debug")]
    public class AutoLoginController : AppController 
    {
        public string ReturnUrl { get; private set; }

        public AutoLoginController(AppDbContext dbContext, ILogger<AutoLoginController> logger)
            : base(dbContext, logger)
        {
        }

        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]

        [Route("[area]/login")]
        public IActionResult Login(string returnUrl = null)
        {
            
            var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Name, "SYSAD"),
                                new Claim("UUID", Guid.NewGuid().ToString()),
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

            if (returnUrl!=null)
            {
                return Redirect(returnUrl);
            }

            return Redirect("/");
        }
    }
}