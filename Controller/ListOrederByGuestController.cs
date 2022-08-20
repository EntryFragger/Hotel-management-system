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
    public class ListOrderByGuestController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        /*根据顾客的ID返回顾客全部的订单信息*/
        public IActionResult ListOrderByGuest(string customer_id，string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Management")
            {
                return BadRequest("权限不符");
            }
            List<Order> list = Order.ListByGuest(customer_id);
            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("该顾客不存在订单");
            }
        }
    }
}
