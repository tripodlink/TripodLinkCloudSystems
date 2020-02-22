using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApSampleReception.ViewModels
{
    public class ApSampleReceptionViewModel : AppViewModel
    {
        public ApSampleReceptionViewModel(HttpContext httpcontext)
            : base(httpcontext)
        {
        }

        public List<ProgramMenu> ProgramMenus { get; set; }
    }
}
