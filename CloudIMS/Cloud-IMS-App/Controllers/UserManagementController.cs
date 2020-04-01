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
    public class UserManagementController : ControllerBase
    {
        private AppDbContext dbContext;

        public UserManagementController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
       
        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<ProgramMenu> Index()
        {
            
            var pm_Menu = dbContext.ProgramMenus.Where(pm => pm.ProgramFolderID == "UM").ToList();
            return pm_Menu;
        }
    }
}
