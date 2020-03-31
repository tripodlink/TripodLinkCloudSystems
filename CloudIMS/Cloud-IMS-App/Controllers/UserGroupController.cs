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

        [Route("get-user-group-with-folders-and-menus")]
        public IActionResult GetUserGroupWithFoldersAndMenus(string id)
        {
            try
            {
                UserGroup ug = dbContext.UserGroups.Find(id);

                var pmIds = dbContext.UserGroupPrograms.Where(ugp => ugp.UserGroupID == id).Select(ugp => new { ID = ugp.ProgramMenuID }).ToList();

                ug.ProgramMenus = dbContext.ProgramMenus.Where(pm => pmIds.Any(x => x.ID == pm.ID)).ToList();
                ug.ProgramFolders = dbContext.ProgramFolders.Where(pf => ug.ProgramMenus.Any(x => x.ProgramFolderID == pf.ID) ).ToList();

                return Ok(ug);
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

        [Route("[action]")]
        public IActionResult Add([FromBody] UserGroup ug)
        {
            using(var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    UserGroup ugToAdd = new UserGroup();
                    ugToAdd.ID = ug.ID;
                    ugToAdd.Name = ug.Name;

                    dbContext.UserGroups.Add(ugToAdd);
                    dbContext.SaveChanges();

                    foreach (ProgramMenu pm in ug.ProgramMenus)
                    {
                        UserGroupProgram ugp = new UserGroupProgram();
                        ugp.UserGroupID = ug.ID;
                        ugp.ProgramMenuID = pm.ID;

                        dbContext.UserGroupPrograms.Add(ugp);
                        dbContext.SaveChanges();
                    }

                    transaction.Commit();
                    return Ok(ug);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return BadRequest(GetErrorMessage(ex));
                }
            }
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] UserGroup ug)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    UserGroup userToSave = dbContext.UserGroups.Find(ug.ID);

                    if (userToSave != null)
                    {
                        userToSave.Name = ug.Name;

                        dbContext.SaveChanges();

                        var ugpsToDelete = dbContext.UserGroupPrograms.Where(
                            ugp => ugp.UserGroupID == ug.ID && 
                            !ug.ProgramMenus.Any(pm => pm.ID == ugp.ProgramMenuID))
                            .ToList();

                        dbContext.UserGroupPrograms.RemoveRange(ugpsToDelete);
                        dbContext.SaveChanges();

                        foreach (var pm in ug.ProgramMenus)
                        {
                            var count = dbContext.UserGroupPrograms.Where(ugp => ugp.UserGroupID == ug.ID && ugp.ProgramMenuID == pm.ID).Count();
                            if (count == 0)
                            {
                                var ugp = new UserGroupProgram() { UserGroupID = ug.ID, ProgramMenuID = pm.ID };
                                dbContext.UserGroupPrograms.Add(ugp);
                                dbContext.SaveChanges();
                            }
                        }

                        transaction.Commit();

                        return Ok(ug);
                    }
                    else
                    {
                        throw new Exception($"Invalid user group ID '{ug.ID}'.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return BadRequest(GetErrorMessage(ex));
                }
                
            }
        }


        [Route("[action]")]
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    UserGroup ug = dbContext.UserGroups.Find(id);

                    if (ug != null)
                    {
                        dbContext.UserGroups.Remove(ug);
                        dbContext.SaveChanges();

                        var ugpsToDelete = dbContext.UserGroupPrograms.Where(
                            ugp => ugp.UserGroupID == id)
                            .ToList();

                        dbContext.UserGroupPrograms.RemoveRange(ugpsToDelete);
                        dbContext.SaveChanges();

                        transaction.Commit();

                        return Json(ug);
                    }
                    else
                    {
                        throw new Exception($"Invalid user group ID '{id}'.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return BadRequest(GetErrorMessage(ex));
                }
                
            }
        }
    }
}
