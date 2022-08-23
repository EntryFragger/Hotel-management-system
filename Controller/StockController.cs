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
    public class StockController : ControllerBase
    {
        /// <summary>
        /// 获取所有库存信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <returns>所有的库存信息/returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Stock_GetAll(string tokenValue)/* 获取所有库存信息的接口*/
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                List<Stock> list = Stock.GetList();
                if (list.Count > 0)
                {
                    return Ok(new JsonResult(list));
                }
                else
                {
                    return NotFound("数据库中不存在库存");
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
        /// <summary>
        /// 删除特定库存
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="Name">库存名称</param>
        /// <returns>删除特定库存的结果</returns>
        public IActionResult DeleteStock(string tokenValue, string Name)//删除某库存
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                if (Stock.Find(Name) == null)
                {
                    return BadRequest("没有该库存");
                }
                else
                {
                    Stock.Delete(Name);
                    return Ok("删除成功");
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
        /// <summary>
        /// 修改库存
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="name">库存名称</param>
        /// <param name="unit">计量单位</param>
        /// <param name="quantity">数量</param>
        /// <returns>修改库存的结果</returns>
        public IActionResult ChangeStock(string tokenValue, string name, string unit, string quantity)//修改库存
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                if (Stock.Find(name) == null)//没有该库存无法修改
                {
                    return BadRequest("无该库存");
                }
                else
                {
                    Stock.Add(name, unit, quantity);
                    return Ok("修改完成");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 添加新品类库存
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="name">库存名称</param>
        /// <param name="unit">计量单位</param>
        /// <param name="quantity">数量</param>
        /// <returns>添加新品类库存的结果</returns>


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateNewStock(string tokenValue, string name, string unit, string quantity)//添加新品类库存
        {
            try
            {
                //判断token
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                if (Stock.Find(name) != null)//已有该类库存无法添加
                {
                    return BadRequest("该库存已经存在");
                }
                else
                {
                    Stock.Add(name, unit, quantity);
                    return Ok("添加完成");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
