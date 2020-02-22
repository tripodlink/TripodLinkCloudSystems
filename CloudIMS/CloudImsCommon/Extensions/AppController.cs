using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudImsCommon.Database;
using Microsoft.Extensions.Logging;

namespace CloudImsCommon.Extensions
{
    public class AppController : Controller
    {
       public  AppDbContext DbContext;
       public  ILogger<AppController> Logger;
        public AppController(AppDbContext dbContext, ILogger<AppController> logger) {
            DbContext = dbContext;
            Logger = logger;
        }

        public bool IsAuthenticated() {
            try
            {
                var identity = HttpContext.User.Identity;

                return identity.IsAuthenticated;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}