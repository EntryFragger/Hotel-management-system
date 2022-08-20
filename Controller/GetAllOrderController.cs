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
/*该controller提供 获取所有订单信息的接口*/
namespace BackEnd.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetAllOrderController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Order_GetAll(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if(user.Department!="Management")
            {
                return BadRequest("权限不符");
            }
            List<Order> list = Order.GetAllList();
            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("数据库中不存在订单");
            }
        }
    }
}
