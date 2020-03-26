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
    public class HomeController : ControllerBase
    {
        private AppDbContext dbContext;

        public HomeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


       
        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<ProgramMenu> Index()
        {

            var groupIdArr = dbContext.UserAccountGroups.Where(u => u.UserAccountID == "SYSAD")
                            .Select(u => new { UserGroupID = u.UserGroupID })
                            .ToList();

            var userProgramsArr = dbContext.UserGroupPrograms
                                              .Where(ugp => groupIdArr.Any(gi => gi.UserGroupID == ugp.UserGroupID))
                                              .Select(ugp => new { ProgramID = ugp.ProgramMenuID })
                                              .ToList();

            List<ProgramMenu> menus = dbContext.ProgramMenus
                                                   .Where(pm => userProgramsArr.Any(up => up.ProgramID == pm.ID))
                                                   .ToList();

            List<ProgramFolder> folders = dbContext.ProgramFolders
                                                    .Where(pf => menus.Any(pm => pm.ProgramFolderID == pf.ID))
                                                    .ToList();

            var pm_Menu = dbContext.ProgramMenus.ToList();
            return pm_Menu;
        }
    }
}
