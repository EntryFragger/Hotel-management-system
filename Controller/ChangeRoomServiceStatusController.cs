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
    public class ChangeRoomServiceStatusController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult RoomService_ChangeStatus(string tokenValue, string room_id, string time, string status)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                //判断输入合法性
                if (room_id.Trim().Length == 0 || time.Trim().Length == 0 || status.Trim().Length == 0)
                {
                    return BadRequest("输入信息有误");
                }
                //返回结果
                int issuccess = RoomService.Change_RoomService_Status(room_id, time, status);
                if (issuccess != -1)
                {
                    return Ok("房间服务状态修改成功");
                }
                else
                {
                    return BadRequest("房间服务不存在，无法修改");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
