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

/*审批财务报单*/
/*先跳过权限验证，权限验证最好不体现在这个接口，假如想要被拒绝的财务报单，就是状态为失败的财务报单也能被记录在信息库中*/
namespace BackEnd.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateFinicalStatementController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        /*审批财物报单*/
        public IActionResult FinicalStatement_Create(string statement_id, string statement_content, long amount,string state,string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Management")
            {
                return BadRequest("权限不符");
            }
            if (statement_id.Trim().Length == 0 || statement_content.Trim().Length == 0 || amount<0 || state.Trim().Length == 0)
            {
                return BadRequest("输入信息不完整");
            }
            int issuccess = FinicalStatement.Add(statement_id, statement_content, amount, state);
            if(issuccess != -1)
            {
                return Ok("财务报单创建成功");
            }
            else
            {
                return NotFound("财务报单创建失败");
            }
        }
    }
}
