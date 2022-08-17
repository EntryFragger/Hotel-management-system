using Oracle.ManagedDataAccess.Client;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Utility
{
    public class DBHelper
    {
        public static string Constr { set; get; }
        public static DataTable ExecuteTable(string comText, params OracleParameter[] oracleParameters)
        {
            using (OracleConnection conn = new OracleConnection(Constr))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(comText, conn);
                cmd.Parameters.AddRange(oracleParameters);
                OracleDataAdapter sda = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds.Tables[0];
            }
        }
        public static int ExecuteNonQuery(string cmdText, params OracleParameter[] oracleParameters)
        {
            using (OracleConnection conn = new OracleConnection(Constr))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(cmdText, conn);
                cmd.Parameters.AddRange(oracleParameters);
                int rows = cmd.ExecuteNonQuery();
                if (rows <= 0)
                {
                    throw new Exception("数据库操作有错误");
                }
                return rows;
            }
        }
    }
}
