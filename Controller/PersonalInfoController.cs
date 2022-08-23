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
    public class PersonalInfoController : ControllerBase
    {
        /// <summary>
        /// 个人修改当前人的信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="newInfo">新的信息</param>
        /// <returns>是否修改成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ResetInfor(string tokenValue, EmployeeInforDetailedWithoutID newInfo)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                Employee employee = Employee.Find(user.ID);
                Employee.Add(employee.ID, newInfo.Name, newInfo.Gender, newInfo.Age, newInfo.Salary, newInfo.PhoneNum, newInfo.Department, employee.Password);
                return Ok("修改成功");

            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns>是否修改成功的提示信息</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ResetPassword(string tokenValue, string oldPassword, string newPassword)
        {
            if (oldPassword == newPassword)
            {
                return BadRequest("新密码与原密码一致");
            }
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                Employee employee = Employee.Find(user.ID);
                string truePassword = employee.Password;
                if (truePassword != oldPassword)
                {
                    return BadRequest("旧密码错误");
                }
                Employee.Add(employee.ID, employee.Name, employee.Gender, employee.Age, employee.Salary, employee.PhoneNum, employee.Department, newPassword);
                return Ok("修改成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
