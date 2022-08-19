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
    public class GetSpecificRoomController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Room_GetSpecificRoom(string room_id)
        {
            if(room_id.Trim().Length == 0)
            {
                return BadRequest("输入房间ID为空");
            }
            Room room = Room.Find(room_id);
            if(room != null)
            {
                return Ok(room);
            }
            else
            {
                return NotFound("房间不存在");
            }
        }
    }
}
