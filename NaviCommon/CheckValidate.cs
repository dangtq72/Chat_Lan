using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Controls;
using System.Net;

namespace NaviCommon
{
    public class CheckValidate
    {
        static public string g_NguoiTao = "HNX";
        static public string g_NguoiSua = "HNX";

        public static bool CheckValidDate(string str)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;
            DateTime temp;

            try
            {
                temp = DateTime.ParseExact(str, "dd/MM/yyyy", provider);

                if (temp < DateTime.ParseExact("01/01/1800", "dd/MM/yyyy", provider))
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        // xem lai neu st khong phai la so
        public static bool CheckDecimal(string st)
        {
            //string Str = textBox1.Text.Trim();
            decimal Num;

            bool isNum = decimal.TryParse(st, out Num);

            if (isNum)
                return true;

            else
                return false;

        }

        // xem lai neu st khong phai la so
        public static bool CheckDouble(string st)
        {
            //string Str = textBox1.Text.Trim();
            double Num;

            bool isNum = double.TryParse(st, out Num);

            if (isNum)
                return true;

            else
                return false;
        }


        // xem lai neu st khong phai la so 
        //La so duong neu >= 0;
        public static bool CheckDecimal_duong(string st)
        {
            //string Str = textBox1.Text.Trim();
            decimal Num;

            decimal.TryParse(st, out Num);

            if (Num <= 0)
                return false;
            else
                return true;
        }

        // xem lai neu st khong phai la so
        public static bool CheckDecimal_am(string st)
        {
            //string Str = textBox1.Text.Trim();
            decimal Num;

            decimal.TryParse(st, out Num);

            if (Num < 0)
                return false;
            else
                return true;
        }

        public static bool CheckInt(string st)
        {
            //string Str = textBox1.Text.Trim();
            Int64 Num;

            Int64.TryParse(st, out Num);

            if (Num > 0 || st.Equals("0"))
                return true;
            else
                return false;
        }

        public static bool checkValidEmail(string strEmail)
        {
            try
            {
                //@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                //string pattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
                string pattern = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
                Regex rx = new Regex(pattern, RegexOptions.IgnoreCase);

                if (!rx.IsMatch(strEmail))
                    return false;
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Hàm check ký tự đặc biệt
        /// </summary>
        /// <param name="pStrContent">Nội dung chuối truyền vào check </param>
        /// <returns>True : False</returns>
        public static bool CheckSpecialCharactor(string pStrContent)
        {
            try
            {
                if (pStrContent.ToString().Contains("@") == true || pStrContent.ToString().Contains("#") == true || pStrContent.ToString().Contains("!") == true
                    || pStrContent.ToString().Contains("$") == true || pStrContent.ToString().Contains("%") == true || pStrContent.ToString().Contains("^") == true
                    || pStrContent.ToString().Contains("&") == true || pStrContent.ToString().Contains("*") == true || pStrContent.ToString().Contains("(") == true
                    || pStrContent.ToString().Contains(")") == true || pStrContent.ToString().Contains("_") == true
                    || pStrContent.ToString().Contains("=") == true || pStrContent.ToString().Contains("+") == true || pStrContent.ToString().Contains("\\") == true
                    || pStrContent.ToString().Contains("|") == true || pStrContent.ToString().Contains("`") == true || pStrContent.ToString().Contains("~") == true
                    || pStrContent.ToString().Contains("<") == true || pStrContent.ToString().Contains(">") == true || pStrContent.ToString().Contains("?") == true
                    || pStrContent.ToString().Contains("/") == true || pStrContent.ToString().Contains(".") == true || pStrContent.ToString().Contains("{") == true
                    || pStrContent.ToString().Contains("}") == true || pStrContent.ToString().Contains("[") == true || pStrContent.ToString().Contains("]") == true
                    || pStrContent.ToString().Contains(";") == true || pStrContent.ToString().Contains(":") == true || pStrContent.ToString().Contains(",") == true
                    || pStrContent.ToString().Contains(Convert.ToString('"')) == true || pStrContent.ToString().Contains("'") == true)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                Common.log.Error(ex.ToString()); return false;
            }
        }


        /// <summary>
        /// Kiểm tra ký tự đặc biệt
        /// </summary>
        /// <param name="strnewAcc"></param>
        /// <returns></returns>
        public static bool checkCharacterAZ09(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if ((convertedPw >= 65) && (convertedPw <= 90))
                    {
                        //ky tu hoa
                        _ck = true;
                    }
                    else if ((convertedPw >= 97) && (convertedPw <= 122))
                    {
                        //ky tu thuong 
                        _ck = true;
                    }
                    else if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        //ky tu so
                        _ck = true;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool checkkytudacbiet(string s1)
        {
            try
            {
                if (s1.ToString().Contains("@") == true || s1.ToString().Contains("#") == true || s1.ToString().Contains("!") == true
                    || s1.ToString().Contains("$") == true || s1.ToString().Contains("%") == true || s1.ToString().Contains("^") == true
                    || s1.ToString().Contains("&") == true || s1.ToString().Contains("*") == true || s1.ToString().Contains("(") == true
                    || s1.ToString().Contains(")") == true || s1.ToString().Contains("_") == true
                    || s1.ToString().Contains("=") == true || s1.ToString().Contains("+") == true || s1.ToString().Contains("\\") == true
                    || s1.ToString().Contains("|") == true || s1.ToString().Contains("`") == true || s1.ToString().Contains("~") == true
                    || s1.ToString().Contains("<") == true || s1.ToString().Contains(">") == true || s1.ToString().Contains("?") == true
                    || s1.ToString().Contains("/") == true || s1.ToString().Contains(".") == true || s1.ToString().Contains("{") == true
                    || s1.ToString().Contains("}") == true || s1.ToString().Contains("[") == true || s1.ToString().Contains("]") == true
                    || s1.ToString().Contains(";") == true || s1.ToString().Contains(":") == true || s1.ToString().Contains(",") == true
                    || s1.ToString().Contains(Convert.ToString('"')) == true || s1.ToString().Contains("'") == true)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Check ký tự đặc biệt cho tên file gửi lưu ký ( có chứa ký tự _)
        /// </summary>
        public static bool Checkkytudacbiet_FileName(string s1)
        {
            try
            {
                if (s1.ToString().Contains("@") == true || s1.ToString().Contains("#") == true || s1.ToString().Contains("!") == true
                    || s1.ToString().Contains("$") == true || s1.ToString().Contains("%") == true || s1.ToString().Contains("^") == true
                    || s1.ToString().Contains("&") == true || s1.ToString().Contains("*") == true || s1.ToString().Contains("(") == true
                    || s1.ToString().Contains(")") == true
                    || s1.ToString().Contains("=") == true || s1.ToString().Contains("+") == true || s1.ToString().Contains("\\") == true
                    || s1.ToString().Contains("|") == true || s1.ToString().Contains("`") == true || s1.ToString().Contains("~") == true
                    || s1.ToString().Contains("<") == true || s1.ToString().Contains(">") == true || s1.ToString().Contains("?") == true
                    || s1.ToString().Contains("/") == true || s1.ToString().Contains(".") == true || s1.ToString().Contains("{") == true
                    || s1.ToString().Contains("}") == true || s1.ToString().Contains("[") == true || s1.ToString().Contains("]") == true
                    || s1.ToString().Contains(";") == true || s1.ToString().Contains(":") == true || s1.ToString().Contains(",") == true
                    || s1.ToString().Contains(Convert.ToString('"')) == true || s1.ToString().Contains("'") == true)
                {
                    return false;
                }
                else return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// check all là khoảng trắng- dangtq
        /// = true là chứa all space
        /// </summary>
        public static bool checkAllSpace(string s)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = s.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(s[counter]);

                    if (convertedPw == 32)
                    {
                        _ck = true;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }
                return _ck;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }

        public static bool checkso(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        _ck = true;
                    }
                    else if (convertedPw == 46) //dau cham
                    {
                        _ck = true;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// dang tq check so nguyen ap dung cho cac truong tien te va hien thi dau , ngan cach 
        /// </summary>
        /// <param name="strnewAcc"></param>
        /// <returns></returns>
        public static bool checkmoney(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if (counter == 0 && convertedPw == 44)
                    {
                        _ck = false;
                        break;
                    }

                    if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        _ck = true;
                    }
                    else if ((convertedPw == 44) || (convertedPw == 46))//dau phay va dau cham
                    {
                        _ck = true;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// dang tq check so số thập phân 
        /// </summary>
        /// <param name="strnewAcc"></param>
        /// <returns></returns>
        public static bool checksothapphan(string strnewAcc)
        {
            try
            {
                bool _ck = false;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = strnewAcc.GetEnumerator();
                int count = 0;
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(strnewAcc[counter]);

                    if ((convertedPw >= 48) && (convertedPw <= 57))
                    {
                        _ck = true;
                    }
                    else if (convertedPw == 46 && count <= 0)//dau phay va dau cham
                    {
                        _ck = true;
                        count++;
                    }
                    else
                    {
                        _ck = false;
                        break;
                    }
                    counter++;
                }

                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// check định dạng của % 
        /// </summary>
        /// <returns></returns>
        public static bool CheckFormatPercent(string percent)
        {
            try
            {

                bool _ck = true;
                int count_46 = 0;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = percent.GetEnumerator();
                while (charEnum.MoveNext())
                {
                    convertedPw = Convert.ToInt32(percent[counter]);
                    if (convertedPw == 46) // dau cham
                        count_46++;
                    counter++;
                }

                if (count_46 > 1)
                    _ck = false;
                return _ck;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// kiểm tra khoảng %  (0 - 100)
        /// </summary>
        /// <param name="strMoveRatio"></param>
        /// <returns></returns>
        public static bool CheckRangePercent(string percent)
        {
            try
            {
                decimal _ratio;
                _ratio = Convert.ToDecimal(percent);

                if (_ratio >= 0 && _ratio <= 100)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// su kien keydown cho datepicker
        /// </summary>
        public static void DateTime_KeyDown(TextBox txt, DatePicker DatePicker, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space)
                {
                    DatePicker.IsDropDownOpen = true;
                }
                if (DatePicker.Focusable == true)
                {
                    if (e.Key == Key.Escape)
                    {
                        DatePicker.IsDropDownOpen = false;
                        e.Handled = true;
                    }
                    else if (e.Key == Key.Enter)
                    {
                        DatePicker.IsDropDownOpen = false;
                        txt.Text = ConvertData.ConvertDate2String(DatePicker.SelectedDate.Value.Date);
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //check ty le PH ck 
        public static bool checkReleaseRate(string p_strReleaseRate)
        {
            try
            {
                bool _ck = false;
                int _dau2cham_cout = 0;
                int _dau2cham_vitri = 0;
                int counter = 0;
                int convertedPw;
                CharEnumerator charEnum = p_strReleaseRate.GetEnumerator();

                if (Convert.ToInt32(p_strReleaseRate[0]) == 32 || Convert.ToInt32(p_strReleaseRate[0]) == 48 || Convert.ToInt32(p_strReleaseRate[0]) == 58 || Convert.ToInt32(p_strReleaseRate[counter]) == 58)
                {
                    return false;
                }
                else
                {
                    #region check chuoi neu ktu dau va cuoi ko phai la dau 2 cham, ky tu dau tien ko phai la so 0 hoac khoang trang
                    while (charEnum.MoveNext())
                    {
                        convertedPw = Convert.ToInt32(p_strReleaseRate[counter]);
                        // dangtq sửa theo index bug 48108
                        //if ((convertedPw >= 48) && (convertedPw <= 57))
                        //{   //ky tu so
                        //    _ck = true;
                        //}

                        // new
                        if (((convertedPw >= 48) && (convertedPw <= 57)) || (convertedPw == 46))
                        {   //ky tu so hoac la dau . phan cach thap phan
                            _ck = true;
                        }
                        else if (convertedPw == 58 && _dau2cham_cout < 2)
                        {

                            _dau2cham_vitri = counter;
                            _dau2cham_cout += 1;
                            _ck = true;
                        }
                        else
                        {
                            _ck = false;
                            break;
                        }

                        counter++;
                    }
                    #endregion
                }

                if (_ck == true)
                {
                    //check xem dau 2 cham o vi tri dau tien hay cuoi cung 
                    if (_dau2cham_cout == 0 || Convert.ToInt32(p_strReleaseRate[_dau2cham_vitri + 1]) == 48)
                    {
                        _ck = false;
                    }
                }
                return _ck;
            }
            catch (Exception)
            {
                return false;
            }

        }

        // sangdd sửa 
        //Kiểm tra khoảng trắng trong chuỗi.True tức là chuỗi có khoảng trắng.
        public static bool check_Space(string str)
        {
            bool _ck = false;
            if (str.Contains(" "))
            {
                _ck = true;
            }

            return _ck;
        }

        //check du lieu la Ty le( 0< tl < 100) - HuongLL

        public static bool CheckRate(string st)
        {
            decimal Num;

            bool isNum = decimal.TryParse(st, out Num);

            if (isNum && Num > 0)
            {
                return true;
            }
            else
                return false;
        }

        public static bool IsValidIp(string addr)
        {
            try
            {
                IPAddress ip;
                bool valid = !string.IsNullOrEmpty(addr) && IPAddress.TryParse(addr, out ip);
                return valid;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckPasswordStrength(string p_Password)
        {
            try
            {
                int _numberMatch = 0;
                if (p_Password.Length < 8)
                    return false;

                if (Regex.IsMatch(p_Password, @"\d+")) _numberMatch++;  //check co chua so khong
                if (Regex.IsMatch(p_Password, @"[a-z]")) _numberMatch++; //check co chua chu thuong khong
                if (Regex.IsMatch(p_Password, @"[A-Z]")) _numberMatch++; //check co chua chu hoa khong
                if (Regex.IsMatch(p_Password, @"[!@#\$%\^&\*\?_~\-\(\);\.\+:]+")) _numberMatch++;

                if (CheckIsUnicode(p_Password)) _numberMatch++;
                if (CheckIsUnicode_Upper(p_Password)) _numberMatch++;

                if (_numberMatch >= 3) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool CheckIsUnicode(string p_unicode)
        {
            try
            {
                bool _ck = false;
                string[] pattern = new string[7];
                pattern[0] = "á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ";
                pattern[1] = "ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ";
                pattern[2] = "é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ";
                pattern[3] = "ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ";
                pattern[4] = "í|ì|ỉ|ị|ĩ";
                pattern[5] = "ý|ỳ|ỷ|ỵ|ỹ";
                pattern[6] = "đ";

                // kiểm tra xem có chữ tiếng việt thường hay không
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(p_unicode, pattern[i]);
                    if (matchs.Count > 0)
                        return true;
                }

                return _ck;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIsUnicode_Upper(string p_unicode)
        {
            try
            {
                bool _ck = false;
                string[] pattern = new string[7];

                pattern[0] = "á|ả|à|ạ|ã|ă|ắ|ẳ|ằ|ặ|ẵ|â|ấ|ẩ|ầ|ậ|ẫ";
                pattern[1] = "ó|ỏ|ò|ọ|õ|ô|ố|ổ|ồ|ộ|ỗ|ơ|ớ|ở|ờ|ợ|ỡ";
                pattern[2] = "é|è|ẻ|ẹ|ẽ|ê|ế|ề|ể|ệ|ễ";
                pattern[3] = "ú|ù|ủ|ụ|ũ|ư|ứ|ừ|ử|ự|ữ";
                pattern[4] = "í|ì|ỉ|ị|ĩ";
                pattern[5] = "ý|ỳ|ỷ|ỵ|ỹ";
                pattern[6] = "đ";

                // kiểm tra xem có chữ tiếng việt thường hay không
                for (int i = 0; i < pattern.Length; i++)
                {
                    MatchCollection matchs = Regex.Matches(p_unicode, pattern[i].ToUpper());
                    if (matchs.Count > 0)
                        return true;
                }

                return _ck;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckIsTime_HHmmss(string p_string_time, string p_delimiter = ":")
        {
            try
            {
                return Regex.IsMatch(p_string_time, @"([0-1][0-9]|[2][0-3])[ ]?" + p_delimiter + "[ ]?[0-5][0-9][ ]?" + p_delimiter + "[ ]?[0-5][0-9]");
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckCompareTime_HHmmss(string p_from_time, string p_to_time, string p_err_condition = ">")
        {
            try
            {
                //string userInput = "15:43:13";
                //var time = TimeSpan.Parse(userInput);
                DateTime fromdateTime = DateTime.Today.Add(TimeSpan.Parse(p_from_time));
                DateTime todateTime = DateTime.Today.Add(TimeSpan.Parse(p_to_time));
                bool _ck = true;

                if (p_err_condition == ">" && fromdateTime > todateTime) // from > to => lỗi
                    _ck = false;
                else if (p_err_condition == ">=" && fromdateTime >= todateTime) // from >= to => lỗi
                    _ck = false;
                else if (p_err_condition == "=" && fromdateTime == todateTime) // from = to => lỗi
                    _ck = false;
                else if (p_err_condition == "<=" && fromdateTime <= todateTime) // from <= to => lỗi
                    _ck = false;
                else if (p_err_condition == "<" && fromdateTime < todateTime) // from , to => lỗi
                    _ck = false;
                else if (p_err_condition == "<>" && fromdateTime != todateTime) // from != to => lỗi
                    _ck = false;

                return _ck;
            }
            catch
            {
                return false;
            }
        }

        public static bool CheckCompareDate_ddMMyyyy(string p_from_date, string p_to_date, string p_err_condition = ">")
        {
            try
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.CurrentCulture;

                DateTime fromdateTime = DateTime.ParseExact(p_from_date, "dd/MM/yyyy", provider);
                DateTime todateTime = DateTime.ParseExact(p_to_date, "dd/MM/yyyy", provider);
                bool _ck = true;

                if (p_err_condition == ">" && fromdateTime > todateTime) // from > to => lỗi
                    _ck = false;
                else if (p_err_condition == ">=" && fromdateTime >= todateTime) // from >= to => lỗi
                    _ck = false;
                else if (p_err_condition == "=" && fromdateTime == todateTime) // from = to => lỗi
                    _ck = false;
                else if (p_err_condition == "<=" && fromdateTime <= todateTime) // from <= to => lỗi
                    _ck = false;
                else if (p_err_condition == "<" && fromdateTime < todateTime) // from , to => lỗi
                    _ck = false;
                else if (p_err_condition == "<>" && fromdateTime != todateTime) // from != to => lỗi
                    _ck = false;

                return _ck;
            }
            catch
            {
                return false;
            }
        }
    }
}
