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
       public  AppDbContext dbContext;
       public  ILogger<AppController> Logger;
        public AppController(AppDbContext dbContext, ILogger<AppController> logger) {
            this.dbContext = dbContext;
            Logger = logger;
        }

        public string GetErrorMessage(Exception ex)
        {
            var errorMessage = ex.Message;

            if (ex.InnerException != null)
            {
                errorMessage += "\r\nInner Exception: " + ex.InnerException.Message;
            }

            return errorMessage;
        }
    }
}