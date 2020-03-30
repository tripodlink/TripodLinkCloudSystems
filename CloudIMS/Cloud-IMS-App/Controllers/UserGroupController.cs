using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Cloud_IMS_App.Controllers
{
    [Route("api/[controller]")]
    public class UserGroupController: AppController
    {

        public UserGroupController(AppDbContext dbContext, ILogger<UserGroupController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            try
            {
                return Ok(dbContext.UserGroups.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("[action]")]
        public IActionResult Find(string id)
        {
            try
            {
                return Ok(dbContext.UserGroups.Find(id));
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [Route("[action]")]
        public IActionResult FindUserGroupsByUserId(string id)
        {
            try
            {
                var groupIds = dbContext.UserAccountGroups.Where(uag => uag.UserAccountID == id).Select(uag => new { GroupID = uag.UserGroupID });
                return Ok(dbContext.UserGroups.Where(ug => groupIds.Any(g => g.GroupID == ug.ID)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
}
