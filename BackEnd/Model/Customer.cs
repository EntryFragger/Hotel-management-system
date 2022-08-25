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
    public class Customer
    {
        public string CustomerID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string PhoneNum { set; get; }
        public string Area { set; get; }
        public int VipLv { set; get; }

        public static Customer Find(string ID)
        {
            Customer Customer = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Customer WHERE CustomerID = :CustomerID",
                new OracleParameter(":CustomerID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                Customer = new Customer()
                {
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    Name = dr["NAME"].ToString(),
                    Gender = dr["GENDER"].ToString(),
                    PhoneNum = dr["PHONENUM"].ToString(),
                    Area = dr["AREA"].ToString(),
                    VipLv = int.Parse(dr["VIPLV"].ToString())
                };
            }
            return Customer;
        }
        public static int AddCustomer(string ID, string Name, string Gender, string PhoneNum, string Area, int VipLv)//添加顾客
        {
            Customer Customer = Find(ID);
            if (Customer == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Customer(CustomerID,Name,Gender,PhoneNum,Area,VipLv) VALUES(:CustomerID,:Name,:Gender,:PhoneNum,:Area,:VipLv)",
                    new OracleParameter(":CustomerID", ID),
                    new OracleParameter(":Name", Name),
                    new OracleParameter(":Gender", Gender),
                    new OracleParameter(":PhoneNum", PhoneNum),
                    new OracleParameter(":Area", Area),
                     new OracleParameter(":VipLv", VipLv)
                    );
            }
            else
            {
                //throw new Exception("顾客已存在，无法添加");
                return -1;
            }
        }
        /*  public static int DeleteCustomer(string ID)
          {
              return DBHelper.ExecuteNonQuery("DELETE FROM Customer WHERE CustomerID = :CustomerID",
                  new OracleParameter(":CustomerID", ID)
                  );
          }*/
        public static int ChangeVip(string ID, int VipLv)//修改vip等级
        {
            return DBHelper.ExecuteNonQuery("UPDATE Customer SET CustomerID = :CustomerID,VipLv=:VipLv  FROM Customer WHERE CustomerID = :CustomerID",
                   new OracleParameter(":CustomerID", ID),
                     new OracleParameter(":VipLv", VipLv)
                );
        }
        public static int ChangeCustomer(string ID, string Name, string Gender, string PhoneNum, string Area, int VipLv)
        {
            Customer Customer = Find(ID);
            if (Customer != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE Customer SET CustomerID = :CustomerID,Name = :Name,Gender = :Gender,PhoneNum = :PhoneNum ,VipLv=:VipLv WHERE CustomerID = :CustomerID",
                   new OracleParameter(":CustomerID", ID),
                    new OracleParameter(":Name", Name),
                    new OracleParameter(":Gender", Gender),
                    new OracleParameter(":PhoneNum", PhoneNum),
                    new OracleParameter(":Area", Area),
                    new OracleParameter(":VipLv", VipLv)
                   );
            }
            else
            {
                return -1;
            }
        }
        public static List<Customer> ListAll()
        {
            List<Customer> list = new List<Customer>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Customer");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Customer()
                {
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    Name = dr["NAME"].ToString(),
                    Gender = dr["GENDER"].ToString(),
                    PhoneNum = dr["PHONENUM"].ToString(),
                    Area = dr["AREA"].ToString(),
                    VipLv = int.Parse(dr["VIPLV"].ToString())
                });
            }
            return list;
        }
        public static int FindVip(string ID)//查询某个顾客vip等级
        {
            int vip = 0;
            DataTable dt = DBHelper.ExecuteTable("SELECT VipLv FROM Customer WHERE CustomerID = :CustomerID",
                new OracleParameter(":CustomerID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                String dm = dt.Rows[0].ToString();
                vip = int.Parse(dm);
            }
            return vip;
        }
        public class CustomerInfo
        {
            public string CustomerID { set; get; }//PK
            public string Name { set; get; }
            public string Gender { set; get; }
            public string PhoneNum { set; get; }
            public string Area { set; get; }
        }
    }

}
