using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Nvs_Monitor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private CultureInfo ci;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
                ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                Thread.CurrentThread.CurrentCulture = ci;
                NaviCommon.Common.log.Error("Da chay chuyen dinh dang ngay dd/MM/yyyy");
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
            }
        }
    }
}
