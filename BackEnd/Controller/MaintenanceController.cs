using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.Utility;
using Oracle.ManagedDataAccess.Client;

namespace BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MaintenanceController : ControllerBase
    {
        /// <summary>
        /// 获取设备维护信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <returns>设备维护信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetMaintenanceInfo(string tokenValue)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                List<Maintenance> maintenances = Maintenance.GetList();
                return Ok(new JsonResult(maintenances));
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 提交设施维护信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="date">日期</param>
        /// <param name="itemID">设备ID</param>
        /// <param name="employeeID">员工ID</param>
        /// <returns>信息提交结果</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult SubmitMaintenanceInfo(string tokenValue, string date, string itemID, long employeeID)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Logistics")
                {
                    return BadRequest("权限不符");
                }
                Maintenance maintenance = Maintenance.Find(itemID);
                string itemName = maintenance.ItemName;
                Maintenance.Add(itemID, employeeID, date, itemName);
                return Ok("信息提交成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}