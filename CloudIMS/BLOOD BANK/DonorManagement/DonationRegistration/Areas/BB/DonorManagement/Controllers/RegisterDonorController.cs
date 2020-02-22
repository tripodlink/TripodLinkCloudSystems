using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BloodBank.DonorManagement.RegisterDonor.Controllers
{
    [Area("bb")]
    [Folder("donor-management")]
    [Authorize]
    public class RegisterDonorController : AppController
    {
        public RegisterDonorController(AppDbContext dbContext, ILogger<RegisterDonorController> logger)
            : base(dbContext, logger)
        {
        }

        [Route("[area]/[folder]/register-donor")]
        [Route("[area]/[folder]/register-donor/index")]
        public IActionResult Index()
        {
            return View();
        }

    }
}