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
    public class DepartmentController : AppController
    {   
        public DepartmentController(AppDbContext dbContext, ILogger<DepartmentController> logger)
            :base(dbContext,logger)
        {
            this.dbContext = dbContext;
        }

        [Route("")]
        [Route("[action]")]
        public IEnumerable<Department> Index()
        {
                var depList = dbContext.Departments.ToList();
                return depList;
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add([FromBody] Department departmentList)
        {
            try
            {
                dbContext.Departments.Add(departmentList);
                dbContext.SaveChanges();
                return Ok(departmentList);
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update([FromBody] Department departmentList)
        {
            try
            {
                Department depList = dbContext.Departments.Find(departmentList.ID);

                if (depList != null)
                {
                    depList.ID = departmentList.ID;
                    depList.DepartmentName = departmentList.DepartmentName;

                    dbContext.Departments.Update(depList);
                    dbContext.SaveChanges();

                    return Ok(departmentList);
                }
                else
                {
                    throw new Exception($"User Not found with a user ID of '{depList.ID}'.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(GetErrorMessage(ex));
            }
        }
        [Route("[action]")]
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            try
            {
                Department depList = dbContext.Departments.Find(id);

                if (depList != null)
                {
                    dbContext.Departments.Remove(depList);
                    dbContext.SaveChanges();

                    return Json(id);
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
