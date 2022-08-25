using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BackEnd.Utility;

namespace BackEnd.Model
{
    public class Stock
    {
        public string Name { set; get; }
        public string Unit { set; get; }
        public string Quantity { set; get; }
        public static List<Stock> GetList()
        {
            List<Stock> list = new List<Stock>();
            DataTable dt = DBHelper.ExecuteTable("SELECT* FROM STOCK");
            foreach (DataRow dr in dt.Rows)
                list.Add(new Stock()
                {
                    Name = dr["NAME"].ToString(),
                    Unit = dr["UNIT"].ToString(),
                    Quantity = dr["QUANTITY"].ToString()
                });
            return list;
        }
        public static int Delete(string name)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM STOCK WHERE Name = :Name",
                new OracleParameter(":Name", name)
                );
        }
        public static Stock Find(string name)
        {
            Stock instance = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM STOCK WHERE Name = :Name",
                new OracleParameter(":Name", name)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                instance = new Stock()
                {
                    Name = dr["NAME"].ToString(),
                    Unit = dr["UNIT"].ToString(),
                    Quantity = dr["QUANTITY"].ToString()
                };
            }
            return instance;
        }
        public static int Add(string name, string unit, string quantity)
        {
            Stock stock = Find(name);
            if (stock != null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM STOCK WHERE Name = :Name",
                   new OracleParameter(":Name", name)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO STOCK(Name,Unit,Quantity)" +
                "VALUES(:Name,:Unit,:Quantity) ",
                  new OracleParameter(":Name", name),
                  new OracleParameter(":Unit", unit),
                  new OracleParameter(":Quantity", quantity)
                  );
            }
            return DBHelper.ExecuteNonQuery("INSERT INTO STOCK(Name,Unit,Quantity)" +
                "VALUES(:Name,:Unit,:Quantity) ",
                  new OracleParameter(":Name", name),
                  new OracleParameter(":Unit", unit),
                  new OracleParameter(":Quantity", quantity)
                  );
        }

    }
}
