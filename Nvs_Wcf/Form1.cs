using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceModel;
using System.Text;
using System.Windows.Forms;

namespace Nvs_Wcf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
        }

        ServiceHost serviceHost;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                NaviCommon.Common.log.Error("Bat dau khoi tao service ....");

                if (CommonData.GetData())
                {

                    serviceHost = new ServiceHost(typeof(NvsService));
                    serviceHost.Open();
                    lblStatus.Text = "Service Start";
                    lblStatus.ForeColor = System.Drawing.Color.Blue;
                    NaviCommon.Common.log.Error("Khoi tao service thanh cong ....");
                }
                else
                {
                    lblStatus.Text = "Lỗi rồi";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                lblStatus.Text = "Lỗi rồi";
                lblStatus.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
