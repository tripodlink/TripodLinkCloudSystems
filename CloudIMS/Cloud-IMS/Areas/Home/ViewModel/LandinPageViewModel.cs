using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.LandingPage.ViewModels
{
    public class LandinPageViewModel : AppViewModel
{
    public LandinPageViewModel(HttpContext httpcontext)
            : base(httpcontext)
    {

    }
    public List<ProgramMenu> ProgramMenus { get; set; } = new List<ProgramMenu>();
    public List<ProgramFolder> ProgramFolders { get; set; } = new List<ProgramFolder>();
}
}
