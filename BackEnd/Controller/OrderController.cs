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
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// 获取所有订单的信息
        /// </summary>
        /// <param name="token_value">token</param>
        /// <returns>所有订单的信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        
        public IActionResult Order_GetAll(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if(user.Department!= "Logistics")
            {
                return BadRequest("权限不符");
            }
            List<RoomOrder> list = RoomOrder.GetAllList();
            if (list!=null)
            {
                return Ok(new JsonResult(list));
            }
            else
            {
                return NotFound("数据库中不存在订单");
            }
        }
        /// <summary>
        /// 根据客人CustomerID找出其所有的订单
        /// </summary>
        /// <param name="customer_id">顾客ID</param>
        /// <param name="token_value">token</param>
        /// <returns>对应顾客的所有订单</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        
        public IActionResult ListOrderByGuest(string customer_id,string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Logistics")
            {
                return BadRequest("权限不符");
            }
            List<RoomOrder> list = RoomOrder.ListByGuest(customer_id);
            if (list != null)
            {
                return Ok(new JsonResult(list));
            }
            else
            {
                return NotFound("该顾客不存在订单");
            }
        }

        /// <summary>
        /// 顾客退房
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="room_id">房间ID</param>
        /// <returns>退房结果</returns>
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
                if (room_status == "Available")
                {
                    return BadRequest("该房间未入住，无需退房");
                }
                //一切正常，开始进行退房操作
                List<RoomOrder> list = RoomOrder.ListByRoom(room_id);
                RoomOrder or = null;
                for(int i=0;i<list.Count;i++)
                {
                    if(list[i].OrderStatus=="ing")
                    {
                        or = list[i];
                    }
                }
                int issuccess_one=RoomOrder.Change_Order_Status(or.OrderID);
                /*以上为改变订单状态,成功改变订单状态则开始改变坊间状态*/
                if (issuccess_one != -1)
                {
                    int issuccess = Room.Change_Room_Status(room_id, "Available");
                    //只有房间状态也修改成功才算完成退房
                    if(issuccess!=-1)
                        return Ok("退房成功");
                    else
                        return NotFound("退房失败");
                }
                else
                {
                    return NotFound("退房失败");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
