﻿using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Routing
{
    public class FolderAttribute : RouteValueAttribute
    {
        public FolderAttribute(string regionName) : base("Folder", regionName)
        {
        }
    }
}
