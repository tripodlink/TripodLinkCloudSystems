using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDictionary.Unit.ViewModels
{
    public class UserAccountViewModel : AppViewModel
    {
        public UserAccountViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {

        }

        public List<ProgramMenu> ProgramMenus { get; set; } = new List<ProgramMenu>();
    }
}
