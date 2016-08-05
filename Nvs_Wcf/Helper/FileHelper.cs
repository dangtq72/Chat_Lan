using System;
using System.Text;
using System.IO;


namespace Nvs_Wcf
{
    public class FileHelper
    {
        #region Contructor

        public FileHelper(string p_FileName)
            : this(p_FileName, "LogData")
        {
        }

        public FileHelper(string p_FileName, string p_PathFile)
        {
            try
            {
                //string filepath = System.IO.Path.Combine(AppPath(), filename);
                c_FileName = p_FileName;
                c_FullName = System.IO.Path.Combine(p_PathFile, p_FileName);
                if (!Directory.Exists(p_PathFile))
                    Directory.CreateDirectory(p_PathFile);

                System.IO.FileStream _FileStream = System.IO.File.Open(c_FullName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.ReadWrite);
                _FileStream.Seek(0, SeekOrigin.End);
                c_StreamWriter = new StreamWriter(_FileStream);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion

        #region parameter

        private string c_FullName;
        private string c_FileName;
        private StreamWriter c_StreamWriter;
        private object c_objLock = new object();

        #endregion

        #region Public Method
        public void WriteData(string strData)
        {
            try
            {
                lock (c_objLock)
                {
                    c_StreamWriter.WriteLine(strData);
                    c_StreamWriter.Flush();
                }
            }
            catch
            {
            }
        }
        public void CloseFileData()
        {
            try
            {
                c_StreamWriter.Flush();
                c_StreamWriter.Close();
            }
            catch
            {
            }
        }
        #endregion
    }
}
