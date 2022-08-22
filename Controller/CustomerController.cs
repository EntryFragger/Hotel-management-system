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
    public class CustomerController : ControllerBase
    {
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
                if (room_status != "Avaliable")
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
                    long OID = Order.NextID();//订单ID
                    int vipLv = Customer.FindVip(CustomerID);//找到该顾客vip等级
                    float discount = Vip.SelectDiscount(vipLv);//对应折扣
                    int roomprice = Room.FindRoomPrice(RoomID);//找到该房间单价
                    float price = Days * discount * roomprice;//计算金额
                    Order.CreateOrder(OID, RoomID, CustomerID, starttime, endtime, Days, price);//创建订单
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
                    return Ok(Customer);
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
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
