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
    public class EmployeeInforController : ControllerBase
    {
        /// <summary>
        /// 获取要查询的员工信息（简）
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="department">部门</param>
        /// <param name="ID">员工ID</param>
        /// <returns>查询到的员工信息</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEmployeeSimpleInfor(string tokenValue, string department, string ID)
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
                        List<EmployeeInforSimple> employeeInforSimple = Employee.GetAllSimple();
                        return Ok(new JsonResult(employeeInforSimple));
                    }
                    else if (department == "NULL")
                        return BadRequest("两个参数都为NULL无法查询");
                    else
                    {
                        List<EmployeeInforSimple> employeeInforSimple = Employee.QueryByDep(department);
                        return Ok(new JsonResult(employeeInforSimple));
                    }
                }
                else
                {
                    EmployeeInforSimple employeeInforSimple = Employee.QueryByID(long.Parse(ID));
                    if (department == "ALL" || department == "NULL")
                    {
                        return Ok(new JsonResult(employeeInforSimple));
                    }
                    else
                    {
                        if (employeeInforSimple.Department != department)
                        {
                            return NotFound("当前员工信息不存在");
                        }
                        else
                        {
                            return Ok(new JsonResult(employeeInforSimple));
                        }
                    }
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }

        }
        /// <summary>
        /// 获取员工信息（详）
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="ID">员工ID</param>
        /// <returns>查找到的员工信息详情</returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetEmployeeDetailedInfor(string tokenValue, long ID)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Management")
                {
                    return BadRequest("权限不符");
                }
                EmployeeInforDetailed employeeInfordetailed = Employee.GetInforDetailed(ID);
                if(employeeInfordetailed==null)
                    return NotFound("不存在该员工");
                return Ok(new JsonResult(employeeInfordetailed));
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }

        /// <summary>
        /// 管理部修改员工信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="newInfo">新的员工信息</param>
        /// <returns>修改是否成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ChangeEmployeeInfor(string tokenValue, EmployeeInforDetailed newInfo)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Management")
                {
                    return BadRequest("权限不符");
                }
                Employee employee = Employee.Find(newInfo.ID);
                if (employee == null)
                {
                    return NotFound("没有该员工");
                }
                if (newInfo.Department != "Logistics" && newInfo.Department != "Finance" &&
                    newInfo.Department != "Management" && newInfo.Department != "Reception")
                    return BadRequest("错误的部门名称");
                string password = employee.Password;
                Employee.Add(newInfo.ID, newInfo.Name, newInfo.Gender, newInfo.Age, newInfo.Salary, newInfo.PhoneNum, newInfo.Department, password);
                return Ok("修改成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 管理部新增员工信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="newInfo">新增员工信息</param>
        /// <returns>是否增加成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult AddEmployeeInfor(string tokenValue, EmployeeInforDetailedWithoutID newInfo)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Management")
                {
                    return BadRequest("权限不符");
                }
                if (newInfo.Department != "Logistics" && newInfo.Department != "Finance" &&
                    newInfo.Department != "Management" && newInfo.Department != "Reception")
                    return BadRequest("错误的部门名称");
                long ID = Employee.NextID();
                string password = "123";
                Employee.Add(ID, newInfo.Name, newInfo.Gender, newInfo.Age, newInfo.Salary, newInfo.PhoneNum, newInfo.Department, password);
                return Ok("增加成功");
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
        /// <summary>
        /// 管理部删除员工的信息
        /// </summary>
        /// <param name="tokenValue">token</param>
        /// <param name="ID">要删除的员工ID</param>
        /// <returns>删除是否成功的提示</returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEmployeeInfor(string tokenValue, long ID)
        {
            try
            {
                EmployeeInforToken user = JWTHelper.GetUsers(tokenValue);
                if (user.Department != "Management")
                {
                    return BadRequest("权限不符");
                }
                if (Employee.Delete(ID) > 0)
                {
                    return Ok("删除成功");
                }
                else
                {
                    return NotFound("没有该员工");
                }
            }
            catch (OracleException oe)
            {
                return BadRequest("数据库请求出错" + oe.Number.ToString());
            }
        }
    }
}