using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApLandingPage.ViewModels
{
    public class ApLandingPageUserAccountFoldersViewModel : AppViewModel
    {
        public ApLandingPageUserAccountFoldersViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {
        }
        public UserAccount UserAccount { get; set; }
        public List<ProgramMenu> Menus { get; set; }
    }
}
