using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Utility;

namespace BackEnd.Model
{
    public class JWTHeader
    {
        /// <summary>
        /// 解密算法
        /// </summary>
        public string alg { get; set; } = "SHA256";

        /// <summary>
        /// TOKEN类型
        /// </summary>
        public string typ { get; set; } = "JWT";
        /// <summary>
        /// 过期时间戳
        /// </summary>
        public string expTime { get; set; } = DateTime.Now.AddHours(2).ToString();

    }

    public class JWTPayload
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 用户所属部门
        /// </summary>
        public string Department { get; set; }
    }

}
