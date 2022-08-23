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
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 使用ID和密码进行登录
        /// </summary>
        /// <param name="ID">用户ID</param>
        /// <param name="Password">用户密码</param>
        /// <returns>token</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Login(long ID, string Password)
        {
            try
            {
                Employee employee = Employee.Find(ID);
                if (employee == null)
                {
                    return BadRequest("账号不存在");
                }
                if (Password != employee.Password)
                {
                    return BadRequest("账号与密码不符");
                }
                JWTPayload jwt = new JWTPayload();
                jwt.ID = employee.ID;
                jwt.Department = employee.Department;
                return Ok(JWTHelper.SetJwtEncode(jwt));
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
