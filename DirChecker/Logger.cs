using System;
using System.IO;
using System.Diagnostics;
namespace DirChecker
{
    public interface ILogger
    {
        void WriteLog(string log);
        void OpenLog();
        void DeleteLog();
    }
    public class TextLogger : ILogger
    {
        #region ログのパス
        private string _logpath = string.Empty;
        public string logpath
        {
            get
            {
                if (string.IsNullOrEmpty(_logpath))
                {
                    _logpath = Path.Combine(Path.GetTempPath(), "DirChecker.log");
                }
                return _logpath;
            }
            set
            {
                _logpath = value;
            }
        }
        #endregion

        public void WriteLog(string msg)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logpath, true))
                {
                    sw.WriteLine(msg);
                }
            }
            catch { } //ログの出力でエラーになった場合は潰す
        }
        public void OpenLog()
        {
            if (!File.Exists(logpath))
                throw new FileNotFoundException("ログファイルがありません。\r\nパス：" + logpath);
            Process.Start("file://" + logpath);
        }
        public void DeleteLog()
        {
            if (!File.Exists(logpath))
                throw new FileNotFoundException("ログファイルがありません。\r\nパス：" + logpath);
            File.Delete(logpath);
        }
    }
}
