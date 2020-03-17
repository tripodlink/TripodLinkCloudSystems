using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDictionary.Unit.ViewModels
{
    public class UnitViewModel : AppViewModel
    {
        public UnitViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {

        }

        public List<UnitCode> UnitCodes { get; set; } = new List<UnitCode>();
    }
}
