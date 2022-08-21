using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.DBUtility;
using Oracle.ManagedDataAccess.Client;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialStatementController : ControllerBase
    {
        /// <summary>
        /// 获取当前人的财务报单
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <returns>相应人的财务报单</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetFinancialStatement(string tokenValue)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                List<FinancialStatement> financialStatement = FinancialStatement.GetList(user.ID);
                return Ok(new JsonResult(financialStatement));
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }

        }
        /// <summary>
        /// 提交财务报表
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="statementContent">报单内容</param>
        /// <param name="amount">金额</param>
        /// <param name="state">状态</param>
        /// <returns>返回报单是否提交成功的信息</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult SubmitFinancialStatement(string tokenValue,string statementContent,long amount,string state)
        {
            try
            {
                string statementID = FinancialStatement.NextStatementID();
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                FinancialStatement.Add(user.ID, statementID, statementContent, amount, state);
                return Ok("提交财务报单成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// /*审批财物报单*/
        /// </summary>
        /// <param name="statement_id"></param>
        /// <param name="statement_content"></param>
        /// <param name="amount"></param>
        /// <param name="state"></param>
        /// <param name="token_value"></param>
        /// <returns></returns>
        public IActionResult FinicalStatement_Create(string statement_id, string statement_content, long amount, string state, string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Finance")
            {
                return BadRequest("权限不符");
            }
            if (statement_id.Trim().Length == 0 || statement_content.Trim().Length == 0 || amount < 0 || state.Trim().Length == 0)
            {
                return BadRequest("输入信息不完整");
            }
            int issuccess = FinicalStatement.Add(statement_id, statement_content, amount, state);
            if (issuccess != -1)
            {
                return Ok("财务报单创建成功");
            }
            else
            {
                return NotFound("财务报单创建失败");
            }
        }
        /// <summary>
        /// 获取所有财务报单，面向财务报
        /// </summary>
        /// <param name="token_value"></param>
        /// <returns></returns>
        public IActionResult GetFinancialStatement(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Finance")
            {
                return BadRequest("权限不符");
            }
            List<FinancialStatement> list = FinancialStatement.GetList();
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound("不存在财务报单");
            }
        }
    }
}
