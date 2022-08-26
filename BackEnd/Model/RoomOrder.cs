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
    public class RoomOrder
    {
        /*需要后端随机生成的ID都是long，不需要随机生成的ID都是string*/
        public long OrderID { set; get; }//PK
        public string RoomID { set; get; }
        public string CustomerID { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }
        public long Days { set; get; }//入住天数

        /*status取值 ed是已经完成 ing是正在进行*/
        public string OrderStatus { set; get; }
        public string Violation { set; get; }
        public float Amount { set; get; }

        /*根据订单的OrderId返回对应订单的所有信息*/
        public static RoomOrder Find(long ID)
        {
            RoomOrder room_order = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM ROOMORDER WHERE OrderID = :OrderID",
                new OracleParameter(":OrderID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                room_order = new RoomOrder()
                {
                    OrderID=long.Parse(dr["ORDERID"].ToString()),
                    RoomID = dr["ROOMID"].ToString(),
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    StartTime = dr["STARTTIME"].ToString(),
                    EndTime = dr["ENDTIME"].ToString(),
                    Days = long.Parse(dr["DAYS"].ToString()),
                    OrderStatus = dr["ORDERSTATUS"].ToString(),
                    Violation = dr["VIOLATION"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString())
                };
            }
            return room_order;
        }

        /*用于生成下一个ID*/
        public static long NextID()
        {
            long result = 0;
            RoomOrder ac = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT MAX(OrderID) AS ORDERID  FROM ROOMORDER");
            if (GetAllList() == null)
                return 1;
            else
            {
                DataRow dr = dt.Rows[0];
                result = long.Parse(dr["ORDERID"].ToString())+1;
            }
            return result;
        }

        /*调用该函数将获取所有订单的所有信息，无订单返回null*/
        public static List<RoomOrder> GetAllList()
        {
            List<RoomOrder> list = new List<RoomOrder>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMORDER");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new RoomOrder()
                {
                    OrderID = long.Parse(dr["ORDERID"].ToString()),
                    RoomID = dr["ROOMID"].ToString(),
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    StartTime = dr["STARTTIME"].ToString(),
                    EndTime = dr["ENDTIME"].ToString(),
                    Days = long.Parse(dr["DAYS"].ToString()),
                    OrderStatus = dr["ORDERSTATUS"].ToString(),
                    Violation = dr["VIOLATION"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString())
                });
            }
            /*为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*调用此函数查询某个顾客的所有订单，输入为顾客CustomerId，返回该顾客全部订单信息*/
        /*无订单返回null*/
        public static List<RoomOrder> ListByGuest(string ID)
        {
            List<RoomOrder> list = new List<RoomOrder>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMORDER WHERE CustomerID = :CustomerID",
               new OracleParameter(":CustomerID", ID)
               );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new RoomOrder()
                {
                    OrderID = long.Parse(dr["ORDERID"].ToString()),
                    RoomID = dr["ROOMID"].ToString(),
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    StartTime = dr["STARTTIME"].ToString(),
                    EndTime = dr["ENDTIME"].ToString(),
                    Days = long.Parse(dr["DAYS"].ToString()),
                    OrderStatus = dr["ORDERSTATUS"].ToString(),
                    Violation = dr["VIOLATION"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString())
                });
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*调用此函数查询某个房间的所有订单，输入为RoomID，返回该房间全部订单信息*/
        /*无订单返回null*/
        public static List<RoomOrder> ListByRoom(string ID)
        {
            List<RoomOrder> list = new List<RoomOrder>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMORDER WHERE RoomID = :RoomID",
               new OracleParameter(":RoomID", ID)
               );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new RoomOrder()
                {
                    OrderID = long.Parse(dr["ORDERID"].ToString()),
                    RoomID = dr["ROOMID"].ToString(),
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    StartTime = dr["STARTTIME"].ToString(),
                    EndTime = dr["ENDTIME"].ToString(),
                    Days = long.Parse(dr["DAYS"].ToString()),
                    OrderStatus = dr["ORDERSTATUS"].ToString(),
                    Violation = dr["VIOLATION"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString())
                });
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }


        /*根据状态列出订单信息，预留的可能使用的接口*/
        /*status取值 ed是已经完成 ing是正在进行*/
        public static List<RoomOrder> ListByOrederStatus(string status)
        {
            List<RoomOrder> list = new List<RoomOrder>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMORDER WHERE OrderStatus = :OrderStatus",
                new OracleParameter(":OrderStatus", status)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new RoomOrder()
                {
                    OrderID = long.Parse(dr["ORDERID"].ToString()),
                    RoomID = dr["ROOMID"].ToString(),
                    CustomerID = dr["CUSTOMERID"].ToString(),
                    StartTime = dr["STARTTIME"].ToString(),
                    EndTime = dr["ENDTIME"].ToString(),
                    Days = long.Parse(dr["DAYS"].ToString()),
                    OrderStatus = dr["ORDERSTATUS"].ToString(),
                    Violation = dr["VIOLATION"].ToString(),
                    Amount = float.Parse(dr["AMOUNT"].ToString())
                });
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*新创建的订单一定是进行中，所以参数中没有订单状态*/
        /*创建合法性问题没考虑订单ID的问题，只考虑了该房间是否仍然有订单处于进行状态*/
        public static int CreateOrder(long OID, string RID, string CID, string starttime, string endtime, long Days, float price)
        {
            /*考察订单合法性，确定房间是否被重复定下，假设订单ID这种东西*/
            List<RoomOrder> list = ListByRoom(RID);
            string ing = "ing";
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMORDER WHERE RoomID = :RoomID AND OrderStatus= :OrderStatus",
              new OracleParameter(":RoomID", RID),
              new OracleParameter(":OrderStatus", ing)
              );

            if (dt.Rows.Count == 0)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO RoomOrder(OrderID,RoomID,CustomerID,StartTime,EndTime,Days,OrderStatus,Violation,Amount) VALUES(:OrderID,:RoomID,:CustomerID,:StartTime,:EndTime,:Days,:OrderStatus,:Violation,:Amount)",//后面几个应该也有冒号吧，添加了days
                    new OracleParameter(":OrderID", OID),
                    new OracleParameter(":RoomID", RID),
                    new OracleParameter(":CustomerID", CID),
                    new OracleParameter(":StartTime", starttime),
                    new OracleParameter(":EndTime", endtime),
                     new OracleParameter(":Days", Days),//添加了days
                    new OracleParameter(":OrderStatus", ing),
                     new OracleParameter(":Violation", null),
                    new OracleParameter(":Amount", price)
                    );
            }
            else
            {
                //throw new Exception("房间已被订出，订单无法添加");
                return -1;
            }
        }

        /*调用该函数修改订单的状态，将未完成的订单修改为已经完成*/
        public static int Change_Order_Status(long order_id)
        {
            string status = "done";
            RoomOrder room_order = Find(order_id);
            if (room_order != null)
            {
                DBHelper.ExecuteNonQuery("DELETE FROM ROOMORDER WHERE ORDERID = :ORDERID",
                   new OracleParameter(":ORDERID", order_id)
                   );
                return DBHelper.ExecuteNonQuery("INSERT INTO RoomOrder(OrderID,RoomID,CustomerID,StartTime,EndTime,Days,OrderStatus,Violation,Amount) VALUES(:OrderID,:RoomID,:CustomerID,:StartTime,:EndTime,:Days,:OrderStatus,:Violation,:Amount)",//后面几个应该也有冒号吧，添加了days
                   new OracleParameter(":OrderID", room_order.OrderID),
                   new OracleParameter(":RoomID", room_order.RoomID),
                   new OracleParameter(":CustomerID", room_order.CustomerID),
                   new OracleParameter(":StartTime", room_order.StartTime),
                   new OracleParameter(":EndTime", room_order.EndTime),
                    new OracleParameter(":Days", room_order.Days),//添加了days
                   new OracleParameter(":OrderStatus", status),
                    new OracleParameter(":Violation", null),
                   new OracleParameter(":Amount", room_order.Amount)
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
