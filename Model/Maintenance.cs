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
        public string EmployeeId { set; get; }
        public string Date { set; get; }
        public string ItemName { set; get; }
        public static List<Maintenance> GetList()
        {
            List<Maintenance> list = new List<Maintenance>();
            DataTable dt = DBHelper.ExecuteTable("SELECT Date,ItemID,EmployeeID,ItemName FROM MAINTENANCE");
            foreach (DataRow dr in dt.Rows)
                list.Add(dr.DtToModel<Maintenance>());
            return list;
        }
        public static int Add(string itemID, string employeeID, string date, string itemName)
        {
            return DBHelper.ExecuteNonQuery("INSERT INTO MAINTENANCE(ItemID,EmployeeID, Date,ItemName)" +
                "VALUES(:ItemID,:EmployeeID, :Date,:ItemName) ",
              new OracleParameter(":ItemID", itemID),
              new OracleParameter(":EmployeeID", employeeID),
              new OracleParameter(":Date", date),
              new OracleParameter(":ItemName", itemName)
              );
        }
    }
}
