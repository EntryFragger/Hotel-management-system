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
        [ProducesResponseType(404)]
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
        [ProducesResponseType(404)]
        public IActionResult SubmitFinancialStatement(string tokenValue, string statementContent, float amount, string state)
        {
            try
            {
                long statementID = FinancialStatement.NextStatementID();
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
        /// 获取所有财务报单，面向财务部
        /// </summary>
        /// <param name="token_value">token</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetAllFinancialStatement(string token_value)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Finance")
            {
                return BadRequest("权限不符");
            }
            List<FinancialStatement> list = FinancialStatement.GetList();
            if (list != null)
                return Ok(new JsonResult(list));
            else
                return NotFound("不存在财务报单信息");
        }
        /// <summary>
        /// 财务部调用审批财务报单，会将财务报单的状态从未通过改为通过
        /// </summary>
        /// <param name="token_value">token</param>
        /// <param name="sID">要审批的财务报单ID</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult GetFinancialStatement(string token_value, long sID)
        {
            EmployeeInforToken user = JWTHelper.GetUsers(token_value);
            if (user.Department != "Finance")
            {
                return BadRequest("权限不符");
            }
            int success = FinancialStatement.Change_FinicalStatement_Status(sID);
            if (success != -1)
            {
                return Ok("审批成功");
            }
            else
            {
                return NotFound("不存在财务报单");
            }
        }
    }
}