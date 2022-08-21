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
    public class Room
    {
        public string RoomID { set; get; }//PK
        public string RoomType { set; get; }
        public string RoomStatus { set; get; }
        public string RoomPrice { set; get; }
        
        public static Room Find(string ID)
        {
            Room room = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM ROOM WHERE RoomID = :RoomID",
                new OracleParameter(":RoomID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                room = dr.DtToModel<Room>();
            }
            return room;
        }

        public static List<RoomInfo> RoomInfo_ListByType(string type)
        {
            List<RoomInfo> list = new List<RoomInfo>();
            DataTable dt = DBHelper.ExecuteTable("SELECT RoomID , RoomType , RoomStatus FROM ROOM WHERE RoomType = :RoomType",
                new OracleParameter(":RoomType", type)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<RoomInfo>());
            }
            return list;
        }

        public static List<RoomInfo> RoomInfo_ListAllType()
        {
            List<RoomInfo> list = new List<RoomInfo>();
            DataTable dt = DBHelper.ExecuteTable("SELECT RoomID , RoomType , RoomStatus FROM ROOM");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<RoomInfo>());
            }
            return list;
        }

        public static List<Room> ListByStatus(string status)
        {
            List<Room> list = new List<Room>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOM WHERE RoomStatus = :RoomStatus",
                new OracleParameter(":RoomStatus", status)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Room>());
            }
            return list;
        }

        public static int FindRoomPrice(string ID)//查找某个房间的价格
        {
            int Price = 0;
            DataTable dt = DBHelper.ExecuteTable("SELECT RoomPrice FROM ROOM WHERE RoomID = :RoomID",
                new OracleParameter(":RoomID", ID)
                );
            if (dt.Rows.Count > 0)
            {
                String dm = dt.Rows[0].ToString();
                Price = int.Parse(dm);
            }
            return Price;
        }

        public static int Change_Room_Status(string ID, string status)
        {
            Room room = Find(ID);
            if (room != null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE ROOM SET RoomStatus = :RoomStatus WHERE RoomID = :RoomID",
                   new OracleParameter(":RoomID", ID),
                   new OracleParameter(":RoomStatus", status)
                   );
            }
            else
            {
                return -1;
            }
        }

        public static List<Room> ListAll()
        {
            List<Room> list = new List<Room>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOM");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<Room>());
            }
            return list;
        }

        /*public static int DeleteRoom(string ID)
        {
            return DBHelper.ExecuteNonQuery("DELETE FROM ROOM WHERE roomId = :roomId",
                new OracleParameter(":roomId", ID)
                );
        }*/

        /*public static int AddRoom(string ID, string type, string price, string status)
        {
            Room room = Find(ID);
            if (room == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO ROOM(roomId,roomType,price,roomStatus) VALUES(:roomId,:roomType,:price,:roomStatus)",
                    new OracleParameter(":roomId", ID),
                    new OracleParameter(":roomType", type),
                    new OracleParameter(":price", price),
                    new OracleParameter(":roomStatus", status)
                    );
            }
            else
            {
                throw new Exception("房间已存在，无法添加");
                return 0;
            }
        }*/
    }
    public class RoomInfo
    {
        public string RoomID { set; get; }//PK
        public string RoomType { set; get; }
        public string RoomStatus { set; get; }
    }
    public class Type_Price
    {
        public string RoomType { set; get; }
        public string RoomPrice { set; get; }
    }
}
