using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjInfo
{
    [Serializable]
    public class CallBackData_Info
    {
        public string Key { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
        public decimal Level { get; set; }
    }
}
