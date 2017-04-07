using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nvs_Wcf
{
    public class User_Interface
    {
        public User_Interface(User_Info p_User_Info)
        {
            c_User_Info = p_User_Info;

            Thread _thread_get_data = new Thread(Push_Msg);
            _thread_get_data.IsBackground = true;
            _thread_get_data.Start();
        }

        public User_Info c_User_Info;
        MyQueue c_queue_receive = new MyQueue();

        List<Message_Info> _lst_Send = new List<Message_Info>();
        List<Message_Info> _lst_Recieve = new List<Message_Info>();
        List<User_Friends_Info> _lst_Friends = new List<User_Friends_Info>();

        /// <summary>
        /// Lưu msg nó đã nhận
        /// </summary>
        public void Add_Msg_Recieve(Message_Info p_msg)
        {
            c_queue_receive.Enqueue(p_msg);
        }

        /// <summary>
        /// Lưu msg nó đã gửi đi
        /// </summary>
        public void Add_Msg_Send(Message_Info p_msg)
        {
            try
            {
                Message_Info _Message_Info = new Message_Info(p_msg);
                _Message_Info.Status = (int)Enum_Message_Status.OK;
                p_msg.Type = (int)Enum_Message_Type.Send;
                _lst_Send.Add(_Message_Info);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public List<Message_Info> GetAll_Send()
        {
            return _lst_Send;
        }

        public List<Message_Info> GetAll_Receive()
        {
            return _lst_Recieve;
        }

        public List<User_Friends_Info> Get_Friends()
        {
            return _lst_Friends;
        }


        public User_Info Get_User_Info()
        {
            return c_User_Info;
        }

        Message_Info Get_msg_Send()
        {
            Message_Info _Message_Info = (Message_Info)c_queue_receive.Dequeue();
            if (_Message_Info != null)
                return _Message_Info;
            else return _Message_Info;
        }

        void Push_Msg()
        {
            while (true)
            {
                try
                {
                    // thằng này nếu ko có lệnh thì nó ngủ luôn, nên không cần sleep
                    Message_Info item = (Message_Info)Get_msg_Send();
                    if (item != null)
                    {
                        if (item.IsGroup == 1)
                        {
                            foreach (Member_Info _Member_Info in c_User_Info.List_Member)
                            {
                                if (_Member_Info.Member_Name == c_User_Info.User_Name) continue;
                                //Push_By_Identify(_Member_Info.Member_Name, item);
                                if (DBMemory.c_dic_User_Interface.ContainsKey(_Member_Info.Member_Name))
                                    DBMemory.c_dic_User_Interface[_Member_Info.Member_Name].Push_By_Identify(_Member_Info.Member_Name, item);
                            }
                        }
                        else
                        {
                            //string _msg = NaviCommon.JsonFactory.Create_Json_Send<Message_Info>("SEND", item);
                            Push_By_Identify(item.To_User_Name, item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    Thread.Sleep(1000);
                }
            }
        }

        public void Push_By_Identify(string p_id, Message_Info p_data)
        {
            try
            {
                if (DBMemory.c_dic_identifi_callback_client.ContainsKey(p_id))
                {
                    NvsService_Callback _item = DBMemory.c_dic_identifi_callback_client[p_id];
                    _item.PushMessage(p_data);
                    p_data.Status = (int)Enum_Message_Status.OK;
                }
                else
                {
                    p_data.Status = (int)Enum_Message_Status.Not_OK;
                }

                p_data.Type = (int)Enum_Message_Type.Receive;
                _lst_Recieve.Add(p_data);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}
