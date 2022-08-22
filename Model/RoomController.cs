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
    public class RoomController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRoomInfo_ByType(string tokenValue, string room_type)
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
                if (room_type.Trim().Length == 0)
                {
                    return BadRequest("输入房间类型为空");
                }
                //初始化一个list
                List<Room> list = null;
                //判断room_type的特殊性
                if (room_type == "ALL")
                {
                    list = Room.RoomInfo_ListAllType();
                }
                else
                {
                    list = Room.RoomInfo_ListByType(room_type);
                }
                //返回结果
                if (list.Count > 0)
                {
                    return Ok(new JsonResult(list));
                }
                else
                {
                    return NotFound("不存在相应类型的房间");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Room_GetSpecificRoom(string tokenValue, string room_id)
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
                Room room = Room.Find(room_id);
                if (room != null)
                {
                    return Ok(new JsonResult(room));
                }
                else
                {
                    return NotFound("房间不存在");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
