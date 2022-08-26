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
    public class PurchaseController : ControllerBase
    {
        /// <summary>
        /// 获取所有的货物收购信息
        /// </summary>
        /// <param name="token_value">token</param>
        /// <returns>所有货物的收购信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        /*获取所有的货物收购信息*/
        public IActionResult GetAllPurchase(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Logistics")
            {
                return BadRequest("权限不符");
            }
            List<Purchase> list = Purchase.GetAllList();
            if (list != null)
            {
                return Ok(new JsonResult(list));
            }
            else
            {
                return NotFound("不存在采购信息");
            }
        }
        /// <summary>
        /// 创建货物收购信息,如果创建成功，同时添加收支信息
        /// </summary>
        /// <param name="goods_name">商品名称</param>
        /// <param name="quantity">数量</param>
        /// <param name="price">价格</param>
        /// <param name="date">日期</param>
        /// <param name="unit">单位</param>
        /// <param name="token_value">token</param>
        /// <returns>创建是否成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult Purchase_Create(string goods_name, string quantity, float price, string date, string unit, string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Management")
            {
                return BadRequest("权限不符");
            }
            if (goods_name.Trim().Length == 0 || quantity.Trim().Length == 0 || date.Trim().Length == 0 || price < 0 || unit.Trim().Length == 0)
            {
                return BadRequest("输入信息不完整");
            }
            long p_id = Purchase.NextID();
            int issuccess = Purchase.CreatePurchase(p_id, goods_name, unit, quantity, price, date);
            if (issuccess != -1)
            {
                /*这里要取到最大的一个收获订单号，没有就取0*/
                long account_id = Account.NextID();
                /*随着收购，要对收支信息做出对应修改*/
                Account.CreateAccount(account_id, date, price, "expenses");
                return Ok("收购信息创建成功");
            }
            else
            {
                return NotFound("收购信息创建失败");
            }
        }
    }
}