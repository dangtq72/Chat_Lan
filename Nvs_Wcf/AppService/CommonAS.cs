
using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;
using ZetaCompressionLibrary;

namespace Nvs_Wcf
{

    public partial interface NvsContractService
    {
        [OperationContract()]
        string AllCode_CheckWCF();

        [OperationContract()]
        User_Info User_Login(string p_UserName, string p_PassWord);

        [OperationContract()]
        void Send_Msg(Message_Info p_Message_Info);

        [OperationContract()]
        List<User_Info> Get_All_User();

        [OperationContract()]
        List<Message_Info> Get_msgSend(string p_UserName);

        [OperationContract()]
        List<Message_Info> Get_msgReceive(string p_UserName);
    }


    public partial class NvsService : NvsContractService
    {
        public string AllCode_CheckWCF()
        {
            return "OK";
        }

        public User_Info User_Login(string p_UserName, string p_PassWord)
        {
            try
            {
                User_Info _User_Info = DBMemory.Get_User_Info(p_UserName, p_PassWord);
                if (_User_Info == null)
                    return new User_Info();
                else
                {
                    Subscribe(_User_Info.User_Name);
                    Auto_Push_Data.Broadcast_Session(new Session_Info(_User_Info.User_Name, (int)Enum_Session_Status.LogIn));

                    return _User_Info;
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new User_Info();
            }
        }

        public void Send_Msg(Message_Info p_Message_Info)
        {
            try
            {
                if (DBMemory.c_dic_User_Interface.ContainsKey(p_Message_Info.From_User_Name))
                    DBMemory.c_dic_User_Interface[p_Message_Info.From_User_Name].Add_Msg_Send(p_Message_Info);

                if (DBMemory.c_dic_User_Interface.ContainsKey(p_Message_Info.To_User_Name))
                    DBMemory.c_dic_User_Interface[p_Message_Info.To_User_Name].Add_Msg_Recieve(p_Message_Info);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public List<User_Info> Get_All_User()
        {
            try
            {
                List<User_Info> _lst = new List<User_Info>();
                foreach (User_Interface item in DBMemory.c_dic_User_Interface.Values)
                {
                    User_Info _User_Info = item.Get_User_Info();
                    _lst.Add(_User_Info);
                }

                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        public List<Message_Info> Get_msgSend(string p_UserName)
        {
            try
            {
                List<Message_Info> _lst = new List<Message_Info>();
                if (DBMemory.c_dic_User_Interface.ContainsKey(p_UserName))
                {
                    return DBMemory.c_dic_User_Interface[p_UserName].GetAll_Send();
                }
                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Message_Info>();
            }
        }

        public List<Message_Info> Get_msgReceive(string p_UserName)
        {
            try
            {
                List<Message_Info> _lst = new List<Message_Info>();
                if (DBMemory.c_dic_User_Interface.ContainsKey(p_UserName))
                {
                    return DBMemory.c_dic_User_Interface[p_UserName].GetAll_Receive();
                }
                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Message_Info>();
            }
        }
    }
}
