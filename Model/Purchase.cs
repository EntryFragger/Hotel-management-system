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
    /*采购订单类，存储物资采购的信息*/
    public class Purchase
    {
        public string PurchaseID { set; get; }//PK
        public string GoodsName { set; get; }

        /*计量单位*/
        public string Unit { set; get; }

        public string Quantity { set; get; }
        public int Price { set; get; }
        public string Date { set; get; }

        /*根据订单的PurchaseID返回对应订单的所有信息*/
        public static Purchase Find(string ID)
        {
            Purchase order = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Purchase WHERE PurchaseID = :PurchaseID",
                new OracleParameter(":PurchaseID", ID)
                );
            if (dt.Rows.Count() > 0)
            {
                DataRow dr = dt.Rows[0];
                order = dr.DtToModel<Purchase>();
            }
            return order;
        }

        /*调用该函数将获取所有收购的所有信息，无收购返回null*/
        public static List<Purchase> GetAllList()
        {
            List<Purchase> list = new List<Purchase>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Purchase",);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Order>());
            }
            /*为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

 

        /*创建新的库存收购信息*/
        public static int CreateAccount(string PID, string goodsname, string unit, string quantity, int price, string date)
        {
            /*只能根据id考察合法性*/
            Purchase pr= Find()
            if (pr==null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Account(PurchaseID,GoodsName,Unit,Quantity,Price,Date) VALUES(:PurchaseID,:GoodsName,:Unit,:Price,:Date)",
                    new OracleParameter(":PurchaseID", PID),
                    new OracleParameter(":GoodsName", goodsname),
                    new OracleParameter(":Unit", unit),
                    new OracleParameter(":Quantity", quantity)
                    new OracleParameter(":Price", price),
                    new OracleParameter(":Date", date),
                    );
            }
            else
            {
                throw new Exception("收支信息ID冲突无法添加");
                return 0;
            }
        }
        /*删除一个元组*/
        public static int DeleteAccount(string ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM Account WHERE PurchaseID = :PurchaseID",
            new OracleParameter(":PurchaseID", ID)
            );
        }
}
