using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Extensions
{
    public class AppViewModel
    {
        public AppViewModel(HttpContext httpcontext)
        {
            var claims = httpcontext.User.Claims;

            foreach (var claim in claims)
            {
                if (claim.Type == "SideBarMenu")
                {
                    this.SindeBarMenus = claim.Value;

                    break;
                }
            }
        }

        public string SindeBarMenus { get; }
    }
}
