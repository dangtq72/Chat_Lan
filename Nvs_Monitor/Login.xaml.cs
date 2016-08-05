using System;
using System.Windows;
using System.Windows.Input;
using System.Configuration;
using System.Threading;
using System.Collections;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Timers;
using Nvs_Controller;
using NaviCommon;
using ObjInfo;

namespace Nvs_Monitor
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        #region parameter

        int c_LoadCommonDataStatus = 0;//trạng thái của load dữ liệu: 0: dang load, 1: load khong thanh cong, 2: load thanh cong
        string c_msgNotify; //mesage thogn bao
        public bool c_isLogin = true;

        enum MsgType
        {
            NORMAL = 0,
            ERROR = 1
        }
        #endregion

        #region control event
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                
                probar.Visibility = System.Windows.Visibility.Hidden; //ẩn thanh progetbar;
                //
                if (c_isLogin)
                {
                    //ẩn các nút đi để thực hiện check/load dữ liệu
                    gridUser.Visibility = System.Windows.Visibility.Hidden;
                    gridPass.Visibility = System.Windows.Visibility.Hidden;
                    lblUser.Visibility = System.Windows.Visibility.Hidden;
                    btnLogin.Visibility = System.Windows.Visibility.Hidden;

                    //bat thread kiem tra he thong va load tham so he thong luon;
                    probar.Visibility = System.Windows.Visibility.Visible; //Hiên thị thanh progetbar
                    Thread _thr = new Thread(ThreadSystemCheck);
                    _thr.IsBackground = true;
                    _thr.Start();
                }
                else
                {
                    gridUser.Visibility = System.Windows.Visibility.Hidden;
                    lblUser.Visibility = System.Windows.Visibility.Visible;
                    gridPass.Visibility = System.Windows.Visibility.Visible;
                    btnLogin.Visibility = System.Windows.Visibility.Visible;

                    pwbPass.Password = "";
                    pwbPass.Focus();
                    lblUser.Content = "Chỉ có USER ABC XYZ mới đăng nhập được hệ thống.";
                    lblUser.Height = 28;
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (c_isLogin)
                    {
                        UserLogin();
                    }
                    else
                    {
                        UserLock();
                    }
                }
                if (e.Key == Key.CapsLock)
                {
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (c_isLogin)
            {
                UserLogin();
            }
            else
            {
                UserLock();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = NoteBox.Show(" Bạn có muốn thoát khỏi chương trình không?", "Thông báo", NoteBoxLevel.Question);
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                Application.Current.Shutdown();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUser.SelectAll();
        }

        private void pwbPass_GotFocus(object sender, RoutedEventArgs e)
        {
            pwbPass.SelectAll();
            pwbPass_KeyDown(null, null);
        }

        private void pwbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (Console.CapsLock == true)
            {
                tt_pass.Visibility = Visibility.Visible;
                //tt_pass.Placement = PlacementMode.Bottom;
                tt_pass.PlacementTarget = pwbPass;
                tt_pass.IsOpen = true;
            }
            else
                tt_pass.Visibility = Visibility.Hidden;
        }

        private void pwbPass_LostFocus(object sender, RoutedEventArgs e)
        {
            tt_pass.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        #endregion

        #region threadcheck

        private void ThreadSystemCheck()
        {
            try
            {
                c_LoadCommonDataStatus = 0; //cờ báo thực bắt dầu thực hiện kiểm tra;

                #region 1. Kiem tra ket noi WCF
                Controller _AllCodeController = new Controller();

                c_msgNotify = "Kiểm tra kết nối đến WCF service ...";
                ShowLabel(MsgType.NORMAL, c_msgNotify, 10);

                bool _Result = _AllCodeController.AllCode_checkWCF();
                if (_Result == false)
                {
                    c_LoadCommonDataStatus = 1; //kiẻm tra bị lôi
                    c_msgNotify = "Error: Lỗi kết nối đến WCF service";
                    ShowLabel(MsgType.ERROR, c_msgNotify, 30);
                    return;
                }

                _AllCodeController.UnSubscribe();
                #endregion

                c_LoadCommonDataStatus = 2;
                ShowLabel(MsgType.NORMAL, "kiểm tra kết nối WCF thành công", 100);

                //hiển thị lại các nút chức năng
                ShowLabel(MsgType.ERROR, "", 0);
                ShowControl();
            }
            catch
            {
                c_LoadCommonDataStatus = 1; //kiẻm tra bị lôi
                c_msgNotify = "Error: Lỗi khi load tham số hệ thống";
                ShowLabel(MsgType.ERROR, c_msgNotify, 0);
            }
        }

        #endregion

        #region private

        private void UserLogin()
        {
            try
            {
                lblLogin.Visibility = Visibility.Hidden;

                if (c_LoadCommonDataStatus == 0)
                {
                    MessageBox.Show("Đang kiểm tra/load tham số hệ thống, hay đợi trong giây lat !");
                    return;
                }
                else if (c_LoadCommonDataStatus == 1)
                {
                    MessageBox.Show(c_msgNotify);
                    return;
                }
                Controller _Controller = new Controller();
                User_Info _UserInfo = _Controller.User_Login(txtUser.Text, pwbPass.Password);

                if (_UserInfo.User_Name != null && _UserInfo.User_Name != "")
                {
                    NaviCommon.Common.ID = _UserInfo.User_Name;
                    Common.c_User_Info = _UserInfo;
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    lblLogin.Visibility = Visibility.Visible;
                    lblLogin.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Yellow);
                    lblLogin.Content = "Tài khoản hoặc mật khẩu không đúng";
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                lblLogin.Visibility = Visibility.Visible;
                lblLogin.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                lblLogin.Content = "Đăng nhập thất bại";
            }
        }

        private void UserLock()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                NoteBox.Show("Đăng nhập thất bại.", "Thông báo");
                App.Current.Shutdown();
            }
        }

        #endregion

        #region Delegate

        delegate void DelegateShowLabel(MsgType p_MsgType, string strMsg, int p_pgbValue);
        private void ShowLabel(MsgType p_MsgType, string strMsg, int p_pgbValue)
        {
            if (lblMsg.Dispatcher.CheckAccess() == false)
            {
                lblMsg.Dispatcher.Invoke(new DelegateShowLabel(ShowLabel), p_MsgType, strMsg, p_pgbValue);
            }
            else
            {
                lblMsg.Visibility = Visibility.Visible;
                lblMsg.Content = strMsg;
                //

                //
                switch (p_MsgType)
                {
                    case MsgType.ERROR:
                        this.lblMsg.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                        this.probar.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                        break;
                    default:
                        this.lblMsg.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                        this.probar.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.DarkGreen);
                        if (p_pgbValue > 0)
                        {
                            probar.Value = p_pgbValue;
                        }
                        break;

                }
            }
        }

        delegate void DelegateShowControl();
        private void ShowControl()
        {
            try
            {
                if (txtUser.Dispatcher.CheckAccess() == false)
                {
                    txtUser.Dispatcher.Invoke(new DelegateShowControl(ShowControl));
                }
                else
                {

                    lblUser.Visibility = System.Windows.Visibility.Hidden;
                    probar.Visibility = System.Windows.Visibility.Hidden;  //ẩn thanh proget bar
                    //kiem tra du lieu thanh cong, hien thi cac nut chưc nag
                    gridUser.Visibility = System.Windows.Visibility.Visible;
                    gridPass.Visibility = System.Windows.Visibility.Visible;
                    btnLogin.Visibility = System.Windows.Visibility.Visible;

                    try
                    {
                        //lay lai user dang nhap gan nhat
                    }
                    catch { }
                    txtUser.Focus();
                    txtUser.SelectAll();

                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        #endregion
    }
}
