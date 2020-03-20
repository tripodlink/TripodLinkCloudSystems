using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataDictionary.Unit.ViewModels
{
    public class ManufacturerViewModel : AppViewModel
    {
        public ManufacturerViewModel(HttpContext httpcontext)
            :base(httpcontext)
        {

        }

        public List<Manufacturer> manufacturers { get; set; } = new List<Manufacturer>();
    }
}
