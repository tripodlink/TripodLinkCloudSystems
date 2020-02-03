using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleDatabaseIdentityAuthentication.Data;

namespace SampleDatabaseIdentityAuthentication.Controllers
{
    public class AppController : Controller
    {
        public AppDbContext CreateDbContext(String companyID) {
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

        private bool IsAuthenticated()
        {
            var identity = HttpContext.User.Identity;

            return identity.IsAuthenticated;
        }
    }
}