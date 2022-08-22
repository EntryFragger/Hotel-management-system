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
    public class Vip
    {
        public int VipLv { set; get; }
        public float Discount { set; get; }
        public static Vip find(int VipLv)
        {
            Vip Vip = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Vip WHERE VipLv = :VipLv",
                new OracleParameter(":VipLv", VipLv)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Vip = dr.DtToModel<Vip>();
            }
            return Vip;
        }
        public static int AddVip(int VipLv,float Discount)//等级不能为非正数且不能超过10
        {
            Vip Vip = find(VipLv);
            if (Vip == null&& VipLv>0&&VipLv<=10)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Vip(VipLv,Discount) VALUES(:VipLv,:Discount)",
                    new OracleParameter(":VipLv", VipLv),
                    new OracleParameter(":Discount", Discount)
                    );
            }
            else
            {
                throw new Exception("等级已存在或等级不合理，无法添加");
            }
        }
        public static float SelectDiscount(int VipLv)//等级不能为非正数且不能超过10
        {
            float discount = 1;
            DataTable dt = DBHelper.ExecuteTable("SELECT Discount  FROM Vip WHERE VipLv = :VipLv",
                new OracleParameter(":VipLv", VipLv)
                );
            if (dt.Rows.Count > 0)
            {
                String dm = dt.Rows[0].ToString();
                discount = float.Parse(dm);
            }
            return discount;
        }
        public static int ChangeVip(int VipLv, float Discount)
        {
            Vip Vip = find(VipLv);
            if (Vip != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE Vip SET VipLv = :VipLv，Discount = :Discount  FROM Vip WHERE VipLv = :VipLv",
                   new OracleParameter(":VipLv", VipLv),
                    new OracleParameter(":Discount", Discount)
                   );
            }
            else
            {
               
                return -1;
            }
        }
        public static List<Vip> ListAll()
        {
            List<Vip> list = new List<Vip>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Vip");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Vip>());
            }
            return list;
        }

    }
}

