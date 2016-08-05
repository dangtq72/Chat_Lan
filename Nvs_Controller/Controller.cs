using NaviCommon;
using ObjInfo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZetaCompressionLibrary;

namespace Nvs_Controller
{
    public class Controller
    {
        public bool AllCode_checkWCF()
        {
            try
            {
                string s = CommonData.c_serviceWCF.AllCode_CheckWCF();

                if (s == "OK")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

        public User_Info User_Login(string p_UserName, string p_PassWord)
        {
            try
            {
                User_Info _User_Info = CommonData.c_serviceWCF.User_Login(p_UserName, p_PassWord);
                return _User_Info;
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
                CommonData.c_serviceWCF.Send_Msg(p_Message_Info);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        public void UnSubscribe()
        {
            CommonData.c_serviceWCF.UnSubscribe(NaviCommon.Common.ID);
        }

        public void Subscribe()
        {
            CommonData.c_serviceWCF.Subscribe(NaviCommon.Common.ID);
        }

        public List<User_Info> Get_All_User()
        {
            try
            {
                List<User_Info> _lst = new List<User_Info>(); 
                foreach (User_Info item in CommonData.c_serviceWCF.Get_All_User())
                {
                    _lst.Add(item);
                }

                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<User_Info>();
            }
        }

        public List<Message_Info> Get_msgReceive(string p_UserName)
        {
            try
            {
                List<Message_Info> _lst = new List<Message_Info>();
                foreach (Message_Info item in CommonData.c_serviceWCF.Get_msgReceive(p_UserName))
                {
                    _lst.Add(item);
                }

                return _lst;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new List<Message_Info>();
            }
        }

        public List<Message_Info> Get_msgSend(string p_UserName)
        {
            try
            {
                List<Message_Info> _lst = new List<Message_Info>();
                foreach (Message_Info item in CommonData.c_serviceWCF.Get_msgSend(p_UserName))
                {
                    _lst.Add(item);
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
