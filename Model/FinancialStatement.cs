using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BackEnd.Utility;

namespace BackEnd.Model
{
    public class FinancialStatement
    {
        public string ID { set; get; }
        public long StatementID { set; get; }
        public string StatementContent { set; get; }
        public long Amount { set; get; }
        public string State { set; get; }
        public static long NextStatementID()
        {
            long result = 0;
            FinancialStatement ac = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(StatementID)  FROM FinancialStatement");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ac = dr.DtToModel<FinancialStatement>();
                result = ac.StatementID;
            }
            return result + 1;
        }
        public static List<FinancialStatement> GetList(string ID)
        {
            List<FinancialStatement> list = new List<FinancialStatement>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM FINANCIALSTATEMENT WHERE ID=:ID",
                new OracleParameter(":ID", ID)
                );
            foreach (DataRow dr in dt.Rows)
                list.Add(dr.DtToModel<FinancialStatement>());
            return list;
        }
        public static FinancialStatement Find(long statementID)
        {
            FinancialStatement financialStatement = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM FINANCIALSTATEMENT WHERE StatementID=:StatementID",
                new OracleParameter(":StatementID", statementID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                financialStatement = dr.DtToModel<FinancialStatement>();
            }
            return financialStatement;

        }
        /// <summary>
        /// 将财务报单的状态从未通过修改为已通过
        /// </summary>
        /// <param name="f_id"></param>
        /// <returns></returns>
        public static int Change_FinicalStatement_Status(long f_id)
        {
            string status = "passed";
            FinancialStatement financialstatement = Find(f_id);
            if (financialstatement != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE FinancialStatement SET State = :State WHERE StatementID = :StatementID",
                    new OracleParameter(":StatementID", f_id),
                    new OracleParameter(":State", status)
                    );
            }
            else
            {
                //throw new Exception("房间服务不存在，无法修改");
                return -1;
            }
        }
    }
    
}
