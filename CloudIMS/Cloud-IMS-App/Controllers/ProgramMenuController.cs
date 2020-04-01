using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramMenuController : AppController
    {
        public ProgramMenuController(AppDbContext dbContext, ILogger<ProgramMenuController> logger)
            : base(dbContext, logger)
        {
        }


        [HttpGet]
        [Route("get-program-by-id")]
        public IActionResult FindProgramById(string id)
        {
            try
            {
                ProgramMenu pm = dbContext.ProgramMenus.Find(id);

                if (pm != null)
                {
                    return Ok(pm);
                }
                else
                {
                    throw new Exception($"Program menu with id of '{ id }' is not found.");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [HttpGet]
        [Route("get-all-programs")]
        public IActionResult GetAllPrograms()
        {
            try
            {
                return Ok(dbContext.ProgramMenus.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [HttpGet]
        [Route("get-all-programs-by-folder-id")]
        public IActionResult GetAllProgramsByFolderId(string folder_id)
        {
            try
            {
                return Ok ( dbContext.ProgramMenus.Where(pm => pm.ProgramFolderID == folder_id));
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [HttpGet]
        [Route("get-program-folder-by-id")]
        public IActionResult GetProgramFolderById(string folder_id)
        {
            try
            {
                ProgramFolder pf = dbContext.ProgramFolders.Find(folder_id);

                if (pf != null) {
                    return Ok(pf);
                }
                else
                {
                    throw new Exception($"Program folder with id of '{ folder_id }' is not found.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("get-all-program-folders")]
        public IActionResult GetAllProgramFolders()
        {
            try
            {
                return Ok(dbContext.ProgramFolders.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [HttpGet]
        [Route("get-all-program-folders-with-menus")]
        public IActionResult GetAllProgramFoldersWithMenus()
        {
            try
            {
                var pfList = dbContext.ProgramFolders.ToList();

                foreach (ProgramFolder  pf in pfList) {
                    pf.ProgramMenus = dbContext.ProgramMenus.Where(pm => pm.ProgramFolderID == pf.ID).ToList();
                }

                return Ok(pfList);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

    }
}