using Cloud_IMS_App.Controllers;
using CloudImsCommon.Authentication;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cloud_IMS_Api.Controllers
{
    [Route("api/[controller]")]
    public class UserAccountController : AppController
    {
        private readonly IAppAuthenticationService appAuthenticationService;
        public UserAccountController(AppDbContext dbContext, ILogger<UserAccountController> logger, IAppAuthenticationService appAuthenticationService)
            : base(dbContext, logger)
        {
            this.appAuthenticationService = appAuthenticationService;
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

        [Route("[action]")]
        public IActionResult Find(string id)
        {
            try
            { 
                //find the user
                UserAccount user = dbContext.UserAccounts.Find(id);

                if (user != null)
                {
                    var userGroupIdList = dbContext.UserAccountGroups
                    .Where(uag => uag.UserAccountID == id)
                    .Select(uag => new { GoupID = uag.UserGroupID })
                    .ToList();

                    //get user's user group list
                    user.UserGroups = dbContext.UserGroups
                        .Where(ug => userGroupIdList.Any(ugL => ugL.GoupID == ug.ID))
                        .ToList();

                    var programIdList = dbContext.UserGroupPrograms
                        .Where(ugp => user.UserGroups.Any(ug => ug.ID == ugp.UserGroupID))
                        .Distinct()
                        .Select(ugp => new { ProgramID = ugp.ProgramMenuID })
                        .ToList();

                    //get all the granted programs to the user
                    user.ProgramMenus = dbContext.ProgramMenus
                        .Where(pm => programIdList.Any(pmL => pmL.ProgramID == pm.ID))
                        .ToList();

                    //get all the granted program folders base on it's granted program menus
                    user.ProgramFolders = dbContext.ProgramFolders
                        .Where(pf => user.ProgramMenus.Any(pm => pm.ProgramFolderID == pf.ID))
                        .ToList();
                }                

                //user is null if not found
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [Route("[action]")]
        public IActionResult FindByIdOrName(string search_key)
        {
            try
            {
                var search_id = search_key.ToUpper();
                var search_name = search_key.ToUpper().Replace("*", "%");

                if (!search_key.StartsWith("%"))
                {
                    search_name = "%" + search_name;
                }

                if (!search_key.EndsWith("%"))
                {
                    search_name = search_name + "%";
                }

                return Ok(dbContext.UserAccounts
                    .Where(u => EF.Functions.Like(u.UserID, search_id) || EF.Functions.Like(u.UserName, search_name))
                    .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }



        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] UserAccount user)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    UserAccount userToAdd = new UserAccount()
                    {
                        UserID = user.UserID,
                        UserName = user.UserName,
                        Password = user.Password,
                        IsActive = user.IsActive,
                        CreatedOn = DateTime.Now,
                        CreatedBy = user.CreatedBy,
                        UpdatedOn = DateTime.Now,
                        UpdatedBy = user.UpdatedBy
                    };

                    dbContext.UserAccounts.Add(userToAdd);
                    dbContext.SaveChanges();

                    foreach (UserGroup ug in user.UserGroups)
                    {
                        int count = dbContext.UserAccountGroups.Where(x => x.UserAccountID == user.UserID && x.UserGroupID == ug.ID).Count();

                        if (count == 0)
                        {
                            UserAccountGroup uag = new UserAccountGroup();
                            uag.UserAccountID = user.UserID;
                            uag.UserGroupID = ug.ID;

                            dbContext.UserAccountGroups.Add(uag);
                            dbContext.SaveChanges();
                        }
                    }

                    transaction.Commit();

                    return Ok(user);
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
        public IActionResult Update([FromBody] UserAccount user)
        {

            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    UserAccount userForUpdate = dbContext.UserAccounts.Find(user.UserID);

                    if (userForUpdate != null)
                    {
                        userForUpdate.UserName = user.UserName;
                        userForUpdate.Password = user.Password;
                        userForUpdate.IsActive = user.IsActive;
                        userForUpdate.UpdatedBy = user.UpdatedBy;
                        userForUpdate.UpdatedOn = DateTime.Now;

                        dbContext.SaveChanges();

                        
                       List<UserAccountGroup> uagToDelete = dbContext.UserAccountGroups
                            .Where(uag => uag.UserAccountID == user.UserID && !user.UserGroups.Any(ug => ug.ID == uag.UserGroupID))
                            .ToList();

                        if (uagToDelete != null && uagToDelete.Count > 0)
                        {
                            dbContext.UserAccountGroups.RemoveRange(uagToDelete);
                            dbContext.SaveChanges();
                        }

                        foreach (UserGroup ug in user.UserGroups)
                        {
                            int count = dbContext.UserAccountGroups.Where(x => x.UserAccountID == user.UserID && x.UserGroupID == ug.ID).Count();

                            if(count == 0) { 
                                UserAccountGroup uag = new UserAccountGroup();
                                uag.UserAccountID = user.UserID;
                                uag.UserGroupID = ug.ID;

                                dbContext.UserAccountGroups.Add(uag);
                                dbContext.SaveChanges();
                            }
                        }

                        transaction.Commit();

                        return Ok(user);
                    }
                    else
                    {
                        throw new Exception($"User Not found with a user ID of '{user.UserID}'.");
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
                    UserAccount userToDelete = dbContext.UserAccounts.Find(id);

                    if (userToDelete != null)
                    {
                        dbContext.UserAccounts.Remove(userToDelete);

                        var userAccountGroups = dbContext.UserAccountGroups.Where(uag => uag.UserAccountID == id).ToList();
                        dbContext.UserAccountGroups.RemoveRange(userAccountGroups);

                        dbContext.SaveChanges();

                        transaction.Rollback();

                        return Json(id);
                    }
                    else
                    {
                        throw new Exception($"User Not found with a user ID of '{id}'.");
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return BadRequest(GetErrorMessage(ex));
                }
            }                
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserAccount model)
        {
            var user = this.appAuthenticationService.Authenticate(model.UserID, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Incorrect user ID or password." });
            }
            else if(! user.IsActive)
            {
                return BadRequest(new { message = "User is not active." });
            }

            return Ok(user);
        }


        [AllowAnonymous]
        [HttpPost("validate-user-token")]
        public IActionResult ValidateUserToken([FromBody]UserAccount model)
        {
            var isValid = this.appAuthenticationService.ValidateUserToken(model.UserID, model.Token);
            return Ok(isValid);
        }
    }
}