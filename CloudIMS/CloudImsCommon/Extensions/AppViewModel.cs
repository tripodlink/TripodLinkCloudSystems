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
                    this.SideBarMenus = claim.Value;
                }

                if (claim.Type == "SideBarFolder")
                {
                    this.SideBarFolders = claim.Value;
                }
            }
        }

        public string SideBarMenus { get; }
        public string SideBarFolders { get; }
    }
}
