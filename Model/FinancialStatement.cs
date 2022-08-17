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
        public string StatementId { set; get; }
        public string StatementContent { set; get; }
        public string Amount { set; get; }
        public string State { set; get; }
        public static List<FinancialStatement> GetList()
        {
            List<FinancialStatement> list = new List<FinancialStatement>();
            DataTable dt = DBHelper.ExecuteTable("SELECT StatementId,StatementContent,Amount,State FROM FINANCIALSTATEMENT");
            foreach (DataRow dr in dt.Rows)
                list.Add(dr.DtToModel<FinancialStatement>());
            return list;
        }
        public static FinancialStatement Find(string statementID)
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
        public static int Add(string statementID, string statementcontent, string amount, string state)
        {
            FinancialStatement financialStatement = Find(statementID);
            if(financialStatement!=null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM FINANCIALSTATEMENT WHERE StatementID = :StatementID",
                   new OracleParameter(":StatementID", statementID)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(StatementID,StatementContent,Amount,State)" +
                "VALUES(:StatementID,:StatementContent,:Amount,:State) ",
                  new OracleParameter(":StatementID", statementID),
                  new OracleParameter(":StatementContent", statementcontent),
                  new OracleParameter(":Amount", amount),
                  new OracleParameter(":State", state)
              );
            }
            return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(StatementID,StatementContent,Amount,State)" +
                "VALUES(:StatementID,:StatementContent,:Amount,:State) ",
              new OracleParameter(":StatementID", statementID),
              new OracleParameter(":StatementContent", statementcontent),
              new OracleParameter(":Amount", amount),
              new OracleParameter(":State", state)
              );
        }
    }
    
}
