using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;

namespace Nvs_Wcf
{
    public class Auto_Push_Data
    {
        public static void Broadcast_Session(Session_Info p_Session_Info)
        {
            try
            {
                List<string> _lst_inactiveClients = new List<string>();

                foreach (KeyValuePair<string, NvsService_Callback> item in DBMemory.c_dic_identifi_callback_client)
                {
                    // kiểm tra xem kết nối có ok hay không
                    if ((((IChannel)item.Value).State != CommunicationState.Opened))
                    {
                        _lst_inactiveClients.Add(item.Key);
                    }

                    string _data = item.Key + Common_Event.MESSAGETYPE_SPLIT + DateTime.Now.ToString("hh:MM:ss:fff") + Common_Event.MESSAGETYPE_SPLIT + DateTime.Now.ToString("hh:MM:ss:fff");
                    try
                    {
                        item.Value.PushSession(p_Session_Info);
                    }
                    catch (Exception exx)
                    {
                        //NaviCommon.Common.log.Error("Error when " + DateTime.Now.ToString("hh:MM:ss:fff"));
                        NaviCommon.Common.log.Error(exx.ToString());

                        // add những thằng nào bị lỗi vào list inactive để tý nữa xóa nó đi
                        _lst_inactiveClients.Add(item.Key);

                        // tắt ở task manager
                        // The communication object, System.ServiceModel.Channels.ServiceChannel, cannot be used for communication because it has been Aborted.

                        // rút dây mạng
                        // The server did not provide a meaningful reply; this might be caused by a contract mismatch, a premature session shutdown or an internal server NaviCommon.Common.

                        //// hủy kết nối của nó đi
                        //if (exx.ToString().Contains("Aborted"))
                        //{
                        //    lock (DBMemory.c_object_lock)
                        //    {
                        //        DBMemory.c_dic_identifi_callback_client.Remove(item.Key);
                        //        NaviCommon.Common.log.Error("Client " + item.Key + " UnSubscribe because ServiceChannel has been Aborted");
                        //    }
                        //}
                    }
                }

                // xóa những thằng nào bị lỗi đi
                if (_lst_inactiveClients.Count > 0)
                {
                    foreach (string client in _lst_inactiveClients)
                    {
                        lock (DBMemory.c_object_lock)
                        {
                            DBMemory.c_dic_identifi_callback_client.Remove(client);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}


