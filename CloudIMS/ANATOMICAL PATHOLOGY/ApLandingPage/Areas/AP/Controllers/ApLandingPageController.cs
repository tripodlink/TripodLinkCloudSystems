using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CloudCmsCommon.Extensions;
using CloudCmsCommon.Models;
using CloudCmsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AnatomicalPathology.Controllers
{
    [Area("ap")]
    [Route("[area]/ap-landing-page")]
    [Route("[area]/")]
    [Authorize]
    public class ApLandingPageController : AppTenantController
    {
        public IActionResult Index()
        {
            IEnumerable<Claim> claims = HttpContext.User.Claims;
            String user_id = claims.First(x => x.Type == "UserID").Value;
            String company_id = claims.First(x => x.Type == "CompanyID").Value;

            var dbcontext = CreateDbContext(company_id);
            UserAccount user = dbcontext.UserAccounts.Find(user_id);

            

            //var x = from folders in dbcontext.ProgramFolders
                     
            return View();
        }

    }
}