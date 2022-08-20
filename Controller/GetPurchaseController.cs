using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Utility;
using BackEnd.Model;

namespace BackEnd.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPurchaseController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        /*获取所有采购信息*/
        public IActionResult GetUndoneJob(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Management")
            {
                return BadRequest("权限不符");
            }
            List<Purchase> list = Purchase.GetAllList();
            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("不存在采购信息");
            }
        }
    }
}
