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

/*实际上前端并不需要创建收支信息的接口*/
/*收支信息表的创建是随着其他表的活动进行的，是数据一致性的考虑*/
/*所以前端不需要创建收支信息的接口*/
namespace BackEnd.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 获取所有的收支信息
        /// </summary>
        /// <param name="token_value">token</param>
        /// <returns>查询到的收支信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllAcount(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Finance")
            {
                return BadRequest("权限不符");
            }
            List<Account> list = Account.GetAllList();
            if (list != null)
            {
                return Ok(new JsonResult(list));
            }
            else
            {
                return NotFound("不存在收支信息");
            }
        }
    }
}