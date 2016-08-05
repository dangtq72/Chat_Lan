using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaviCommon
{
    public class NVSEvent
    {

        //Delegate
        public delegate void NVSEventHandler(object sender, NVSEventArgs e);

        // Event TAM
        public event NVSEventHandler WhenReceiveCallBackDataEvent;
        public event NVSEventHandler WhenClickAlertEvent;

        //Methods 
        public void WhenReceiveCallBackData(object sender, object obj)
        {
            if (WhenReceiveCallBackDataEvent != null)
            {
                WhenReceiveCallBackDataEvent(sender, new NVSEventArgs(obj));
            }
        }

        public void WhenClickAlert(object sender, object obj)
        {
            if (WhenClickAlertEvent != null)
            {
                WhenClickAlertEvent(sender, new NVSEventArgs(obj));
            }
        }
    }


    public class NVSEventArgs : EventArgs
    {
        object _obj;

        public NVSEventArgs(object obj)//message for a peer connected
        {

            _obj = obj;
        }

        public object Obj
        {
            get
            {
                return _obj;
            }
        }
    }
}


