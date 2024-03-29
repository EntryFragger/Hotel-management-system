using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BackEnd.Utility;

namespace BackEnd.Model
{
    public class Maintenance
    {
        public string ItemId { set; get; }
        public long EmployeeId { set; get; }
        public string mDate { set; get; }
        public string ItemName { set; get; }
        public static Maintenance Find(string itemID)
        {
            Maintenance instance = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM MAINTENANCE WHERE ItemID = :ItemID",
                new OracleParameter(":ItemID", itemID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                instance = new Maintenance()
                {
                    ItemId = dr["ITEMID"].ToString(),
                    EmployeeId = long.Parse(dr["EMPLOYEEID"].ToString()),
                    mDate = dr["MDATE"].ToString(),
                    ItemName = dr["ITEMNAME"].ToString()
                };
            }
            return instance;
        }
        public static List<Maintenance> GetList()
        {
            List<Maintenance> list = new List<Maintenance>();
            DataTable dt = DBHelper.ExecuteTable("SELECT mDate,ItemID,EmployeeID,ItemName FROM MAINTENANCE");
            foreach (DataRow dr in dt.Rows)
                list.Add(new Maintenance()
                {
                    ItemId = dr["ITEMID"].ToString(),
                    EmployeeId = long.Parse(dr["EMPLOYEEID"].ToString()),
                    mDate = dr["MDATE"].ToString(),
                    ItemName = dr["ITEMNAME"].ToString()
                });
            return list;
        }
        public static int Add(string itemID, long employeeID, string mdate, string itemName)
        {
            return DBHelper.ExecuteNonQuery("INSERT INTO MAINTENANCE(ItemID,EmployeeID, mDate,ItemName)" +
                "VALUES(:ItemID,:EmployeeID, :mDate,:ItemName) ",
              new OracleParameter(":ItemID", itemID),
              new OracleParameter(":EmployeeID", employeeID),
              new OracleParameter(":Date", mdate),
              new OracleParameter(":ItemName", itemName)
              );
        }
    }
}
