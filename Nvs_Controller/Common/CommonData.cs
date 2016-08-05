using NaviCommon;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Nvs_Controller
{
    public class CommonData
    {
        static object objLockService_ = new object();

        static NvsService.NvsContractServiceClient _serviceWCF;//service dùng chung cho cả hệ thống
        static InstanceContext _context;
        static NvsServiceCallback _callback;

        public static NvsService.NvsContractServiceClient c_serviceWCF
        {
            get
            {
                try
                {
                    lock (objLockService_)
                    {
                        if (_serviceWCF == null)
                        {
                            Create_Instance("null");
                            SetDefaultServiceConfig();
                        }
                        else if (_serviceWCF.State == System.ServiceModel.CommunicationState.Faulted)
                        {
                            Create_Instance("Faulted");
                            SetDefaultServiceConfig();
                        }
                        else if (_serviceWCF.State == System.ServiceModel.CommunicationState.Closed)
                        {
                            Create_Instance("Closed");
                            SetDefaultServiceConfig();
                        }

                        //mỗi lần gọi sẽ test kết nối, nếu kết nối ok thì sẽ set lại sendtimeout thành 10 phút
                        _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(30);
                        string s = _serviceWCF.AllCode_CheckWCF();
                        if (s == "OK")
                        {
                            _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromMinutes(10);
                        }
                        return _serviceWCF;
                    }

                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    Create_Instance("Error");
                    return _serviceWCF;
                }
            }
        }

        private static void Create_Instance(string p_status)
        {
            try
            {
                if (NaviCommon.Common.c_User_Info != null)
                {
                    NaviCommon.Common.ID = NaviCommon.Common.c_User_Info.User_Name;
                }
                else
                    NaviCommon.Common.ID = CreateRandomString();

                NaviCommon.Common.log.Error("Create Instance to Service " + NaviCommon.Common.ID + " status " + p_status);

                _callback = new NvsServiceCallback();
                _context = new InstanceContext(_callback);
                _serviceWCF = new NvsService.NvsContractServiceClient(_context);
                _serviceWCF.Subscribe(NaviCommon.Common.ID);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private static void RestartWCFService()
        {
            try
            {
                if (_serviceWCF == null)
                {
                    _serviceWCF = new NvsService.NvsContractServiceClient(_context);
                    SetDefaultServiceConfig();
                }
                else
                {
                    _serviceWCF.Abort();
                    _serviceWCF = new NvsService.NvsContractServiceClient(_context);
                    SetDefaultServiceConfig();
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private static void SetDefaultServiceConfig()
        {
            try
            {
                _serviceWCF.InnerChannel.OperationTimeout = TimeSpan.FromSeconds(20);
                _serviceWCF.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(20);
                _serviceWCF.Endpoint.Binding.OpenTimeout = TimeSpan.FromMinutes(1);
                _serviceWCF.Endpoint.Binding.CloseTimeout = TimeSpan.FromSeconds(10);
            }
            catch
            {
            }
        }

        private static string CreateRandomString()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                char c; Random rand = new Random();
                for (int i = 0; i < 6; i++)
                {
                    c = Convert.ToChar(Convert.ToInt32(rand.Next(65, 87)));
                    sb.Append(c);
                }
                return sb.ToString();
            }
            catch
            {
                return "";
            }
        }

    }

    public class NvsServiceCallback : NvsService.NvsContractServiceCallback
    {
        public void PushMessage(Message_Info _data)
        {
            Common_Event.c_NVSEvent.WhenReceiveCallBackData(Key_Raise_Event.Message, _data);
        }

        public void PushData(string _data)
        {

        }

        public void PushSession(Session_Info _data)
        {
            if (Common.c_User_Info != null && _data.User_Name != Common.c_User_Info.User_Name)
                Common_Event.c_NVSEvent.WhenReceiveCallBackData(Key_Raise_Event.Session, _data);
        }
    }

}
