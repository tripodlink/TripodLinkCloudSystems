using Cloud_IMS_App.Controllers;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Cloud_IMS_Api.Controllers
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

        [Route("[action]")]
        public IActionResult Find(string id)
        {
            try
            {
                //UserAccount user = dbContext.UserAccounts.Find(id);

                //user.UserGroups = dbContext.UserGroups.Where(ug =>
                //    dbContext.UserAccountGroups.Where(uag => uag.UserAccountID == id).Select(x => new { GroupID = x.UserGroupID }).Any(uag => uag.GroupID == ug.ID)
                //).ToList();
                
                return Ok("");
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



        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] UserAccount user)
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

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }


        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] UserAccount user)
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

                    return Ok(user);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{user.UserID}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }

        [Route("")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                UserAccount userForUpdate = dbContext.UserAccounts.Find(id);

                if (userForUpdate != null)
                {
                    dbContext.UserAccounts.Remove(userForUpdate);
                    dbContext.SaveChanges();

                    return Ok(id);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{id}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
    }
}