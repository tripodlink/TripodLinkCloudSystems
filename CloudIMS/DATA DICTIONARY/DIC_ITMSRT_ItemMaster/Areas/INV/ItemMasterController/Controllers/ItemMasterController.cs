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
    public class ItemMasterController : AppController
    {

        public ItemMasterController(AppDbContext dbContext, ILogger<ItemMasterController> logger)
            : base(dbContext, logger)
        {

        }

        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <param name="imID"></param>
        /// <param name="ItemGroupDropDown"></param>
        /// <param name="itemname"></param>
        /// <param name="itemunit"></param>
        /// <param name="itemsup"></param>
        /// <param name="itemmanu"></param>
        /// <returns></returns>

        [Route("[area]/[folder]/item-master")]
        [Route("[area]/[folder]/item-master/index")]

        public IActionResult Index(String ID)
        {
            var model = new ItemMasterViewModel(HttpContext);
            model.ItemMasters = dbContext.ItemMasters.ToList();
            model.ItemGroups = dbContext.ItemGroups.ToList();

        
            return View("Index", model);
          
        }

        [Route("[area]/[folder]/item-master/AddEdit")]
        public IActionResult AddEdit(String imID, String ItemGroupDropDown, String itemname, String itemunit, String itemsup, String itemmanu)
        {
            var model = new ItemMasterViewModel(HttpContext);
            model.ItemMasters = dbContext.ItemMasters.ToList();
            try
            {
                var im = dbContext.ItemMasters.Find(imID);
                if (im != null)
                {
                    im.ItemGroup = ItemGroupDropDown;
                    im.ItemName = itemname;
                    im.Unit = itemunit;
                    im.Supplier = itemsup;
                    im.Manufacturer = itemmanu;

                    dbContext.ItemMasters.Update(im);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index", model);
                }
                else
                {
                    var im2 = new ItemMaster();
                    im2.ID = imID;
                    im2.ItemGroup = ItemGroupDropDown;
                    im2.ItemName = itemname;
                    im2.Unit = itemunit;
                    im2.Supplier = itemsup;
                    im2.Manufacturer = itemmanu;
                    dbContext.ItemMasters.Add(im2);
                    dbContext.SaveChanges();
                    return RedirectToAction("index", model);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return View("index", model);
        }


        [Route("[area]/[folder]/item-master/Delete")]
        public IActionResult Delete(String ID)
        {
            var model = new ItemMasterViewModel(HttpContext);
            model.ItemMasters = dbContext.ItemMasters.ToList();
            try
            {
                var im = dbContext.ItemMasters.Find(ID);

                if (im != null)
                {
                    dbContext.ItemMasters.Remove(im);
                    dbContext.SaveChanges();
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