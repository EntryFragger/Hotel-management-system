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
    public class CreateRoomServiceController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult RoomService_Create(string room_id, string time, string remark,string amount)
        {
            if(room_id.Trim().Length == 0 || time.Trim().Length == 0 || remark.Trim().Length == 0 || amount.Trim().Length == 0)
            {
                return BadRequest("输入信息不完整");
            }
            int issuccess = RoomService.AddRoomService(room_id, time, "", remark, amount, "", "");
            if(issuccess != -1)
            {
                return Ok("住房服务创建成功");
            }
            else
            {
                return NotFound("房间服务已存在，无法添加");
            }
        }
    }
}
