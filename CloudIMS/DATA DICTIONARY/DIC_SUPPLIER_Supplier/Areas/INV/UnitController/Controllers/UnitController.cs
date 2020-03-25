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
    public class UnitController : AppController
    {

        public UnitController(AppDbContext dbContext, ILogger<UnitController> logger)
            : base(dbContext, logger)
        {

        }

        /// <summary>
        /// The action to login user.
        /// </summary>
        /// <param name="Code"></param>
        /// <param name="Desc"></param>
        /// <param name="ShortDesc"></param>
        /// <returns></returns>

        [Route("[area]/[folder]/unit")]
        [Route("[area]/[folder]/unit/index")]

        public IActionResult Index()
        {
            var model = new UnitViewModel(HttpContext);
            model.UnitCodes = dbContext.UnitCodes.ToList();
            return View("Index", model);
        }

        [Route("[area]/[folder]/item-group/AddEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult AddEdit(String Code, String Desc, String ShortDesc)
        {
            var model = new UnitViewModel(HttpContext);
            model.UnitCodes = dbContext.UnitCodes.ToList();
            try
            {
                var uc = dbContext.UnitCodes.Find(Code);
                if (uc != null)
                {
                    uc.Description = Desc;
                    uc.ShortDescription = ShortDesc;
                    dbContext.UnitCodes.Update(uc);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index", model);
                }
                else
                {
                    var uc2 = new UnitCode();
                    uc2.Code = Code;
                    uc2.Description = Desc;
                    uc2.ShortDescription = ShortDesc;

                    dbContext.UnitCodes.Add(uc2);
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


        [Route("[area]/[folder]/unit/Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Delete(String ID)
        {
            var model = new UnitViewModel(HttpContext);
            model.UnitCodes = dbContext.UnitCodes.ToList();
            try
            {
                var uc = dbContext.UnitCodes.Find(ID);

                if (uc != null)
                {
                    dbContext.UnitCodes.Remove(uc);
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