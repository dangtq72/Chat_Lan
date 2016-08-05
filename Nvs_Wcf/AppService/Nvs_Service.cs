using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;
using NaviCommon;

namespace Nvs_Wcf
{
    [ServiceContract(CallbackContract = typeof(NvsService_Callback))]
    public partial interface NvsContractService
    {
        [OperationContract]
        void Subscribe(string p_id);

        [OperationContract]
        void UnSubscribe(string p_id);

    }

    public interface NvsService_Callback
    {
        [OperationContract]
        void PushMessage(Message_Info _data);

        [OperationContract]
        void PushSession(Session_Info _data);

        [OperationContract]
        void PushData(string _data);

        // có thể sử dụng EventDataType để truyền tải dữ liệu
    }


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                  ConcurrencyMode = ConcurrencyMode.Multiple,
                   UseSynchronizationContext = false)]
    public partial class NvsService : NvsContractService
    {
        public NvsService()
        {
        }

        public void Subscribe(string p_id)
        {
            try
            {
                NvsService_Callback _NvsService_Callback = OperationContext.Current.GetCallbackChannel<NvsService_Callback>();

                lock (DBMemory.c_object_lock)
                {
                    //remove the old client
                    if (DBMemory.c_dic_identifi_callback_client.ContainsKey(p_id))
                        DBMemory.c_dic_identifi_callback_client.Remove(p_id);

                    DBMemory.c_dic_identifi_callback_client.Add(p_id, _NvsService_Callback);

                    NaviCommon.Common.log.Error("Client " + p_id + " Subscribe");
                }

                if (DBMemory.c_dic_User_Interface.ContainsKey(p_id))
                    DBMemory.c_dic_User_Interface[p_id].c_User_Info.Online_Status = (int)Enum_Session_Status.LogIn;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public void UnSubscribe(string p_id)
        {
            try
            {
                lock (DBMemory.c_object_lock)
                {
                    DBMemory.c_dic_identifi_callback_client.Remove(p_id);

                    NaviCommon.Common.log.Error("Client " + p_id + " UnSubscribe");
                }

                Auto_Push_Data.Broadcast_Session(new Session_Info(p_id, (int)Enum_Session_Status.LogOut));

                if (DBMemory.c_dic_User_Interface.ContainsKey(p_id))
                    DBMemory.c_dic_User_Interface[p_id].c_User_Info.Online_Status = (int)Enum_Session_Status.LogOut;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}
