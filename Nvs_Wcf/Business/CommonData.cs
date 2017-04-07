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
        public static string GConnectionString = "";

        public static bool GetData()
        {
            try
            {
                GConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

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

                XmlNodeList xnList = xml.SelectNodes("Data");
                foreach (XmlNode xn in xnList)
                {
                    XmlElement _User = xn["USERS"];
                    foreach (XmlLinkedNode xn_s in _User)
                    {
                        User_Info _User_Info = new User_Info();
                        _User_Info.User_Name = xn_s["User_Name"].InnerText;

                        _User_Info.Password = xn_s["Pass"].InnerText;
                        _User_Info.Online_Status = Convert.ToInt16(xn_s["Online_Status"].InnerText);
                        _User_Info.Status = xn_s["Status"].InnerText;
                        _User_Info.IsGroup = Convert.ToInt16(xn_s["IsGroup"].InnerText);
                        _User_Info.List_Member = new List<Member_Info>();
                        if (_User_Info.IsGroup == 1)
                        {
                            XmlElement _member = xn_s["MEMBERS"];
                            foreach (XmlLinkedNode xn_m in _member)
                            {
                                Member_Info _Member_Info = new Member_Info();
                                _Member_Info.Member_Name = xn_m["NAME"].InnerText;

                                _User_Info.List_Member.Add(_Member_Info);
                            }

                        }

                        User_Interface _User_Interface = new User_Interface(_User_Info);
                        DBMemory.c_dic_User_Interface[_User_Info.User_Name] = _User_Interface;
                    }
                }

                #region ko dung
                //foreach (XmlNode node in xml.DocumentElement.ChildNodes)
                //{
                //    if (node.Name == "USER")
                //    {
                //        User_Info _User_Info = new User_Info();

                //        foreach (XmlNode locNode in node)
                //        {
                //            _User_Info.Online_Status = (int)Enum_User_Status.DisConnect;
                //            if (locNode.Name == "User_Name")
                //                _User_Info.User_Name = locNode.InnerText;
                //            else if (locNode.Name == "Pass")
                //                _User_Info.Pass = locNode.InnerText;
                //            else if (locNode.Name == "Ip")
                //                _User_Info.Ip = locNode.InnerText;
                //            else if (locNode.Name == "Online_Status")
                //                _User_Info.Online_Status = Convert.ToInt16(locNode.InnerText);
                //            else if (locNode.Name == "Status")
                //                _User_Info.Status = locNode.InnerText;
                //            else if (locNode.Name == "IsGroup")
                //                _User_Info.Status = locNode.InnerText;
                //            else if (locNode.Name == "MEMBERS")
                //            {
                //                foreach (XmlNode locNode_member in locNode)
                //                {

                //                }
                //            }
                //        }

                //        User_Interface _User_Interface = new User_Interface(_User_Info);
                //        DBMemory.c_dic_User_Interface[_User_Info.User_Name] = _User_Interface;
                //    }
                //} 
                #endregion

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
