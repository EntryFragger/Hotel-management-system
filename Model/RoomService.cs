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
    public class RoomService
    {
        public string RoomID { set; get; }//PK
        public string Time { set; get; }//PK
        public string Type { set; get; }
        public string Remark { set; get; }
        public string Amount { set; get; }
        public string Status { set; get; }
        public string EmployeeID { set; get; }

        public static RoomService Find(string roomid, string time)
        {
            RoomService room_service = null;
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM ROOMSERVICE WHERE RoomID = :RoomID AND Time = :Time",
                new OracleParameter(":RoomID", roomid),
                new OracleParameter(":Time", time)
                );
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                room_service = dr.DtToModel<RoomService>();
            }
            return room_service;
        }

        public static int AddRoomService(string roomid, string time, string type, string remark, string amount, string status, string employee_id)
        {
            RoomService room_service = Find(roomid, time);
            if (room_service == null)
            {
                return DBHelper.ExecuteNonQuery("INSERT INTO ROOMSERVICE(RoomID,Time,Type,Remark,Amount,Status,EmployeeID) VALUES(:RoomID,:Time,:Type,:Remark,:Amount,:Status,:EmployeeID)",
                    new OracleParameter(":RoomID", roomid),
                    new OracleParameter(":Time", time),
                    new OracleParameter(":Type", type),
                    new OracleParameter(":Remark", remark),
                    new OracleParameter(":Amount", amount),
                    new OracleParameter(":Status", status),
                    new OracleParameter(":EmployeeID", employee_id)
                    );
            }
            else
            {
                throw new Exception("房间服务已存在，无法添加");
                return 0;
            }
        }

        public static int Change_RoomService_Status(string roomid, string time, string status)
        {
            RoomService room_service = Find(roomid, time);
            if (room_service == null)
            {
                return DBHelper.ExecuteNonQuery("UPDATE ROOMSERVICE SET Status = :Status WHERE RoomID = :RoomID AND Time = :Time",
                    new OracleParameter(":RoomID", roomid),
                    new OracleParameter(":Time", time),
                    new OracleParameter(":Status", status)
                    );
            }
            else
            {
                throw new Exception("房间服务不存在，无法修改");
                return 0;
            }
        }

        public static List<RoomService> ListAll()
        {
            List<RoomService> list = new List<RoomService>();
            DataTable dt = DBHelper.ExecuteTable("SELECT * FROM ROOMSERVICE");
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<RoomService>());
            }
            return list;
        }

        public static List<RoomJobInfo> GetUndoneJobInfo(string employee_id)
        {
            List<RoomJobInfo> list = new List<RoomJobInfo>();
            DataTable dt = DBHelper.ExecuteTable("SELECT RoomID , Time, Type FROM ROOMSERVICE WHERE Status = :Status AND EmployeeID = :EmployeeID",
                new OracleParameter(":Status", "UnDone"),
                new OracleParameter(":EmployeeID", employee_id)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<RoomJobInfo>());
            }
            return list;
        }

        /*public static List<RoomService> ListByRoom(string ID)
        {
            List<RoomService> list = new List<RoomService>();
            DataTable dt = DBHelper.ExecuteTable("SELECT *  FROM CLEANSERVICE WHERE roomId = :roomId",
                new OracleParameter(":roomId", ID)
                );
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr.DtToModel<RoomService>());
            }
            return list;
        }*/
    }
    public class RoomJobInfo
    {
        public string RoomID { set; get; }//PK
        public string Time { set; get; }//PK
        public string Type { set; get; }

    }
}
