using System;
using System.Text;
using System.IO;
using System.Collections.Generic;


namespace NaviCommon
{
    public class FileHelper
    {
        #region parameter

        private string c_PathFile;
        private string c_FileName;
        private StreamWriter c_StreamWriter;

        #endregion

        #region Contructor

        public FileHelper(string p_FileName, string p_PathFile,string p_extend_name)
        {
            try
            {
                c_FileName = p_FileName;
                c_PathFile = p_PathFile + "\\" + p_FileName + p_extend_name;
                c_PathFile = c_PathFile.Replace("\\\\", "\\");
                if (!Directory.Exists(p_PathFile))
                    Directory.CreateDirectory(p_PathFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Public Method

        public void OpenforWrite()
        {
            try
            {
                System.IO.FileStream _FileStream = System.IO.File.Open(c_PathFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                _FileStream.Seek(0, SeekOrigin.End);

                c_StreamWriter = new StreamWriter(_FileStream);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public void WriteData(string strData)
        {
            try
            {
                c_StreamWriter.WriteLine(strData);
                c_StreamWriter.Flush();
            }
            catch
            { 
            }
        }

        public List<string> ReadAllData()
        {
            try
            {
                List<string> _result = new List<string>();
                if (System.IO.File.Exists(c_PathFile))
                {
                    System.IO.FileStream _FileStream = System.IO.File.Open(c_PathFile, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
                    StreamReader _StreamReader = new StreamReader(_FileStream);
                    while (!_StreamReader.EndOfStream)
                    {
                        string s = _StreamReader.ReadLine();
                        if (s.Length > 50) _result.Add(s);
                    }

                    //kiểm tra xem cuối file đã xuống dòng chưa? nếu chưa thì thêm xuống dòng vào
                    if  (_FileStream.Length>0)
                    {
                        _StreamReader.BaseStream.Seek(-1, SeekOrigin.End);
                        char[] c = new char[1];
                        _StreamReader.Read(c, 0, 1);
                        if (c[0] != 10)
                        {
                            StreamWriter c_StreamWriter = new StreamWriter(_FileStream);
                            c_StreamWriter.Write(Environment.NewLine);
                            c_StreamWriter.Flush();
                            c_StreamWriter.Close();
                        }
                    }
                    _StreamReader.Close();
                    _FileStream.Close();

                }
                return _result;
            }
            catch
            {
                throw;
            }
        }


        public void CloseFileData()
        {
            try
            {
                c_StreamWriter.Flush();
                c_StreamWriter.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
