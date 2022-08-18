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
    public class GetAllRoomController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetRoomInfo_ByType(string room_type)
        {
            List<RoomInfo> list = Room.RoomInfo_ListByType(room_type);
            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("数据库错误：不存在房间");
            }
        }
    }
}
