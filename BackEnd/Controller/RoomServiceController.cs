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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomServiceController : ControllerBase
    {
        /// <summary>
        /// 获取当前员工未完成的任务列表
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <returns>当前员工未完成的任务列表</returns>
        [HttpGet]
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
                long id = user.ID;
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
        /// <summary>
        /// 新建客房服务
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="room_id">房间ID</param>
        /// <param name="time">时间</param>
        /// <param name="type">类型</param>
        /// <param name="remark">备注</param>
        /// <param name="amount">金额</param>
        /// <returns>新建客房服务的结果</returns>



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CreateRoomService(string tokenValue, string room_id, string time, string type, string remark, long amount)
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
                if (room_id.Trim().Length == 0 || time.Trim().Length == 0 || remark.Trim().Length == 0 || type.Trim().Length == 0 || amount < 0)
                {
                    return BadRequest("输入信息不完整或金额非法");
                }
                if (RoomService.CheckType(type) == -1)
                {
                    return BadRequest("输入服务类型错误");
                }
                if (type == "维修" || type == "清洁")
                {
                    if (amount != 0)
                    {
                        return BadRequest("维修与清洁服务价格错误");
                    }
                }
                //返回结果
                long emid = RoomService.Jobdistribution();
                if (emid == -1)
                    return BadRequest("后勤部无人手");
                int issuccess = RoomService.AddRoomService(room_id, time, type, remark, amount, "UnDone", emid);
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
        /// <summary>
        /// 获取所有的客房服务信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <returns>所有的客房服务信息</returns>



        [HttpGet]
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
        /// <summary>
        /// 修改客房服务的状态
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="room_id">房间ID</param>
        /// <param name="time">时间</param>
        /// <param name="status">状态</param>
        /// <returns>修改客房服务状态的结果</returns>


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
                RoomService original_room_service = RoomService.Find(room_id, time);
                if(original_room_service == null)
                {
                    return BadRequest("房间服务不存在");
                }
                string original_status = RoomService.Find(room_id, time).Status;
                if(original_status == "Done")
                {
                    return BadRequest("房间服务已完成，无法修改");
                }
                if (original_status == status)
                {
                    return BadRequest("修改状态与原状态相同，无需修改");
                }
                int issuccess = RoomService.Change_RoomService_Status(room_id, time, status);
                if (issuccess != -1)
                {
                    string current_status = RoomService.Find(room_id, time).Status;
                    if (current_status == "Done")
                    {
                        long aid = Account.NextID();
                        string date = DateTime.Now.ToLongDateString();
                        long amount = RoomService.Find(room_id, time).Amount;
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