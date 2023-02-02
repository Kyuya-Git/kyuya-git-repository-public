using System.IO;
using System.Text;
using System;
namespace EnableFeatures
{
    public class FXLogger
    {
        public string FilePath
        {
            get;
            private set;
        }
        public FXLogger(string _filePath)
        {
            FilePath = _filePath;
        }

        public void Write(string msg, params object[] param)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(FilePath, true, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(DateTime.Now + " " + string.Format(msg, param));
                }
            }
            catch { }
        }

        public void WriteError(string msg, params object[] param) 
        {
            Write("★" + msg, param);
        }
    }
}
