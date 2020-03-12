using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDictionary.Unit.ViewModels
{
    public class ItemMasterViewModel : AppViewModel
    {
        public ItemMasterViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {

        }

        public List<ProgramMenu> ProgramMenus { get; set; } = new List<ProgramMenu>();

        public List<ItemMaster> ItemMasters { get; set; } = new List<ItemMaster>();

        public List<ItemGroup> ItemGroups { get; set; } = new List<ItemGroup>();
    }
}
