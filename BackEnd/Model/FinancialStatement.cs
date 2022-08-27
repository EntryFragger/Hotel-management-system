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
        public long EmployeeID { set; get; }
        public long StatementID { set; get; }
        public string StatementContent { set; get; }
        public float Amount { set; get; }
        public string State { set; get; }
        public static long NextStatementID()
        {
            long MaxID = -1;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(StatementID) AS STATEMENTID FROM FINANCIALSTATEMENT ");
            if (GetListAll()==null)
                return 1;
            else
            {
                DataRow dr = dt.Rows[0];
                MaxID = long.Parse(dt.Rows[0]["STATEMENTID"].ToString()) + 1;
            }
            return MaxID;
        }
        public static List<FinancialStatement> GetList(long EmployeeID)
        {
            List<FinancialStatement> list = new List<FinancialStatement>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM FINANCIALSTATEMENT WHERE EmployeeID=:EmployeeID",
                new OracleParameter(":EmployeeID", EmployeeID)
                );
            foreach (DataRow dr in dt.Rows)
                list.Add(new FinancialStatement()
                {
                    EmployeeID = long.Parse(dr["EMPLOYEEID"].ToString()),
                    StatementID = long.Parse(dr["STATEMENTID"].ToString()),
                    StatementContent = dr["STATEMENTCONTENT"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString()),
                    State = dr["STATE"].ToString(),
                });
            return list;
        }
        public static List<FinancialStatement> GetListAll()
        {
            List<FinancialStatement> list = new List<FinancialStatement>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM FINANCIALSTATEMENT ");
            foreach (DataRow dr in dt.Rows)
                list.Add(new FinancialStatement()
                {
                    EmployeeID = long.Parse(dr["EMPLOYEEID"].ToString()),
                    StatementID = long.Parse(dr["STATEMENTID"].ToString()),
                    StatementContent = dr["STATEMENTCONTENT"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString()),
                    State = dr["STATE"].ToString(),
                });
            if (!list.Any())
                return null;
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
                financialStatement = new FinancialStatement()
                {
                    EmployeeID = long.Parse(dr["EMPLOYEEID"].ToString()),
                    StatementID = long.Parse(dr["STATEMENTID"].ToString()),
                    StatementContent = dr["STATEMENTCONTENT"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString()),
                    State = dr["STATE"].ToString(),
                };
            }
            return financialStatement;

        }
        public static int Add(long employeeID, long statementID, string statementcontent, float amount, string state)
        {
            FinancialStatement financialStatement = Find(statementID);
            if (financialStatement != null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM FINANCIALSTATEMENT WHERE StatementID = :StatementID",
                   new OracleParameter(":StatementID", statementID)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(EmployeeID,StatementID,StatementContent,Amount,State)" +
                "VALUES(:EmployeeID,:StatementID,:StatementContent,:Amount,:State) ",
                  new OracleParameter(":EmployeeID", employeeID),
                  new OracleParameter(":StatementID", statementID),
                  new OracleParameter(":StatementContent", statementcontent),
                  new OracleParameter(":Amount", amount),
                  new OracleParameter(":State", state)
                  );
            }
            return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(EmployeeID,StatementID,StatementContent,Amount,State)" +
                 "VALUES(:EmployeeID,:StatementID,:StatementContent,:Amount,:State) ",
                   new OracleParameter(":EmployeeID", employeeID),
                   new OracleParameter(":StatementID", statementID),
                   new OracleParameter(":StatementContent", statementcontent),
                   new OracleParameter(":Amount", amount),
                   new OracleParameter(":State", state)
                   );
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
                DBHelper.ExecuteNonQuery("DELETE FROM FinancialStatement WHERE StatementID = :StatementID",
                   new OracleParameter(":ORDERID", f_id)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO FINANCIALSTATEMENT(EmployeeID,StatementID,StatementContent,Amount,State)" +
                     "VALUES(:EmployeeID,:StatementID,:StatementContent,:Amount,:State) ",
                       new OracleParameter(":EmployeeID", financialstatement.EmployeeID),
                       new OracleParameter(":StatementID", f_id),
                       new OracleParameter(":StatementContent", financialstatement.StatementContent),
                       new OracleParameter(":Amount", financialstatement.Amount),
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