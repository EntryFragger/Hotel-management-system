using Microsoft.AspNetCore.StaticFiles;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Utility;
namespace BackEnd.Model
{
    public class VIP
    {
        public int VipLv { set; get; }
        public float Discount { set; get; }
        public static VIP find(int VipLv)
        {
            VIP VIP = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM VIP WHERE VipLv = :VipLv",
                new OracleParameter(":VipLv", VipLv)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                VIP = dr.DtToModel<VIP>();
            }
            return VIP;
        }
        public static int AddVIP(int VipLv,float Discount)//等级不能为非正数且不能超过10
        {
            VIP VIP = find(VipLv);
            if (VIP == null&& VipLv>0&&VipLv<=10)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO VIP(VipLv,Discount) VALUES(:VipLv,:Discount)",
                    new OracleParameter(":VipLv", VipLv),
                    new OracleParameter(":Discount", Discount)
                    );
            }
            else
            {
                throw new Exception("等级已存在或等级不合理，无法添加");
            }
        }
        public static int DeleteVIP(int VipLv)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM VIP WHERE VipLv = :VipLv",
                new OracleParameter(":VipLv", VipLv)
                );
        }
        public static int ChangeVIP(int VipLv, float Discount)
        {
            VIP VIP = find(VipLv);
            if (VIP != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE VIP SET VipLv = :VipLv，Discount = :Discount WHERE VipLv = :VipLv",
                   new OracleParameter(":VipLv", VipLv),
                    new OracleParameter(":Discount", Discount)
                   );
            }
            else
            {
               
                return -1;
            }
        }
        public static List<VIP> ListAll()
        {
            List<VIP> list = new List<VIP>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM VIP");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<VIP>());
            }
            return list;
        }

    }
}
