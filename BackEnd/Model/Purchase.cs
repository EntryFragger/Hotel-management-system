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
        public long PurchaseID { set; get; }//PK
        public string GoodsName { set; get; }

        /*计量单位*/
        public string Unit { set; get; }

        public string Quantity { set; get; }
        public float Price { set; get; }
        public string Pdate { set; get; }

        /*根据订单的PurchaseID返回对应订单的所有信息*/
        public static Purchase Find(long ID)
        {
            Purchase o = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Purchase WHERE PurchaseID = :PurchaseID",
                new OracleParameter(":PurchaseID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                o = new Purchase()
                {
                    PurchaseID = long.Parse(dr["PURCHASEID"].ToString()),
                    GoodsName = dr["GOODSNAME"].ToString(),
                    Unit = dr["UNIT"].ToString(),
                    Quantity = dr["QUANTITY"].ToString(),
                    Price = float.Parse(dr["PRICE"].ToString()),
                    Pdate = dr["PDATE"].ToString(),
                };
            }
            return o;
        }

        /*调用该函数将获取所有收购的所有信息，无收购返回null*/
        public static List<Purchase> GetAllList()
        {
            List<Purchase> list = new List<Purchase>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Purchase");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new Purchase()
                {
                    PurchaseID = long.Parse(dr["PURCHASEID"].ToString()),
                    GoodsName = dr["GOODSNAME"].ToString(),
                    Unit = dr["UNIT"].ToString(),
                    Quantity = dr["QUANTITY"].ToString(),
                    Price = float.Parse(dr["PRICE"].ToString()),
                    Pdate = dr["PDATE"].ToString(),
                });
            }
            if (!list.Any())
            {
                return null;
            }
            return list;
        }

        /*用于生成下一个主码ID*/
        public static long NextID()
        {
            long result = 0;
            Purchase ac = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(PurchaseID) AS PURCHASEID  FROM Purchase");
            if (GetAllList() == null)
                return 1;
            else
            {
                DataRow dr = dt.Rows[0];
                result = long.Parse(dr["PURCHASEID"].ToString()) + 1;
            }
            return result;
        }

        /*创建新的库存收购信息*/
        public static int CreatePurchase(long PID, string goodsname, string unit, string quantity, float price, string date)
        {
            /*只能根据id考察合法性*/
            Purchase pr = Find(PID);
            if (pr == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Purchase(PurchaseID,GoodsName,Unit,Quantity,Price,Pdate) VALUES(:PurchaseID,:GoodsName,:Quantity,:Unit,:Price,:Pdate)",
                    new OracleParameter(":PurchaseID", PID),
                    new OracleParameter(":GoodsName", goodsname),
                    new OracleParameter(":Unit", unit),
                    new OracleParameter(":Quantity", quantity),
                    new OracleParameter(":Price", price),
                    new OracleParameter(":Pdate", date)
                    );
            }
            else
            {
                //throw new Exception("收支信息ID冲突无法添加");
                return -1;
            }
        }
        /*删除一个元组*/
        public static int DeleteAccount(string ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM Purchase WHERE PurchaseID = :PurchaseID",
            new OracleParameter(":PurchaseID", ID)
            );
        }
    }
}
