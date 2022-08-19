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
        public string StatementID { set; get; }
        public string StatementContent { set; get; }
        public long Amount { set; get; }
        public string State { set; get; }
        public static string NextStatementID()
        {
            string MaxID = "";
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(StatementID) FROM FINANCIALSTATEMENT ");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                MaxID = (int.Parse(dt.Rows[0]["STATEMENTID"].ToString()) + 1).ToString();
            }
            return MaxID;
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
        public static int Add(string ID,string statementID, string statementcontent, long amount, string state)
        {
            FinancialStatement financialStatement = Find(statementID);
            if(financialStatement!=null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM FINANCIALSTATEMENT WHERE StatementID = :StatementID",
                   new OracleParameter(":StatementID", statementID)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(ID,StatementID,StatementContent,Amount,State)" +
                "VALUES(:ID,:StatementID,:StatementContent,:Amount,:State) ",
                  new OracleParameter(":ID", ID),
                  new OracleParameter(":StatementID", statementID),
                  new OracleParameter(":StatementContent", statementcontent),
                  new OracleParameter(":Amount", amount),
                  new OracleParameter(":State", state)
                  );
            }
            return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(ID,StatementID,StatementContent,Amount,State)" +
                 "VALUES(:ID,:StatementID,:StatementContent,:Amount,:State) ",
                   new OracleParameter(":ID", ID),
                   new OracleParameter(":StatementID", statementID),
                   new OracleParameter(":StatementContent", statementcontent),
                   new OracleParameter(":Amount", amount),
                   new OracleParameter(":State", state)
                   );
        }
    }
    
}
