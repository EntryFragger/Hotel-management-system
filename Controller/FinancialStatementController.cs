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
