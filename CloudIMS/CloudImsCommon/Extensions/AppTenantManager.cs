using Microsoft.EntityFrameworkCore;
using CloudCmsCommon.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CloudCmsCommon.Extensions
{
    public class AppTenantManager
    {
        IEnumerable<AppTenant> tenants = new List<AppTenant>();

        /// <summary>
        /// Update the tenant's database if not yet updated.
        /// </summary>
        /// <param name="companyId">The company ID of the tenant or customer.</param>
        public void  UpdateTenantDatabase(String companyId)
        {
            if (IsValidCompanyId(companyId)) {

                AppTenant tenant = null;

                foreach (var t in tenants)
                {
                    if (t.CompanyID == companyId)
                    {
                        tenant = t;
                        break;
                    }
                }

                if (tenant == null)
                {
                    tenant = new AppTenant() { CompanyID = companyId };
                    this.tenants.Append(tenant);
                }

                if (tenant.IsMigrationCompleted==false)
                {
                    var dbcontext = AppDbContext.CreateInstance(companyId);
                    dbcontext.Database.Migrate();
                    tenant.IsMigrationCompleted = true;
                }
            }
        }
        
        public bool IsValidCompanyId (String companyId) { return true; }
    }
}
