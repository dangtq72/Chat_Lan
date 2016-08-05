using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ObjInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace NaviCommon
{
    public class JsonFactory
    {

        public static string BuildString(object p_obj)
        {
            try
            {

                string _json = Newtonsoft.Json.JsonConvert.SerializeObject(p_obj, Newtonsoft.Json.Formatting.None);
                return _json;
            }
            catch
            {
                return "";
            }
        }

        public static T ParseString<T>(string p_strObj)
        {
            try
            {

                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(p_strObj);
            }
            catch
            {
                return default(T);
            }
        }

        public static string Create_Json_Send<T>(string p_mes_type, T p_object)
        {
            try
            {
                Request_Info<T> _Mes_Req_Info = new Request_Info<T>();
                _Mes_Req_Info.Msg_Type = p_mes_type;
                _Mes_Req_Info.Request = p_object;
                _Mes_Req_Info.Version = "1.0";
                return JsonConvert.SerializeObject(_Mes_Req_Info, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static string Create_Json_Send_String(string p_mes_type, string p_mes)
        {
            try
            {
                Request_Info _Request_Info = new Request_Info();
                _Request_Info.Msg_Type = p_mes_type;
                _Request_Info.Request = p_mes;
                _Request_Info.Version = "1.0";
                return JsonConvert.SerializeObject(_Request_Info, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static string Create_List_Json_Send<T>(string p_mes_type, List<T> p_list)
        {
            try
            {
                Reponse_List_Info<T> _Send_Mes_Info = new Reponse_List_Info<T>();
                _Send_Mes_Info.Msg_Type = p_mes_type;
                _Send_Mes_Info.Reponse = p_list;
                _Send_Mes_Info.Version = "1.0";
                return JsonConvert.SerializeObject(_Send_Mes_Info, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Get mes type
        /// </summary>
        public static string Get_Mes_Type(string p_mes, ref string p_version)
        {
            try
            {
                JObject _JObject = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(p_mes);
                string _s = _JObject["Msg_Type"].ToString();
                p_version = _JObject["Version"].ToString();
                return _s;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        /// <summary>
        /// Get mes type
        /// </summary>
        public static T Get_Object_Send<T>(string p_mes)
        {
            try
            {
                JObject _JObject = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(p_mes);

                return _JObject.ToObject<T>();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return default(T);
            }
        }

        public static string SerializeXml(XmlDocument doc)
        {
            try
            {
                return JsonConvert.SerializeXmlNode(doc);
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }
    }


}
