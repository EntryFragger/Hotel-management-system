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
        public long ID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string PhoneNum { set; get; }
        public string Salary { set; get; }
        public string Department { set; get; }
        //共四种类型：Logistics（后勤部）Finance（财务部）Management（管理部）Reception（前台） 
        public string Password { set; get; }
        public static long NextID()
        {
            long MaxID = -1;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(ID) FROM EMPLOYEE");
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                MaxID = long.Parse(dt.Rows[0]["ID"].ToString()) + 1;
            }
            return MaxID;
        }
        public static Employee Find(long ID)
        {
            Employee instance = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM EMPLOYEE WHERE ID = :ID",
                new OracleParameter(":ID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                instance = dt.Rows[0].DtToModel<Employee>();
            }
            return instance;
        }
        public static EmployeeInforDetailed GetInforDetailed(long ID)
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
        public static int Add(long ID, string name, string gender, string age, string salary, string phonenum, string department, string password)
        {
            Employee employee = Employee.Find(ID);
            if (employee != null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM EMPLOYEE WHERE ID = :ID",
                  new OracleParameter(":ID", ID)
                  );
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
        public static EmployeeInforSimple QueryByID(long ID)
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
        public static int Delete(long ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM EMPLOYEE WHERE ID = :ID",
                new OracleParameter(":ID", ID)
                );
        }

        public static int ChangePassword(long ID, string password)
        {
            return DBHelper.ExecuteNonQuery("UPDATE EMPLOYEE SET Password=:Password WHERE ID = :ID",
                new OracleParameter(":ID", ID),
                new OracleParameter(":Password", password)
                );
        }
        public static List<EmployeeInforSimple> GetAllSimple()
        {
            List<EmployeeInforSimple> list = new List<EmployeeInforSimple>();
            DataTable dt = DBHelper.ExecuteTable("SELECT ID,Name,Department FROM EMPLOYEE");
            foreach (DataRow dr in dt.Rows)
                list.Add(dr.DtToModel<EmployeeInforSimple>());
            return list;
        }
    }
    public class EmployeeInforSimple
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Department { set; get; }
    }
    public class EmployeeInforDetailed
    {
        public long ID { set; get; }
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string Salary { set; get; }
        public string PhoneNum { set; get; }
        public string Department { set; get; }
    }
    public class EmployeeInforToken
    {
        public long ID { set; get; }
        public string Department { set; get; }
    }
    public class EmployeeInforDetailedWithoutID
    {
        public string Name { set; get; }
        public string Gender { set; get; }
        public string Age { set; get; }
        public string Salary { set; get; }
        public string PhoneNum { set; get; }
        public string Department { set; get; }
    }

}