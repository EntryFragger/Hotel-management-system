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
    public class CheckoutController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Room_Checkout(string tokenValue, string room_id)
        {
            try 
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Reception")
                {
                    return BadRequest("权限不符");
                }
                //判断输入合法性
                if (room_id.Trim().Length == 0)
                {
                    return BadRequest("输入房间ID为空");
                }
                //判断房间是否存在
                Room room = Room.Find(room_id);
                if (room == null)
                {
                    return NotFound("该房间不存在");
                }
                //判断是否需要退房
                string room_status = room.RoomStatus;
                if (room_status == "Avaliable")
                {
                    return BadRequest("该房间未入住，无需退房");
                }
                //一切正常，返回结果
                int issuccess = Room.Change_Room_Status(room_id, "Avaliable");
                if (issuccess != -1)
                {
                    return Ok("退房成功");
                }
                else
                {
                    return NotFound("该房间不存在");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
