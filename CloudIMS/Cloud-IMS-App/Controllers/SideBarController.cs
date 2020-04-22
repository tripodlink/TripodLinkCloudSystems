using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{
   
    [Route("api/[controller]")]
    public class SideBarController : ControllerBase
    {
        private AppDbContext dbContext;

        public SideBarController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


       
        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult Index(string id)
        {

            //UserAccount user = dbContext.UserAccounts.Find(id);

            //var userGroupIdList = dbContext.UserAccountGroups
            //    .Where(uag => uag.UserAccountID == id)
            //    .Select(uag => new { GoupID = uag.UserGroupID })
            //    .ToList();

            //user.UserGroups = dbContext.UserGroups
            //    .Where(ug => userGroupIdList.Any(ugL => ugL.GoupID == ug.ID))
            //    .ToList();

            //var programIdList = dbContext.UserGroupPrograms
            //    .Where(ugp => user.UserGroups.Any(ug => ug.ID == ugp.UserGroupID))
            //    .Distinct()
            //    .Select(ugp => new { ProgramID = ugp.ProgramMenuID })
            //    .ToList();

            //user.ProgramMenus = dbContext.ProgramMenus
            //    .Where(pm => programIdList.Any(pmL => pmL.ProgramID == pm.ID))
            //    .ToList();

            //user.ProgramFolders = dbContext.ProgramFolders
            //    .Where(pf => user.ProgramMenus.Any(pm => pm.ProgramFolderID == pf.ID))
            //    .ToList();

            return Ok("");

        }
    }
}
