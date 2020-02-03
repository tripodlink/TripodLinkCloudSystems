using CloudImsCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudImsCommon.Extensions 
{
    public class AppTenant
    {
        public string CompanyID { get; set; }
        public Boolean IsMigrationCompleted { get; set; } = false;
    }
}
