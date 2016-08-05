using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace ObjInfo
{
    public class Request_Info<T>
    {
        public string Msg_Type { get; set; }
        public string Version { get; set; }
        public T Request { get; set; }
    }

    public class Request_List_Info<T>
    {
        public string Msg_Type { get; set; }
        public string Version { get; set; }
        public List<T> Request { get; set; }
    }

    public class Request_Info
    {
        public string Msg_Type { get; set; }
        public string Version { get; set; }
        public string Request { get; set; }
    }

    public class Reponse_List_Info<T>
    {
        public string Msg_Type { get; set; }
        public string Version { get; set; }
        public List<T> Reponse { get; set; }
    }


    public class Operator_Info
    {
        public Operator_Info() { }
        public Operator_Info(string p_appName, int p_App_Type, string p_path, int p_type)
        {
            App_Name = p_appName;
            Path = p_path;
            Type = p_type;
            App_Type = p_App_Type;
            Status = 0;
        }

        public string App_Name { get; set; }
        public string Path { get; set; }
        public int Type { get; set; }
        public int App_Type { get; set; }

        public int Status { get; set; }
    }
}
