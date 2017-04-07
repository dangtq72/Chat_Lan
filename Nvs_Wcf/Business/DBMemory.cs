using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nvs_Wcf
{
    public class DBMemory
    {
        public static object c_object_lock = new object();

        public static Dictionary<string, NvsService_Callback> c_dic_identifi_callback_client = new Dictionary<string, NvsService_Callback>();

        //public static Dictionary<string, User_Info> c_dic_User = new Dictionary<string, User_Info>();
        public static Dictionary<string, User_Interface> c_dic_User_Interface = new Dictionary<string, User_Interface>();

        static MyQueue c_queue_send = new MyQueue();


        public static void EnqueueSend(Message_Info p_msg)
        {
            c_queue_send.Enqueue(p_msg);
        }

        public static Message_Info Get_msg_Send()
        {
            Message_Info _Message_Info = (Message_Info)c_queue_send.Dequeue();
            if (_Message_Info != null)
                return _Message_Info;
            else return _Message_Info;
        }

        public static User_Info Get_User_Info(string p_UserName, string p_PassWord)
        {
            try
            {
                if (c_dic_User_Interface.ContainsKey(p_UserName))
                {
                    if (c_dic_User_Interface[p_UserName].c_User_Info.Password == p_PassWord)
                    {
                        return c_dic_User_Interface[p_UserName].c_User_Info;
                    }
                    else return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }
    }
}
