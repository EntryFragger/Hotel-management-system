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
    public class GetAllRoomServiceController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult RoomService_GetAll(string tokenValue)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                List<RoomServiceInfo> list = RoomService.ListAll_RoomServiceInfo();
                if (list.Count > 0)
                {
                    return Ok(new JsonResult(list));
                }
                else
                {
                    return NotFound("不存在房间服务");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
            
        }
    }
}
