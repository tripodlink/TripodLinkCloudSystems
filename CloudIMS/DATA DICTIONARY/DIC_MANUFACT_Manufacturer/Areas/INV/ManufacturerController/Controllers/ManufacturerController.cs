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
    public class ManufacturerController : AppController
    {

        public ManufacturerController(AppDbContext dbContext, ILogger<ManufacturerController> logger)
            : base(dbContext, logger)
        {

        }

        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ManufactName"></param>
        /// <returns></returns>

        [Route("[area]/[folder]/manufacturer")]
        [Route("[area]/[folder]/manufacturer/index")]

        public IActionResult Index()
        {
            var model = new ManufacturerViewModel(HttpContext);
            model.manufacturers = dbContext.Manufacturers.ToList();
            return View("Index", model);
        }

        [Route("[area]/[folder]/manufacturer/AddEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult AddEdit(String ID, String ManufactName)
        {
            var model = new ManufacturerViewModel(HttpContext);
            model.manufacturers = dbContext.Manufacturers.ToList();
            try
            {
                var mc = dbContext.Manufacturers.Find(ID);
                if (mc != null)
                {
                    mc.ID = ID;
                    mc.ManufactName = ManufactName;
                    dbContext.Manufacturers.Update(mc);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index", model);
                }
                else
                {
                    var mc2 = new Manufacturer();
                    mc2.ID = ID;
                    mc2.ManufactName = ManufactName;
                    dbContext.Manufacturers.Add(mc2);
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


        [Route("[area]/[folder]/manufacturer/Delete")]
        public JsonResult Delete(String ID)
        {
            var model = new ManufacturerViewModel(HttpContext);
            model.manufacturers = dbContext.Manufacturers.ToList();
            try
            {
                var mc = dbContext.Manufacturers.Find(ID);

                if (mc != null)
                {
                    dbContext.Manufacturers.Remove(mc);
                    dbContext.SaveChanges();
                    return this.GETJSON();
                }
                else
                {
                    return this.GETJSON();
                }
            }
            catch (Exception)
            {
                return this.GETJSON();
            }

        }

        public JsonResult GETJSON()
        {
            var model = new ManufacturerViewModel(HttpContext);
            model.manufacturers = dbContext.Manufacturers.ToList();
            return Json(model);
        }
    }
     
}