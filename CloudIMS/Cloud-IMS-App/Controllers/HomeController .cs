using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudImsCommon.Database;
using CloudImsCommon.Extensions;
using CloudImsCommon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cloud_IMS_Api.Controllers
{
   
    [Route("api/[controller]")]
    public class HomeController : AppController
    {
        

        public HomeController(AppDbContext dbContext, ILogger<SupplierController> logger)
             : base(dbContext, logger)
        {
            this.dbContext = dbContext;
        }


       
        [Route("")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<ProgramMenu> Index()
        { 
            var pm_Menu = dbContext.ProgramMenus.ToList();
            return pm_Menu;
        }

        
        [Route("[action]")]
        [HttpGet]
        public IActionResult CountUser()
        {
            var user = dbContext.UserAccounts.ToList().Count;
            return Ok(user);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Count_StockIn()
        {
            var count_stockin = from invInHeader in dbContext.InventoryInTrxHeaders
                                join invInDetail in dbContext.InventoryInTrxDetails
                                on invInHeader.TransactionNo equals invInDetail.TransactionNo
                                select new
                                {
                                    txrno = invInDetail.TransactionNo
                                    
                                };
            var count = count_stockin.Select(x => x.txrno).Count();
            return Ok(count);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Count_StockOut()
        {
            var count_stockin = from invOutHeader in dbContext.InventoryOutTrxHeaders
                                join invOutDetail in dbContext.InventoryOutTrxDetails
                                on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                                where invOutHeader.Status == "I"
                                select new
                                {
                                    txrno = invOutHeader.TransactionNo

                                };
            var count = count_stockin.Select(x => x.txrno).Count();
            return Ok(count);
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetListOfItemStockIn()
        {
            var stockin = from invInHeader in dbContext.InventoryInTrxHeaders
                                join invInDetail in dbContext.InventoryInTrxDetails on invInHeader.TransactionNo equals invInDetail.TransactionNo
                                join im in dbContext.ItemMasters on invInDetail.ItemID equals im.ID
                                join uc in dbContext.UnitCodes  on invInDetail.Unit equals uc.Code
                                select new
                                {
                                    txrno = invInDetail.TransactionNo,
                                    trxdate = invInHeader.TransactionDate,
                                    qty = invInDetail.Quantity,
                                    itemname = im.ItemName,
                                    itemunit = uc.Description

                                };
            return Ok(stockin.ToList());
        }
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetListOfItemStockOut()
        {
            var stockOut = from invOutHeader in dbContext.InventoryOutTrxHeaders
                          join invOutDetail in dbContext.InventoryOutTrxDetails on invOutHeader.TransactionNo equals invOutDetail.TransactionNo
                          join im in dbContext.ItemMasters on invOutDetail.ItemID equals im.ID
                          join uc in dbContext.UnitCodes on invOutDetail.Unit equals uc.Code
                          where invOutHeader.Status == "I"
                          select new
                          {
                              txrno = invOutDetail.TransactionNo,
                              trxdate = invOutHeader.TransactionDate,
                              qty = invOutDetail.Quantity,
                              itemname = im.ItemName,
                              itemunit = uc.Description

                          };
            return Ok(stockOut.ToList());
        }

    }
}
