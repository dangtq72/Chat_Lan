using DesktopAlert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NaviCommon;

namespace Nvs_Monitor
{
    /// <summary>
    /// Interaction logic for Alert_Common.xaml
    /// </summary>
    public partial class Alert_Common : DesktopAlertBase
    {
        public Alert_Common()
        {
            InitializeComponent();
        }

        public static DependencyProperty IsSoundProperty = DependencyProperty.Register("IsSound", typeof(bool), typeof(SimpleAlert));
        public static DependencyProperty SoundFileProperty = DependencyProperty.Register("SoundFile", typeof(string), typeof(SimpleAlert));

        public static DependencyProperty DBProperty = DependencyProperty.Register("MSG", typeof(string), typeof(SimpleAlert));
        public static DependencyProperty UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(SimpleAlert));

        [Bindable(true)]
        public bool IsSound
        {
            get { return (bool)GetValue(IsSoundProperty); }
            set { SetValue(IsSoundProperty, value); }
        }

        [Bindable(true)]
        public string SoundFile
        {
            get { return (string)GetValue(SoundFileProperty); }
            set { SetValue(SoundFileProperty, value); }

        }

        [Bindable(true)]
        public string Msg
        {
            get { return (string)GetValue(DBProperty); }
            set { SetValue(DBProperty, value); }
        }

        [Bindable(true)]
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        SoundEffect _SoundEffect;
        private void DesktopAlertBase_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var bc = new BrushConverter();
                txtMsg.Foreground = (Brush)bc.ConvertFrom("#45ff45");
                txtMsg.Content = Msg;
                if (IsSound)
                {
                    _SoundEffect = new SoundEffect(SoundFile);
                    _SoundEffect.Play();
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }

        private void DesktopAlertBase_Closing(object sender, CancelEventArgs e)
        {
            if (_SoundEffect != null)
            {
                _SoundEffect.Stop();
            }
        }

        private void DesktopAlertBase_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Common_Event.c_NVSEvent.WhenClickAlert(UserName, UserName);
        }
    }
}
