using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NaviCommon
{
    public class Mes_Type
    {
        public static string Msg_Monitor = "M";
        public static string Msg_Operator = "O";
        public static string Msg_Error = "E";

    }

    public enum Enum_User_Status
    {
        DisConnect = 0,
        Connected = 1
    }

    public enum Enum_Session_Status
    {
        LogOut = 0,
        LogIn = 1
    }

    public enum Enum_Message_Status
    {
        OK = 1,
        Not_OK = 0
    }

    public enum Enum_Message_Type
    {
        Send = 1,
        Receive = 2
    }
}
