using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Model;
using BackEnd.Utility;
using Oracle.ManagedDataAccess.Client;

namespace BackEnd.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NucleicAcidTestingController : ControllerBase
    {
        /// <summary>
        /// 个人提交核酸检测信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="result">检测结果</param>
        /// <param name="date">检测日期</param>
        /// <returns>是否提交成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult SubmitNucleicAcidInfor(string tokenValue,string result,string date)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                Employee employee = Employee.Find(user.ID);
                NucleicAcidTesting.Add(user.ID, user.Department, date, result, employee.Name);
                return Ok("提交成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }

        /// <summary>
        /// 管理部获取员工的核酸检测结果信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="department">部门</param>
        /// <param name="ID">员工ID</param>
        /// <returns>查询到的员工核酸检测结果信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetNucleicAcidInfor(string tokenValue,string department,string ID)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Management")
                {
                    return BadRequest("权限不符");
                }
                if (ID == "NULL")
                {
                    if (department == "ALL")
                    {
                        List<NucleicAcidTesting> nucleicAcidInfor = NucleicAcidTesting.ListAll();
                        return Ok(new JsonResult(nucleicAcidInfor));
                    }
                    else if (department == "NULL")
                        return BadRequest("两个参数都为NULL无法查询");
                    else
                    {
                        if (department != "Logistics" && department != "Finance" &&
                         department != "Management" && department != "Reception")
                        {
                            return BadRequest("错误的部门名称");
                        }
                        List<NucleicAcidTesting> nucleicAcidInfor = NucleicAcidTesting.QueryByDep(department);
                        return Ok(new JsonResult(nucleicAcidInfor));
                    }
                }
                else
                {
                    List<NucleicAcidTesting> nucleicAcidInfor = NucleicAcidTesting.QueryByID(long.Parse (ID));
                    if (department == "ALL" || department == "NULL")
                    {
                        return Ok(new JsonResult(nucleicAcidInfor));
                    }
                    else if (department != "Logistics" && department != "Finance" &&
                        department != "Management" && department != "Reception")
                    {
                        return BadRequest("错误的部门名称");
                    }
                    else
                    {
                        if (Employee.Find(long.Parse(ID)).Department != department)
                        {
                            return NotFound("当前员工信息不存在");
                        }
                        else
                        {
                            return Ok(new JsonResult(nucleicAcidInfor));
                        }
                    }
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}
