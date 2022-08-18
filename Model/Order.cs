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
    public class Order
    {
        public string OrderID { set; get; }//PK
        public string RoomID { set; get; }
        public string CustomerID { set; get; }
        public string StartTime { set; get; }
        public string EndTime { set; get; }

        /*status取值 ed是已经完成 ing是正在进行*/
        public string OrederStatus { set; get; }
        public string Violation { set; get; }
        public int Amount { set; get; }

        /*根据订单的OrderId返回对应订单的所有信息*/
        public static Order Find(string ID)
        {
            Order order = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM Order WHERE OrderID = :OrderID",
                new OracleParameter(":OrderID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                order = dr.DtToModel<Order>();
            }
            return order;
        }

        /*调用该函数将获取所有订单的所有信息，无订单返回null*/
        public static List<Order> GetAllList()
        {
            List<Order> list = new List<Order>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Order");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Order>());
            }
            /*为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*调用此函数查询某个顾客的所有订单，输入为顾客CustomerId，返回该顾客全部订单信息*/
        /*无订单返回null*/
        public static List<Order> ListByGuest(string ID)
        {
            List<Order> list = new List<Order>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Order WHERE CustomerID = :CustomerID",
               new OracleParameter(":CustomerID", ID)
               );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Order>());
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*调用此函数查询某个房间的所有订单，输入为RoomID，返回该房间全部订单信息*/
        /*无订单返回null*/
        public static List<Order> ListByRoom(string ID)
        {
            List<Order> list = new List<Order>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Order WHERE RoomID = :RoomID",
               new OracleParameter(":RoomID", ID)
               );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Order>());
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*根据状态列出订单信息，预留的可能使用的接口*/
        /*status取值 ed是已经完成 ing是正在进行*/
        public static List<Order> ListByOrederStatus(string status)
        {
            List<Order> list = new List<Order>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Order WHERE OrederStatus = :OrederStatus",
                new OracleParameter(":OrederStatus", status)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Order>());
            }
            /*链表为空返回空*/
            if (!list.Any())
                return null;
            return list;
        }

        /*新创建的订单一定是进行中，所以参数中没有订单状态*/
        /*创建合法性问题没考虑订单ID的问题，只考虑了该房间是否仍然有订单处于进行状态*/
        public static int CreateOrder(string OID, string RID, string CID, string starttime, string endtime, string price)
        {
            /*考察订单合法性，确定房间是否被重复定下，假设订单ID这种东西*/
            List<Order> list = ListByRoom(RID);
            string ing = "ing";
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM Order WHERE RoomID = :RoomID AND OrederStatus= :OrederStatus",
              new OracleParameter(":RoomID", RID),
              new OracleParameter(":OrederStatus", ing)
              );

            if (dt.Rows.Count == 0)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO Order(OrderID,RoomID,CustomerID,StartTime,EndTime,OrederStatus,Violation,Amount) VALUES(:OrderID,:RoomID,:CustomerID,:StartTime,EndTime,OrederStatus,Violation,Amount)",
                    new OracleParameter(":OrderID", OID),
                    new OracleParameter(":RoomID", RID),
                    new OracleParameter(":CustomerID", CID),
                    new OracleParameter(":StartTime", starttime),
                    new OracleParameter(":EndTime", endtime),
                    new OracleParameter(":OrederStatus", ing),
                     new OracleParameter(":Violation", null),
                    new OracleParameter(":Amount", price)
                    );
            }
            else
            {
                throw new Exception("房间已被订出，订单无法添加");
                return 0;
            }
        }
    }
}
