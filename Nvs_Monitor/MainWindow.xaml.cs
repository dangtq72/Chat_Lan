using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using Nvs_Controller;
using ObjInfo;
using System.Collections;
using Microsoft.Windows.Controls.Ribbon;
using System.Runtime.InteropServices;
using NaviCommon;
using System.Threading;
using System.Xml;
using System.Linq;

namespace Nvs_Monitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            log4net.Config.XmlConfigurator.Configure();
            InitializeComponent();

            UserLogin();

            Thread _thr_checkWCF = new Thread(Method_CheckWCF);
            _thr_checkWCF.IsBackground = true;
            _thr_checkWCF.Start();
        }

        Controller _AllCodeController = new Controller();
        Controller _Controller = new Controller();
        User_Info c_User_To;
        Dictionary<string, User_Info> c_dic_User = new Dictionary<string, User_Info>();

        // lưu thông tin msg đã gửi đi
        Dictionary<string, List<Message_Info>> c_dic_Msg = new Dictionary<string, List<Message_Info>>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Show_Friend();
            Get_Msg_History();
            Common_Event.c_NVSEvent.WhenReceiveCallBackDataEvent += c_NVSEvent_WhenReceiveCallBackDataEvent;
            this.Title = "HELLO " + Common.c_User_Info.User_Name;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                MessageBoxResult result = NoteBox.Show("Bạn có chắc muốn thoát chương trình?", "", NoteBoxLevel.Question);
                if (result == MessageBoxResult.Yes)
                {
                    Controller _AllCodeController = new Controller();
                    _AllCodeController.UnSubscribe();
                    App.Current.Shutdown();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch
            {
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Send_Msg();
        }

        private void lsvFriend_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                User_Info item = (User_Info)lsvFriend.SelectedItem;
                if (item == null)
                    return;

                c_User_To = item;

                if (c_User_To.Online_Status == (int)Enum_User_Status.Connected)
                    lblFriend.Content = c_User_To.User_Name + " - Online " + c_User_To.Status;
                else
                    lblFriend.Content = c_User_To.User_Name + " - Offline " + c_User_To.Status;

                string _key = Common.c_User_Info.User_Name + "|" + item.User_Name;
                string _key2 = item.User_Name + "|" + Common.c_User_Info.User_Name;

                List<Message_Info> _lst = new List<Message_Info>();
                if (c_dic_Msg.ContainsKey(_key))
                {
                    _lst = c_dic_Msg[_key];
                }
                else if (c_dic_Msg.ContainsKey(_key2))
                {
                    _lst = c_dic_Msg[_key2];
                }

                _lst = _lst.OrderBy(p => p.Time).ToList();

                lsvMessage.Items.Clear();
                foreach (Message_Info _Message_Info in _lst)
                {
                    UpdateListView(_Message_Info);
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && txtMsg.Focus())
            {
                Send_Msg();
            }
        }

        void LogOut()
        {
            try
            {
                MessageBoxResult result = NoteBox.Show(" Bạn có muốn thoát khỏi chương trình không?", "Thông báo", NoteBoxLevel.Question);
                if (result == MessageBoxResult.Yes)
                {

                    //goi chuc nang dang nhap
                    UserLogin();

                    //UpdateLayout();
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void UserLogin()
        {
            try
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                Login _login = new Login();
                _login.c_isLogin = true;
                if (_login.ShowDialog() != true)
                {
                    this.Close();
                }

                this.Visibility = System.Windows.Visibility.Visible;
                this.Focus();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void Show_Friend()
        {
            try
            {
                c_dic_User = new Dictionary<string, User_Info>();

                List<User_Info> _lst = _Controller.Get_All_User();
                foreach (User_Info item in _lst)
                {
                    if (item.User_Name != Common.c_User_Info.User_Name)
                    {
                        if (item.Online_Status == (int)Enum_Session_Status.LogIn)
                            item.ShowOnline = Visibility.Visible;
                        else
                            item.ShowOnline = Visibility.Collapsed;

                        c_dic_User[item.User_Name] = item;
                    }
                }

                lsvFriend.ItemsSource = c_dic_User.Values;

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void Get_Msg_History()
        {
            try
            {
                List<Message_Info> _lst_Send = _Controller.Get_msgSend(Common.c_User_Info.User_Name);
                foreach (Message_Info item in _lst_Send)
                {
                    Add_msg_ToDic(item);
                }

                List<Message_Info> _lst_Recieve = _Controller.Get_msgReceive(Common.c_User_Info.User_Name);
                foreach (Message_Info item in _lst_Recieve)
                {
                    Add_msg_ToDic(item);
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void Method_CheckWCF()
        {
            while (true)
            {
                try
                {
                    bool _Result = _AllCodeController.AllCode_checkWCF();
                    if (_Result == false)
                    {
                        Thread.Sleep(5000);

                        _Result = _AllCodeController.AllCode_checkWCF();

                        if (_Result == false)
                        {
                            NaviCommon.Common.log.Error("Disconnect to Service");
                        }
                    }
                }
                catch (Exception ex)
                {
                    NaviCommon.Common.log.Error(ex.ToString());
                    Thread.Sleep(1000);
                }

                Thread.Sleep(1000);
            }
        }

        void c_NVSEvent_WhenReceiveCallBackDataEvent(object sender, NVSEventArgs e)
        {
            try
            {
                string _sender = (string)sender;
                if (_sender == Key_Raise_Event.Message)
                {
                    Message_Info _Message_Info = (Message_Info)e.Obj;
                    _Message_Info.Type = (int)Enum_Message_Type.Receive;
                    UpdateListView(_Message_Info);

                    Add_msg_ToDic(_Message_Info);
                    Common_Alert(_Message_Info);
                }
                else if (_sender == Key_Raise_Event.Session)
                {
                    Session_Info _Session_Info = (Session_Info)e.Obj;
                    Change_User_OnlineStatus(_Session_Info);
                    return;
                }
                else return;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }



        void Change_User_OnlineStatus(Session_Info _Session_Info)
        {
            try
            {
                if (c_dic_User.ContainsKey(_Session_Info.User_Name))
                {
                    c_dic_User[_Session_Info.User_Name].Online_Status = _Session_Info.Online_Status;

                    if (_Session_Info.Online_Status == (int)Enum_Session_Status.LogIn)
                        c_dic_User[_Session_Info.User_Name].ShowOnline = Visibility.Visible;
                    else
                        c_dic_User[_Session_Info.User_Name].ShowOnline = Visibility.Collapsed;
                }

            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void Send_Msg()
        {
            try
            {
                if (txtMsg.Text == "") return;
                if (c_User_To == null) return;
                Message_Info _Message_Info = new Message_Info(Common.c_User_Info.User_Name, txtMsg.Text, c_User_To.User_Name, (int)Enum_Message_Type.Send);
                _Controller.Send_Msg(_Message_Info);

                UpdateListView(_Message_Info);
                txtMsg.Clear();
                txtMsg.Focus();

                Add_msg_ToDic(_Message_Info);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        void Add_msg_ToDic(Message_Info p_Message_Info)
        {
            try
            {
                string _key = p_Message_Info.From_User_Name + "|" + p_Message_Info.To_User_Name;
                string _key2 = p_Message_Info.To_User_Name + "|" + p_Message_Info.From_User_Name;

                if (c_dic_Msg.ContainsKey(_key) == false && c_dic_Msg.ContainsKey(_key2) == false)
                {
                    List<Message_Info> _lst = new List<Message_Info>();
                    _lst.Add(p_Message_Info);
                    c_dic_Msg[_key] = _lst;
                }
                else
                {
                    List<Message_Info> _lst;
                    if (c_dic_Msg.ContainsKey(_key))
                        _lst = c_dic_Msg[_key];
                    else
                        _lst = c_dic_Msg[_key2];

                    _lst.Add(p_Message_Info);
                    if (c_dic_Msg.ContainsKey(_key))
                        c_dic_Msg[_key] = _lst;
                    else
                        c_dic_Msg[_key2] = _lst;
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        delegate void ShowListViewDelegate(Message_Info p_Message_Info);
        private void UpdateListView(Message_Info p_Message_Info)
        {
            try
            {
                if (lsvMessage.Dispatcher.CheckAccess() == false)
                    lsvMessage.Dispatcher.Invoke(new ShowListViewDelegate(UpdateListView), p_Message_Info);
                else
                {
                    if (c_User_To == null) return;
                    if (p_Message_Info.From_User_Name != c_User_To.User_Name) return;
                    //lsvMessage.Items.Insert(0, p_Message_Info);
                    lsvMessage.Items.Add(p_Message_Info);
                    lsvMessage.ScrollIntoView(p_Message_Info);
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        delegate void delegateMes_Alert(Message_Info p_Message_Info);
        private void Common_Alert(Message_Info p_Message_Info)
        {
            if (lsvMessage.Dispatcher.CheckAccess() == false)
            {
                lsvMessage.Dispatcher.Invoke(new delegateMes_Alert(Common_Alert), p_Message_Info);
            }
            else
            {
                ShowCommonAlert(p_Message_Info);
            }
        }

        void ShowCommonAlert(Message_Info p_Message_Info)
        {
            try
            {
                Alert_Common _Alert_Common = new Alert_Common();
                _Alert_Common.Title = p_Message_Info.From_User_Name;

                _Alert_Common.FontWeight = FontWeights.Bold;
                _Alert_Common.FontSize = 13;
                _Alert_Common.Msg = p_Message_Info.Message;

                _Alert_Common.IsSound = true;
                _Alert_Common.SoundFile = Common.c_FileName_Sound_Common;
                _Alert_Common.Time = "00:00:03";
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}
