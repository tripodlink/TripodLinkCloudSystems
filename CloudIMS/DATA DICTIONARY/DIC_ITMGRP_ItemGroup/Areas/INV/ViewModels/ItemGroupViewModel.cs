using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDictionary.Unit.ViewModels
{
    public class ItemGroupViewModel : AppViewModel
    {
        public ItemGroupViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {

        }

        public List<ProgramMenu> ProgramMenus { get; set; } = new List<ProgramMenu>();

        public List<ItemGroup> ItemGroups { get; set; } = new List<ItemGroup>();
    }
}
