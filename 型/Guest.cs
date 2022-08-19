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
    public class Guest
    {
        public string CustomerID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string PhoneNum { set; get; }
        public string Area { set; get; }
        public int VipLv { set; get; }
        public static Guest find(string ID)
        {
            Guest guest = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM guest WHERE CustomerID = :CustomerID",
                new OracleParameter(":CustomerID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                guest = dr.DtToModel<Guest>();
            }
            return guest;
        }
        public static int Addguest(string ID, string Name, string gender, string PhoneNum,string Area,int VipLv)
        {
            Guest guest = find(ID);
            if (guest == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO guest(CustomerID,Name,gender,PhoneNum,Area,VipLv) VALUES(:CustomerID,:Name,:gender,:PhoneNum,:Area,:VipLv)",
                    new OracleParameter(":CustomerID", ID),
                    new OracleParameter(":Name", Name),
                    new OracleParameter(":gender", gender),
                    new OracleParameter(":PhoneNum", PhoneNum),
                    new OracleParameter(":Area", Area),
                     new OracleParameter(":VipLv", VipLv)
                    );
            }
            else
            {
                throw new Exception("顾客已存在，无法添加");
                return 0;
            }
        }
        public static int Deleteguest(string ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM guest WHERE CustomerID = :CustomerID",
                new OracleParameter(":CustomerID", ID)
                );
        }
        public static int Changeguest(string ID,string Name, string gender, string PhoneNum, string Area, int VipLv)
        {
            Guest guest = find(ID);
            if(guest != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE guest SET CustomerID = :CustomerID,Name = :Name,gender = :gender,PhoneNum = :PhoneNum WHERE,VipLv=:VipLv CustomerID = :CustomerID",
                   new OracleParameter(":CustomerID", ID),
                    new OracleParameter(":Name", Name),
                    new OracleParameter(":gender", gender),
                    new OracleParameter(":PhoneNum", PhoneNum),
                    new OracleParameter(":Area", Area),
                    new OracleParameter(":VipLv", VipLv)
                   ); 
            }
            else
            {
                throw new Exception("顾客不存在，无法修改");
                return 0;
            }
        }
        public static List<Guest> ListAll()
        {
            List<Guest> list = new List<Guest>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM guest");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Guest>());
            }
            return list;
        }

    }
}
