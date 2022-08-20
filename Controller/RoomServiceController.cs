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
    public class RoomServiceController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUndoneJob(string tokenValue)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                //获取当前用户的id
                string id = user.ID;
                //返回结果
                List<RoomJobInfo> list = RoomService.GetUndoneJobInfo(id);
                if (list.Count > 0)
                {
                    return Ok(new JsonResult(list));
                }
                else
                {
                    return NotFound("不存在未完成的房间订单");
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
        public IActionResult CreateRoomService(string tokenValue, string room_id, string time, string type, string remark)
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
                if (room_id.Trim().Length == 0 || time.Trim().Length == 0 || remark.Trim().Length == 0 )
                {
                    return BadRequest("输入信息不完整");
                }
                if (RoomService.GetAmount(type) == "")
                {
                    return BadRequest("输入服务类型错误");
                }
                //返回结果
                int issuccess = RoomService.AddRoomService(room_id, time, type, remark, RoomService.GetAmount(type), "UnDone", "");
                if (issuccess != -1)
                {
                    return Ok("住房服务创建成功");
                }
                else
                {
                    return BadRequest("房间服务添加失败(房间号不存在或者房间服务已存在)");
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
        public IActionResult GetAllRoomService(string tokenValue)
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



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ChangeRoomServiceStatus(string tokenValue, string room_id, string time, string status)
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
                    string current_status = RoomService.Find(room_id, time).Status;
                    if(current_status == "Done")
                    {
                        string aid = "temporate_aid";
                        string date = "get_now_date";
                        int amount = 6;
                        //string amount = RoomService.Find(room_id, time).Amount;
                        string type = "income";
                        Account.CreateAccount(aid, date, amount, type);
                    }
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
