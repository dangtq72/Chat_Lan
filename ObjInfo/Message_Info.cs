using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ObjInfo
{
    [DataContract]
    public class Message_Info
    {
        public Message_Info() { }
        public Message_Info(Message_Info p_Message_Info)
        {
            From_User_Name = p_Message_Info.From_User_Name;
            Message = p_Message_Info.Message;
            To_User_Name = p_Message_Info.To_User_Name;
            Time = p_Message_Info.Time;
            Type = p_Message_Info.Type;
            IsGroup = p_Message_Info.IsGroup;
        }

        public Message_Info(string p_From_User_Name,string p_Message, string p_To_User_Name,int p_type, int p_isgroup)
        {
            From_User_Name = p_From_User_Name;
            Message = p_Message;
            To_User_Name = p_To_User_Name;
            //Time = DateTime.Now.ToString("HH:mm:ss:fff");
            Time = DateTime.Now.ToString("HH:mm:ss");
            Type = p_type;
            IsGroup = p_isgroup;
        }

        [DataMember]
        public string From_User_Name { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string To_User_Name { get; set; }

        [DataMember]
        public string Time { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public int IsGroup { get; set; }
    }

    [DataContract]
    public class Session_Info
    {
        public Session_Info() { }

        public Session_Info(string p_UserName,int p_Online_Status)
        {
            User_Name = p_UserName;
            Online_Status = p_Online_Status;
        }

        [DataMember]
        public string User_Name { get; set; }

        [DataMember]
        public int Online_Status { get; set; }

    }
}
