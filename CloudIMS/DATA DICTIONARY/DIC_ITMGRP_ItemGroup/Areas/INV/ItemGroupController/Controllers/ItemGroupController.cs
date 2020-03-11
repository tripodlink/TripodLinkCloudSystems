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
        /// <param name="ID"></param>
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

        [Route("[area]/[folder]/item-group/AddEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult AddEdit(String ID, String item_group_name)
        {
            var model = new ItemGroupViewModel(HttpContext);
            model.ItemGroups = DbContext.ItemGroups.ToList();
            try
            {
                var ig = DbContext.ItemGroups.Find(ID);
                if (ig != null)
                { 
                    ig.ItemGroupName = item_group_name;

                    DbContext.ItemGroups.Update(ig);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index", model);
                }
                else
                {
                    var ig2 = new ItemGroup();
                    ig2.ID = ID;
                    ig2.ItemGroupName = item_group_name;
                 
                    DbContext.ItemGroups.Add(ig2);
                    DbContext.SaveChanges();
                    return RedirectToAction("index", model);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("index", model);
        }


        [Route("[area]/[folder]/item-group/Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Delete(String ID)
        {
            var model = new ItemGroupViewModel(HttpContext);
            model.ItemGroups = DbContext.ItemGroups.ToList();
            try
            {
                var im = DbContext.ItemMasters.Find(ID);

                if (im != null)
                {
                    DbContext.ItemMasters.Remove(im);
                    DbContext.SaveChanges();
                    return RedirectToAction("index", model);
                }
                else
                {
                    return View("index", model);
                }
            }
            catch (Exception)
            {
                return View("index", model);
            }

        }
    }
}