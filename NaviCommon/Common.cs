using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections;
using ObjInfo;

namespace NaviCommon
{
    public class Common
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static User_Info c_User_Info;
        public static string ID;

        static NaviCommon.MyQueue c_queue_send = new NaviCommon.MyQueue();

        public static void Enqueue_Send(object p_object)
        {
            c_queue_send.Enqueue(p_object);
        }

        public static object Get_Object_Info_Send()
        {
            try
            {
                object _object = (object)c_queue_send.Dequeue();
                if (_object != null)
                {
                    return _object;
                }
                else return null;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }       
    }

}
