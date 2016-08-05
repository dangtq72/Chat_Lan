/* 
* Project: NaviCommon
* Author : Tong Quang Dang – Navisoft.
* Summary: cung cap cac ham chuyen doi giua cac kieu du lieu
* Modification Logs:
* DATE             AUTHOR      DESCRIPTION
* --------------------------------------------------------
* Apr 10, 2015  	Dangtq     Created
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NaviCommon
{
    public class ConvertData
    {

        #region Định dạng datetime

        public const string strDate = "dd/MM/yyyy";
        public const string strDate_Time = "dd/MM/yyyy HH:mm";

        public static string ConvertDate2String(DateTime p_date)
        {
            try
            {
                return p_date.ToString(strDate);
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return "";
            }
        }

        public static DateTime ConvertString2Date(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            string _separator = provider.DateTimeFormat.DateSeparator;
            string _dateformat = provider.DateTimeFormat.ShortDatePattern;

            try
            {
                return DateTime.ParseExact(str, _dateformat, provider); // strDate 
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        public static DateTime ConvertString2Date_dd_MM_yyyy(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            string _separator = provider.DateTimeFormat.DateSeparator;
            string _dateformat = provider.DateTimeFormat.ShortDatePattern;

            try
            {
                return DateTime.ParseExact(str, "dd-MM-yyyy", provider); // strDate 
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Convert string to datetime format dd/MM/yyyy HH:mm
        /// </summary>
        /// <param name="str">ví dụ 17/01/2015 09:10</param>
        /// <returns></returns>
        public static DateTime ConvertString2DateWithTime(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            try
            {
                return DateTime.ParseExact(str, strDate_Time, provider);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        #endregion

        #region Dinh dang kieu Money
        public static String ConvertString2Money(string str)
        {

            try
            {
                decimal _st = Convert.ToDecimal(str);
                //"{0:0.##}"
                //"{0:0,0 VND}"
                return String.Format("{0:0,0.##}", _st);

            }
            catch
            {
                return "";
            }
        }

        public static string NhapMoney(TextBox txt)
        {
            try
            {
                if (!CheckValidate.checkmoney(txt.Text) && txt.Text != "")
                {
                    string s = txt.Text;
                    if (s.Length > 1)
                        txt.Text = s.Remove(s.Length - 1, 1);
                    else txt.Text = "";
                    txt.Select(txt.Text.Length, 0);
                }
                return txt.Text;
            }
            catch (Exception ex)
            {
                return "";
                throw ex;
            }
        }

        public static Decimal ConvertStringMoney2Decimal(string soTien)
        {
            try
            {
                Decimal tid = Convert.ToDecimal(soTien);
                return tid;
            }
            catch
            {
                return -1;
            }

        }
        #endregion

        #region kieu image

        public static Byte[] BitmapImage2Byte(BitmapImage imageSource)
        {
            try
            {
                //resize ve 32x32
                Rect rect = new Rect(0, 0, 32, 32);
                // Create a DrawingVisual/Context to render with
                System.Windows.Media.DrawingVisual drawingVisual = new System.Windows.Media.DrawingVisual();
                using (System.Windows.Media.DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    drawingContext.DrawImage(imageSource, rect);
                }

                RenderTargetBitmap resizedImage = new RenderTargetBitmap(32, 32, 96, 96, System.Windows.Media.PixelFormats.Default);
                resizedImage.Render(drawingVisual);
                //
                MemoryStream memStream = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(resizedImage));
                encoder.Save(memStream);
                return memStream.GetBuffer();
            }
            catch
            {
                return new Byte[0];
            }

        }

        public static BitmapImage Byte2BitmapImage(byte[] rawImageData)
        {
            try
            {
                MemoryStream stream = new MemoryStream(rawImageData);
                stream.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = stream;
                bi.EndInit();
                return bi;
            }
            catch
            {
                return new BitmapImage();
            }
        }

        public static Byte[] string2byte(string t)
        {
            try
            {
                return Encoding.ASCII.GetBytes(t);
            }
            catch
            {
                return new Byte[0];
            }
        }

        public static string byte2string(Byte[] t)
        {
            try
            {
                return Encoding.ASCII.GetString(t);
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region Convert DataTable

        public static void ConvertArrayListToDataTable(ArrayList arrayList, ref DataTable p_dt)
        {
            //DataTable dt = new DataTable();

            if (arrayList.Count != 0)
            {
                ConvertObjectToDataTableSchema(arrayList[0], ref p_dt);
                FillData(arrayList, p_dt);
            }

            //return p_dt;
        }

        public static void ConvertObjectToDataTableSchema(Object o, ref DataTable dt)
        {
            //DataTable dt = new DataTable();
            PropertyInfo[] properties = o.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                DataColumn dc = new DataColumn(property.Name);
                dc.DataType = property.PropertyType; dt.Columns.Add(dc);
            }
            //return dt;
        }

        private static void FillData(ArrayList arrayList, DataTable dt)
        {
            foreach (Object o in arrayList)
            {
                DataRow dr = dt.NewRow();
                PropertyInfo[] properties = o.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    dr[property.Name] = property.GetValue(o, null);
                }
                dt.Rows.Add(dr);
            }
        }

        public static DataTable ConvertToDatatable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                table.Columns.Add("STT");
                object[] values = new object[props.Count + 1];
                foreach (T item in data)
                {
                    SoThuTu++;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    values[values.Length - 1] = SoThuTu.ToString();
                    table.Rows.Add(values);
                }
                return table;
            }
            catch (Exception)
            {
                return new DataTable();
            }

        }

        public static DataSet ConvertToDataSet<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                for (int i = 0; i < props.Count; i++)
                {
                    PropertyDescriptor prop = props[i];
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }

                int SoThuTu = 0;
                table.Columns.Add("STT");
                object[] values = new object[props.Count + 1];
                foreach (T item in data)
                {
                    SoThuTu++;
                    for (int i = 0; i < values.Length - 1; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }
                    values[values.Length - 1] = SoThuTu.ToString();
                    table.Rows.Add(values);
                }

                DataSet _ds = new DataSet();
                _ds.Tables.Add(table);
                return _ds;
            }
            catch (Exception)
            {
                return new DataSet();
            }

        }

        #endregion
    }
}
