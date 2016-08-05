using GemBox.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;

namespace NaviCommon
{
    public class CommonFuc
    {

        public const string strDate = "dd/MM/yyyy";
        private static readonly Random rand = new Random();

        public static String FormatNumber(decimal pStr)
        {
            try
            {
                if (pStr != 0)
                {
                    return String.Format("{0:#,###}", pStr);
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "0";
            }
        }

        /// <summary>
        /// Hàm làm tròn lên số double 1.2 ->2 ..
        /// </summary>
        /// <param name="pDoubleNum">Giá trị kiểu double </param>
        /// <returns>trả về kiểu Integer với giá trị làm tròn lên </returns>
        public static int RoundUp(double pDoubleNum)
        {
            try
            {
                return Convert.ToInt32(Math.Ceiling(pDoubleNum).ToString());
            }
            catch (Exception ex)
            {
                Common.log.Error("Loi Double : " + pDoubleNum.ToString() + ex.ToString());
                return -1;
            }
        }

        public static string Encrypt(string toEncrypt)
        {
            try
            {
                var md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedBytes;
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(toEncrypt));
                StringBuilder s = new StringBuilder();
                foreach (byte _hashedByte in hashedBytes)
                {
                    s.Append(_hashedByte.ToString("x2"));
                }
                return s.ToString();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static DateTime CurrentDate()
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                //string IFormatDate = "dd/MM/yyyy HH:mm tt"; //khoong lay giay 
                string IFormatDate = "dd/MM/yyyy HH:mm:ss";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return DateTime.ParseExact(pStr, IFormatDate, provider);

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }
        /// <summary>
        /// Lấy ngày tháng duy nhất 
        /// </summary>
        /// <returns></returns>
        public static DateTime TruncDate()
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                string IFormatDate = "dd/MM/yyyy";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return DateTime.ParseExact(pStr, IFormatDate, provider);

            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Lay ten luu lai anh cho do bi trung ten khi tai anh len tu client
        /// </summary>
        /// <returns></returns>
        public static string GetImageNames()
        {
            try
            {
                string IFormatDate = "ddMMyyyyhhmmss";
                string pStr = DateTime.Now.ToString(IFormatDate);
                return pStr;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return DateTime.Now.ToString("ddMMyyyyhhmmss");
            }
        }

        /// <summary>
        /// truyền vào ngày tháng trả ra thứ mấy 
        /// </summary>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public static string GetDayOfWeek(DateTime pDate, string pLanguage)
        {
            try
            {
                string pStr = "";
                int day = Convert.ToInt32(pDate.DayOfWeek);
                if (!string.IsNullOrEmpty(pLanguage))
                {
                    if (pLanguage.ToUpper() == "VI")
                    {
                        if (day == 1)
                        {
                            pStr = "Thứ hai";
                        }
                        else if (day == 2)
                        {
                            pStr = "Thứ ba";
                        }
                        else if (day == 3)
                        {
                            pStr = "Thứ tư";
                        }
                        else if (day == 4)
                        {
                            pStr = "Thứ năm";
                        }
                        else if (day == 5)
                        {
                            pStr = "Thứ sáu";
                        }
                        else if (day == 6)
                        {
                            pStr = "Thứ bẩy";
                        }
                        else if (day == 0)
                        {
                            pStr = "Chủ nhật";
                        }
                    }
                    else
                    {
                        if (day == 1)
                        {
                            pStr = "Monday";
                        }
                        else if (day == 2)
                        {
                            pStr = "Tuesday";
                        }
                        else if (day == 3)
                        {
                            pStr = "Wednesday";
                        }
                        else if (day == 4)
                        {
                            pStr = "Thursday";
                        }
                        else if (day == 5)
                        {
                            pStr = "Friday";
                        }
                        else if (day == 6)
                        {
                            pStr = "Saturday";
                        }
                        else if (day == 0)
                        {
                            pStr = "Sunday";
                        }
                    }
                }
                else
                {
                    if (day == 1)
                    {
                        pStr = "Monday";
                    }
                    else if (day == 2)
                    {
                        pStr = "Tuesday";
                    }
                    else if (day == 3)
                    {
                        pStr = "Wednesday";
                    }
                    else if (day == 4)
                    {
                        pStr = "Thursday";
                    }
                    else if (day == 5)
                    {
                        pStr = "Friday";
                    }
                    else if (day == 6)
                    {
                        pStr = "Saturday";
                    }
                    else if (day == 0)
                    {
                        pStr = "Sunday";
                    }
                }

                return pStr;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string Create_OTP_Code()
        {
            try
            {
                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int _r = rand.Next(1, 2);
                    if (_r == 1)
                    {
                        c = Convert.ToChar(Convert.ToInt32(rand.Next(48, 57)));
                    }
                    else
                    {
                        c = Convert.ToChar(Convert.ToInt32(rand.Next(97, 122)));
                    }
                    sb.Append(c);
                }

                return sb.ToString().ToLower();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Đọc file excel ra dataset
        /// </summary>
        /// <param name="filelocation">Tên file</param>
        public static DataSet ReadXlsxFile(string filelocation)
        {
            try
            {
                string HDR = "No";
                //string HDR = "Yes";

                string IMEX = "0";
                string strConn;
                //if (filelocation.Substring(filelocation.LastIndexOf('.')).ToLower() == ".xlsx")
                //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
                //else
                //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

                // IMEX=1 là excel hiển thị như thế nào thì nó sẽ đọc như thế
                // donght ko can check version nữa mặc định mình sẽ cài thằng 2013 nên ko cần check thằng thấp hơn .xls
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=" + IMEX + "\"";

                DataSet output = new DataSet();

                using (OleDbConnection conn = new OleDbConnection(strConn))
                {
                    conn.Open();

                    DataTable schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                    foreach (DataRow schemaRow in schemaTable.Rows)
                    {
                        string sheet = schemaRow["TABLE_NAME"].ToString();

                        if (!sheet.EndsWith("_"))
                        {
                            try
                            {
                                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                                cmd.CommandType = CommandType.Text;

                                DataTable outputTable = new DataTable(sheet);
                                output.Tables.Add(outputTable);
                                new OleDbDataAdapter(cmd).Fill(outputTable);
                            }
                            catch (Exception ex)
                            {
                                Common.log.Error(ex.Message);
                            }
                        }
                    }
                }

                return output;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static DataSet FillDataSetFromExcel(string filePath)
        {
            try
            {
                DataSet ds_return = new DataSet();
                // Lưu file vao server
                //string filePath = Microsoft.SqlServer.Server.MapPath("/Content/FlexcelDesignFile/" + uploadFile.FileName);
                //uploadFile.SaveAs(filePath);

                // Đọc file Excel ra DataSet
                string file_extension = System.IO.Path.GetExtension(filePath);
                string connectionString_excel = "";
                switch (file_extension.ToUpper())
                {
                    case ".XLS": //Excel 97-03
                        connectionString_excel = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1;\"");
                        break;
                    case ".XLSX": //Excel 07~
                        connectionString_excel = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"");
                        break;
                }
                OleDbConnection con = new OleDbConnection(connectionString_excel);
                con.Open();

                // Get lis SheetName in file
                List<string> lst_sheetname = new List<string>();
                DataTable dt = new DataTable();
                dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string item = dt.Rows[i]["TABLE_NAME"].ToString().Replace("'", "");
                    if (item.Contains("$"))
                        lst_sheetname.Add(item);
                }

                // Get data in file
                if (lst_sheetname.Count > 0)
                {
                    for (int sheet_index = 0; sheet_index < lst_sheetname.Count; sheet_index++)
                    {
                        OleDbDataAdapter cmd = new System.Data.OleDb.OleDbDataAdapter("select * from [" + lst_sheetname[sheet_index].ToString() + "]", con);
                        DataTable dt_data = new DataTable();
                        cmd.Fill(dt_data);
                        ds_return.Tables.Add(dt_data);
                    }
                }
                con.Close();

                // Return DataSet
                return ds_return;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return new DataSet();
            }
        }

        /// <summary>
        /// Định dạng lại cái số điện thoại nhập vào
        /// </summary>
        /// <param name="p_phone"></param>
        /// <returns></returns>
        public static string FormatPhone(string p_phone)
        {
            try
            {
                string result = p_phone.Replace(".", "").Replace(" ", "").Replace("+84", "0").Replace("+", "").Replace("(", "").Replace(")", "").Replace("-", "");
                string subStr = result.Substring(0, 2);
                string _kq = result.Substring(2, result.Length - 2);
                if (subStr == "84")
                {

                    result = "0" + _kq;

                }
                else
                {
                    if (result.IndexOf("0") != 0)
                    {
                        result = "0" + result;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return null;
            }
        }

        public static string Create_Random_Character(int p_number_get)
        {
            try
            {

                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                Random rand = new Random();
                for (int i = 0; i < p_number_get; i++)
                {
                    c = Convert.ToChar(rand.Next(97, 122));
                    sb.Append(c);
                }
                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static string Create_Random_Password()
        {
            try
            {
                // chu thuong 97 - 122
                // chu hoa 65 - 87
                // so 48 - 57
                StringBuilder sb = new StringBuilder();
                char c;
                //Random rand = new Random();
                for (int i = 0; i < 10; i++)
                {
                    int _r = rand.Next(1, 4);
                    if (_r == 1)
                    {
                        c = Convert.ToChar(rand.Next(48, 57));
                    }
                    else
                    {
                        c = Convert.ToChar(rand.Next(97, 122));
                    }
                    sb.Append(c);
                }

                return sb.ToString().ToUpper();
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString());
                return null;
            }
        }

        #region Ngôn ngữ
        public static string ResourceDictionary2String(ResourceDictionary p_ResourceDictionary)
        {
            try
            {
                System.Text.StringBuilder _sb = new System.Text.StringBuilder();
                StringWriter _sw = new StringWriter(_sb);

                System.Windows.Markup.XamlWriter.Save(p_ResourceDictionary, _sw);
                return _sb.ToString();
            }
            catch
            {
                return "";
            }
        }

        public static ResourceDictionary GetResourceDictionaryfromString(string xamlResource)
        {
            try
            {
                if (xamlResource != "")
                {
                    StringReader _StringReader = new StringReader(xamlResource);
                    System.Xml.XmlReader _XmlReader = System.Xml.XmlReader.Create(_StringReader);

                    ResourceDictionary dict;
                    dict = (ResourceDictionary)System.Windows.Markup.XamlReader.Load(_XmlReader);
                    return dict;
                }
                else
                    return new ResourceDictionary();
            }
            catch
            {
                return new ResourceDictionary();
            }
        }

        public static ResourceDictionary ApplyLanguage(string cultureName = "", string moduleName = "")
        {

            try
            {
                string _path_lang = AppDomain.CurrentDomain.BaseDirectory + "Language\\";//tên đường dẫn đến file chưa Language của prj

                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                ResourceDictionary dict = new ResourceDictionary();
                string path;
                path = _path_lang + moduleName + "." + cultureName + ".xaml";
                Uri _uri = new Uri(path, UriKind.RelativeOrAbsolute);
                dict.Source = new Uri(path, UriKind.RelativeOrAbsolute);
                return dict;
            }
            catch
            {
                return new ResourceDictionary();
            }
        }

        #endregion

        /// <summary>
        /// Lấy danh sách các children control của 1 parent control. Đây là các childen theo Logical, tức là các childen mà khai báo. 
        /// Ví dụ sử dụng: foreach (RibbonButton tb in FindLogicalChildren<RibbonButton>(this))
        /// {
        ///         _str = _str + tb.Name + ";";
        /// }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static IEnumerable<T> FindLogicalChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {

                foreach (object _obj in LogicalTreeHelper.GetChildren(depObj))
                {
                    if (_obj != null && _obj is T)
                    {
                        yield return (T)_obj;
                    }

                    if (_obj is DependencyObject)
                    {
                        foreach (T childOfChild in FindLogicalChildren<T>((DependencyObject)_obj))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lay control cha cua 1 control bat ky
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static T FindLogicalParent<T>(DependencyObject depObj) where T : DependencyObject
        {
            try
            {
                T parent = default(T);
                if (depObj != null)
                {
                    DependencyObject _obj = LogicalTreeHelper.GetParent(depObj);
                    parent = _obj as T;
                    if (parent == null)
                    {
                        parent = FindLogicalParent<T>(_obj);
                    }
                }
                return parent;
            }
            catch
            {
                return default(T);
            }
        }

        public static T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        {
            var count = VisualTreeHelper.GetChildrenCount(parentElement);
            if (count == 0)
                return null;

            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parentElement, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    var result = FindFirstElementInVisualTree<T>(child);
                    if (result != null)
                        return result;

                }
            }
            return null;
        }

        public static bool CompareObject<T>(T Object1, T object2)
        {
            //Get the type of the object
            Type type = typeof(T);

            //return false if any of the object is false
            if (Object1 == null || object2 == null)
                return false;

            //Loop through each properties inside class and get values for the property from both the objects and compare
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                // riêng đối vs thằng time (định kỳ tính) thì không xét
                if (property.Name == "Board_Time" || property.Name == "Time")
                    continue;

                if (property.Name != "ExtensionData")
                {
                    string Object1Value = string.Empty;
                    string Object2Value = string.Empty;
                    if (type.GetProperty(property.Name).GetValue(Object1, null) != null)
                        Object1Value = type.GetProperty(property.Name).GetValue(Object1, null).ToString();
                    if (type.GetProperty(property.Name).GetValue(object2, null) != null)
                        Object2Value = type.GetProperty(property.Name).GetValue(object2, null).ToString();
                    if (Object1Value.Trim() != Object2Value.Trim())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        //public static DataSet Get_DataDB(string p_connection, string p_sql)
        //{
        //    try
        //    {
        //        return OracleHelper.ExecuteDataset(p_connection, CommandType.Text, p_sql);
        //    }
        //    catch (Exception ex)
        //    {
        //        NaviCommon.Common.log.Error(ex.ToString());
        //        return new DataSet();
        //    }
        //}
    }
}
