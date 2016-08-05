using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaviCommon
{
    public class Common_Event
    {
        public static NVSEvent c_NVSEvent = new NVSEvent();
        public const string MESSAGETYPE_SPLIT = "|";
    }

    public class Key_Raise_Event
    {
        public const string Message = "M";
        public const string Session = "S";

    }
}
