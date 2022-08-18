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
    public class UndoneJobController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUndoneJob()
        {
            string id = "23";
            List<RoomJobInfo> list = RoomService.GetUndoneJobInfo(id);
            if (list.Count > 0)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("不存在未完成的房间订单");
            }
        }
    }
}
