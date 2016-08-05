using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjInfo;
using System.Xml;
using NaviCommon;

namespace Nvs_Wcf
{
    public class CommonData
    {
        public static bool GetData()
        {
            try
            {
                //Illegal EscapedUsed
                //------------------
                //"   &quot;
                //'   &apos;
                //<   &lt;
                //>   &gt;
                //&   &amp;

                // đọc file config của index
                string _xml_index = @"Data\Data.xml";
                XmlDocument xml = new XmlDocument();
                xml.Load(_xml_index);

                foreach (XmlNode node in xml.DocumentElement.ChildNodes)
                {
                    if (node.Name == "USER")
                    {
                        User_Info _User_Info = new User_Info();

                        foreach (XmlNode locNode in node)
                        {
                            _User_Info.Online_Status = (int)Enum_User_Status.DisConnect;
                            if (locNode.Name == "User_Name")
                                _User_Info.User_Name = locNode.InnerText;
                            else if (locNode.Name == "Pass")
                                _User_Info.Pass = locNode.InnerText;
                            else if (locNode.Name == "Ip")
                                _User_Info.Ip = locNode.InnerText;
                            else if (locNode.Name == "Online_Status")
                                _User_Info.Online_Status = Convert.ToInt16(locNode.InnerText);
                            else if (locNode.Name == "Status")
                                _User_Info.Status = locNode.InnerText;
                        }

                        User_Interface _User_Interface = new User_Interface(_User_Info);
                        DBMemory.c_dic_User_Interface[_User_Info.User_Name] = _User_Interface;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return false;
            }
        }

    }
}
