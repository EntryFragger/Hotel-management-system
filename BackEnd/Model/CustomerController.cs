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
    public class CustomerController : ControllerBase
    {
        /// <summary>
        /// 顾客订房操作
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="RoomID">房间ID</param>
        /// <param name="CustomerID">顾客ID</param>
        /// <param name="Name">姓名</param>
        /// <param name="Gender">性别</param>
        /// <param name="PhoneNum">电话号码</param>
        /// <param name="Area">地区</param>
        /// <param name="starttime">住房起始时间</param>
        /// <param name="endtime">住房结束时间</param>
        /// <param name="Days">住房天数</param>
        /// <returns>订房操作执行的结果</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult CustomerBookRoom(string tokenValue, string RoomID, string CustomerID, string Name, string Gender, string PhoneNum, string Area, string starttime, string endtime, long Days)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Reception")//前台
                {
                    return BadRequest("权限不符");
                }
                if (RoomID.Trim().Length == 0 || CustomerID.Trim().Length == 0 || Name.Trim().Length == 0 || Gender.Trim().Length == 0 || PhoneNum.Trim().Length == 0 || Area.Trim().Length == 0 || Name.Trim().Length == 0)
                {
                    return BadRequest("输入信息有误");
                }
                string room_status = Room.Find(RoomID).RoomStatus;
                if (Room.Find(RoomID) == null)
                {
                    return BadRequest("该房间不存在");
                }
                if (room_status != "Available")
                {
                    return BadRequest("该房间已入住");
                }
                else
                {
                    Room.Change_Room_Status(RoomID, "Occupied");//房间状态改为已入住
                    if (Customer.Find(CustomerID) == null)//新顾客
                    {
                        Customer.AddCustomer(CustomerID, Name, Gender, PhoneNum, Area, 0);//vip等级默认为0
                    }
                    long OID = RoomOrder.NextID();//订单ID
                    int vipLv = Customer.FindVip(CustomerID);//找到该顾客vip等级
                    float discount = Vip.SelectDiscount(vipLv);//对应折扣
                    int roomprice = Room.FindRoomPrice(RoomID);//找到该房间单价
                    float price = Days * (1-discount) * roomprice;//计算金额
                    RoomOrder.CreateOrder(OID, RoomID, CustomerID, starttime, endtime, Days, price);//创建订单
                    long AccountID = Account.NextID();
                    Account.CreateAccount(AccountID, starttime, price, "income");//收支订单
                    return Ok("预定成功");
                }

            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 获取顾客个人信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="CustomerID">顾客ID</param>
        /// <returns>相应的顾客信息</returns>


        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetCustomerInformation(string tokenValue, string CustomerID)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Reception")//前台
                {
                    return BadRequest("权限不符");
                }
                if (CustomerID.Length == 0)//没输入ID
                {
                    return BadRequest("输入信息有误");
                }
                if (Customer.Find(CustomerID) == null)//ID不在顾客表中
                    return BadRequest("该顾客不存在");
                else
                {
                    Customer Customer = Customer.Find(CustomerID);//返回顾客信息
                    return Ok(new JsonResult(Customer));
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 开通vip
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="CustomerID">顾客ID</param>
        /// <param name="VipLv">vip等级</param>
        /// <returns>开通vip操作的执行结果</returns>


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult OpenVip(string tokenValue, string CustomerID, int VipLv)
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Reception")//前台
                {
                    return BadRequest("权限不符");
                }
                if (CustomerID.Length == 0 || VipLv < 0 || VipLv > 10)//vip等级默认0-10
                {
                    return BadRequest("输入信息有误");
                }
                if (Customer.Find(CustomerID) == null)
                    return BadRequest("该顾客不存在");
                else
                {
                    Customer.ChangeVip(CustomerID, VipLv);
                    return Ok("会员开通成功");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }

}