using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudImsCommon.Database;

namespace CloudImsCommon.Extensions
{
    public class AppTenantController : Controller
    {
        public AppDbContext CreateDbContext(String companyID)
        {
            return AppDbContext.CreateInstance(companyID);
        }


        public AppDbContext CreateDbContext()
        {
            try
            {
                if (IsAuthenticated())
                {
                    var claims = HttpContext.User.Claims.ToList();
                    foreach (var claim in claims)
                    {
                        if (claim.Type == "CompanyID")
                        {
                            return AppDbContext.CreateInstance(claim.Value);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            return null;
        }

        public bool IsAuthenticated() {
            try
            {
                var identity = HttpContext.User.Identity;

                return identity.IsAuthenticated;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}