using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudIms.Areas.UserAccount.Controllers
{
    [Route("api/[controller]")]
    public class UserAccountController : AppController
    {
        public UserAccountController(AppDbContext dbContext, ILogger<UserAccountController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            try
            {
               return Ok(dbContext.UserAccounts.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage (ex));
            }
        }

    }
}