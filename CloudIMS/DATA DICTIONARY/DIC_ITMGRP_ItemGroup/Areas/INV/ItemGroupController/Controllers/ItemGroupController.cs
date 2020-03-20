using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using CloudImsCommon.Routing;
using DataDictionary.Unit.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CloudIms.Areas.UserAccount.Controllers;




namespace CloudIms.Areas.UserAccount.Controllers
{
    [Area("inv")]
    [Folder("data-dictionary")]
    [Authorize]
    public class ItemGroupController : AppController
    {

        public ItemGroupController(AppDbContext dbContext, ILogger<ItemGroupController> logger)
            : base(dbContext, logger)
        {

        }

        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <param name="igID"></param>
        /// <param name="item_group_name"></param>

        /// <returns></returns>

        [Route("[area]/[folder]/item-group")]
        [Route("[area]/[folder]/item-group/index")]

        public IActionResult Index()
        {
            var model = new ItemGroupViewModel(HttpContext);
            model.ItemGroups = DbContext.ItemGroups.ToList();
            return View("Index", model);
        }

        [Route("[area]/[folder]/item-group/Add")]
        public JsonResult Add(String igID, String item_group_name)
        { 
            try
            {
                    var ig2 = new ItemGroup();
                    ig2.ID = igID;
                    ig2.ItemGroupName = item_group_name;
                 
                    DbContext.ItemGroups.Add(ig2);
                    DbContext.SaveChanges();
                    return GETJSON();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return GETJSON();
        }
        [Route("[area]/[folder]/item-group/Edit")]
        public JsonResult Edit(String igID, String item_group_name)
        {
            try
            {
                    var ig = new ItemGroup();
                    ig.ItemGroupName = item_group_name;

                    DbContext.ItemGroups.Update(ig);
                    DbContext.SaveChanges();
                    return GETJSON();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return GETJSON();
        }


        [Route("[area]/[folder]/item-group/Delete")]
        public JsonResult Delete(String ID)
        {
            try
            {
                var im = DbContext.ItemGroups.Find(ID);

                if (im != null)
                {
                    DbContext.ItemGroups.Remove(im);
                    DbContext.SaveChanges();
                    return GETJSON();
                }
                else
                {
                    return GETJSON();
                }
            }
            catch (Exception)
            {
                return GETJSON();
            }

        }
        public JsonResult GETJSON()
        {
            var model = new ItemGroupViewModel(HttpContext);
            model.ItemGroups = DbContext.ItemGroups.ToList();
            return Json (model);
        }
    }
}