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

namespace BackEnd.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateRoomPurchaseController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Purchase_Create(string purchase_id, string goods_name, string quantity,long price,string date,string unit)
        {
            if(purchase_id.Trim().Length == 0 || goods_name.Trim().Length == 0 || quantity.Trim().Length == 0 || date.Trim().Length == 0||price<0|| unit.Trim().Length == 0)
            {
                return BadRequest("输入信息不完整");
            }
            int issuccess = Purchase.CreatePurchase(purchase_id, goods_name, unit, quantity, price, date);
            if(issuccess != -1)
            {
                return Ok("收购信息创建成功");
            }
            else
            {
                return NotFound("收购信息创建失败");
            }
        }
    }
}
