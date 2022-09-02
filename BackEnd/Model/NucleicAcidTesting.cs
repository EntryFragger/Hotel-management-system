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
    public class NucleicAcidTesting
    {
        public long EmployeeID { set; get; }
        public string Department { set; get; }
        public string nDate { set; get; }
        public string Result { set; get; }
        public string Name { set; get; }

        public static List<NucleicAcidTesting>ListAll()
        {
            List<NucleicAcidTesting> list = new List<NucleicAcidTesting>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM NUCLEICACIDTESTING");
            foreach (DataRow dr in dt.Rows)
                list.Add(new NucleicAcidTesting()
                {
                    EmployeeID=long.Parse(dr["EMPLOYEEID"].ToString()),
                    Department = dr["DEPARTMENT"].ToString(),
                    nDate = dr["NDATE"].ToString(),
                    Result = dr["RESULT"].ToString(),
                    Name = dr["NAME"].ToString()
                });
            return list;
        }

        public static List<NucleicAcidTesting>QueryByDep(string department)
        {
            List<NucleicAcidTesting> list = new List<NucleicAcidTesting>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM NUCLEICACIDTESTING WHERE Department=:Department",
                new OracleParameter(":Department", department)
                );
            foreach (DataRow dr in dt.Rows)
                list.Add(new NucleicAcidTesting()
                {
                    EmployeeID = long.Parse(dr["EMPLOYEEID"].ToString()),
                    Department = dr["DEPARTMENT"].ToString(),
                    nDate = dr["NDATE"].ToString(),
                    Result = dr["RESULT"].ToString(),
                    Name = dr["NAME"].ToString()
                });
            return list;
        }
        public static List<NucleicAcidTesting> QueryByID(long ID)
        {
            List<NucleicAcidTesting> list = new List<NucleicAcidTesting>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM NUCLEICACIDTESTING WHERE EmployeeID=:EmployeeID",
                new OracleParameter(":EmployeeID", ID)
                );
            foreach (DataRow dr in dt.Rows)
                list.Add(new NucleicAcidTesting()
                {
                    EmployeeID = long.Parse(dr["EMPLOYEEID"].ToString()),
                    Department = dr["DEPARTMENT"].ToString(),
                    nDate = dr["NDATE"].ToString(),
                    Result = dr["RESULT"].ToString(),
                    Name = dr["NAME"].ToString()
                });
            return list;
        }

        public static int Add(long employeeID,string department,string date,string result,string name)
        {
            return DBHelper.ExecuteNonQuery("INSERT INTO NucleicAcidTesting(EmployeeID,Department,nDate,Result,Name) " +
                "VALUES(:EmployeeID,:Department,:nDate,:Result,:Name)",
                  new OracleParameter(":EmployeeID", employeeID),
                  new OracleParameter(":Department", department),
                  new OracleParameter(":nDate", date),
                  new OracleParameter(":Result", result),
                  new OracleParameter(":Name", name)
                  );
        }
    }
}
