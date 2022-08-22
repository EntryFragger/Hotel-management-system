ouusing Microsoft.AspNetCore.StaticFiles;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Utility;

namespace BackEnd.Model
{
    /*收支信息*/
    public class Account
    {
        public long AccountID { set; get; }//PK
        public string Adate { set; get; }
        public float Amount { set; get; }
        /*种类的取值只有两种:income/expenses*/
        public string Type { set; get; }

        /*根据收支的AccountID返回对应收支的所有信息*/
        public static Account Find(long ID)
        {
            Account ac = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Account WHERE AccountID = :AccountID",
                new OracleParameter(":AccountID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ac = dr.DtToModel<Account>();
            }
            return ac;
        }

        /*用于生成下一个ID*/
        public static long NextID()
        {
            long result = 0;
            Account ac = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(AccountID)  FROM Account");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                ac = dr.DtToModel<Account>();
                result = ac.AccountID;                
            }
            return result+1;
        }

        /*调用该函数将获取所有收支信息，无收支信息返回null*/
        public static List<Account> GetAllList()
        {
            List<Account> list = new List<Account>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Account");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Account>());
            }
            /*为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }


        /*创建新的收支信息，即一笔账*/
        /*创建合法性只能从账目ID考虑*/
        /*不成功为-1，否则不为-1*/
        public static int CreateAccount(long AID, string date, float amount, string type)
        {
            Account ac = Find(AID);
            if (ac == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Account(AccountID,Adate,Amount,Type) VALUES(:AccountID,:Adate,:Amount,:Type)",
                    new OracleParameter(":AccountID", AID),
                    new OracleParameter(":Date", date),
                    new OracleParameter(":Amount", amount),
                    new OracleParameter(":Type", type)
                    );
            }
            else
            {
                //throw new Exception("收支号重复");
                return -1;
            }
        }
    }
}
