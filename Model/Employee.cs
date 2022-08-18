using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BackEnd.Utility;

namespace BackEnd.Model
{
    public class Employee
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string PhoneNum { set; get; }
        public string Salary { set; get; }
        public string Department { set; get; }
        //共四种类型：Logistics（后勤部）Finance（财务部）Management（管理部）Reception（前台） 
        public string Password { set; get; }
        public static EmployeeInforDetailed Find(string ID)
        {
            EmployeeInforDetailed instance = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT ID,Name,Gender,Age,Salary,PhoneNum,Department FROM EMPLOYEE WHERE ID = :ID",
                new OracleParameter(":ID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                instance = dt.Rows[0].DtToModel<EmployeeInforDetailed>();
            }
            return instance;
        }
         public static int Add(string ID, string name, string gender, string age, string salary, string phonenum,string department,string password)
        {
            return DBHelper.ExecuteNonQuery("INSERT INTO EMPLOYEE(ID,Name,Gender,Age,PhoneNum,Salary,Department,Password)" +
                "VALUES(:ID,:Name,:Gender,:Age,:PhoneNum,:Salary,:Department,:Password) ",
              new OracleParameter(":ID", ID),
              new OracleParameter(":Name", name),
              new OracleParameter(":Gender", gender),
              new OracleParameter(":Age", age),
              new OracleParameter(":Salary", salary),
              new OracleParameter(":PhoneNum", phonenum),
              new OracleParameter(":Department", department),
              new OracleParameter(":Password", password)
              );
        }
        public static EmployeeInforSimple QueryByID(string ID)
        {
            EmployeeInforSimple employee = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT ID,Name,Department  FROM EMPLOYEE WHERE ID = :ID",
               new OracleParameter(":ID", ID)
               );
            if (dt.Rows.Count > 0)
                employee = dt.Rows[0].DtToModel<EmployeeInforSimple>();
            return employee;
        }
        public static List<EmployeeInforSimple> QueryByDep(string department)
        {
            List<EmployeeInforSimple> list = new List<EmployeeInforSimple>();
            DataTable dt = DBHelper.ExecuteTable("SELECT ID,Name,Department FROM EMPLOYEE WHERE Department = :Department",
                 new OracleParameter(":Department", department)
                );
            foreach (DataRow dr in dt.Rows)
                list.Add(dr.DtToModel<EmployeeInforSimple>());
            return list;
        }
        public static int Delete(string ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM EMPLOYEE WHERE ID = :ID",
                new OracleParameter(":ID", ID)
                );
        }
    }
    public class EmployeeInforSimple
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Department { set; get; }
    }
    public class EmployeeInforDetailed
    {
        public string ID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string Salary { set; get; }
        public string PhoneNum { set; get; }
        public string Department { set; get; }
    }
    public class EmployeeInforToken
    {
        public string ID { set; get; }
        public string Department { set; get; }
    }



}
