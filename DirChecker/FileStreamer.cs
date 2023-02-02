using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DirChecker
{
    abstract class FileStreamer
    {
        #region private val
        static string _CurrentDirectory = string.Empty;
        protected string CurrentDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_CurrentDirectory))
                {
                    _CurrentDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
                return _CurrentDirectory;
            }
        }

        string _SettingFile = string.Empty;
        protected string SettingFile
        {
            get
            {
                if (string.IsNullOrEmpty(_SettingFile))
                {
                    _SettingFile = Path.Combine(CurrentDirectory, FileName);
                }
                return _SettingFile;
            }
        }
        #endregion

        protected virtual string FileName
        {
            get { throw new NotImplementedException("サブクラスでFileNameが定義されていません。"); }
        }

        public virtual List<CheckDirEntity> Read()
        {
            if (!File.Exists(SettingFile))
            {
                throw new FileNotFoundException("設定ファイルが存在しません:" + SettingFile);
            }
            return null;
        }

        public virtual void Write(List<CheckDirEntity> list)
        {
            File.Create(SettingFile).Close();
        }
    }

    class IniFileStreamer : FileStreamer
    {
        protected override string FileName
        {
            get
            {
                return "DirChecker.ini";
            }
        }

        public override List<CheckDirEntity> Read()
        {
            base.Read();
            List<CheckDirEntity> retlist = new List<CheckDirEntity>();

            int count = GetIntValueFromIni("SETTING", "count");
            for (int i = 1; i <= count; i++)
            {
                CheckDirEntity tmp = new CheckDirEntity();
                string section = string.Format("SETTING_{0:00}", i);
                tmp.name = GetStringValueFromIni(section, "Name");
                tmp.directory = GetStringValueFromIni(section, "Directory");
                tmp.CheckDir = ConvertStrToBool(GetStringValueFromIni(section, "CheckDir"));
                tmp.ExportLog = ConvertStrToBool(GetStringValueFromIni(section, "ExportLog"));
                tmp.DesktopFilesMaxCount = GetIntValueFromIni(section, "DesktopFilesMaxCount");
                retlist.Add(tmp);
            }

            return retlist;
        }

        public override void Write(List<CheckDirEntity> list)
        {
            base.Write(list);

            int count = list.Count();
            WriteToIni("SETTING", "count", count);

            for (int i = 0; i < count; i++)
            {
                string section = string.Format("SETTING_{0:00}", i + 1);
                CheckDirEntity tmp = list[i];
                WriteToIni(section, "Name", tmp.name);
                WriteToIni(section, "Directory", tmp.directory);
                WriteToIni(section, "CheckDir", tmp.CheckDir);
                WriteToIni(section, "ExportLog", tmp.ExportLog);
                WriteToIni(section, "DesktopFilesMaxCount", tmp.DesktopFilesMaxCount);
            }
        }

        private string GetStringValueFromIni(string section, string key)
        {
            return InifileHandler.GetPrivateProfileString(section, key, "", SettingFile);
        }

        private int GetIntValueFromIni(string section, string key)
        {
            return InifileHandler.GetPrivateProfileInt(section, key, 0, SettingFile);
        }

        private void WriteToIni(string section, string key, object value)
        {
            InifileHandler.WritePrivateProfileString(section, key, value, SettingFile);
        }

        private bool ConvertStrToBool(string origin)
        {
            bool result;
            if (!Boolean.TryParse(origin, out result))
            {
                return false;
            }
            return result;
        }
    }

    class XmlFileStreamer : FileStreamer
    {
        protected override string FileName
        {
            get
            {
                return "DirChecker.config";
            }
        }

        public override List<CheckDirEntity> Read()
        {
            base.Read();

            List<CheckDirEntity> retlist = new List<CheckDirEntity>();
            XmlSerializer serializer = new XmlSerializer(typeof(List<CheckDirEntity>));
            using (StreamReader sr = new StreamReader(SettingFile, Encoding.GetEncoding("shift-jis")))
            {
                retlist = (List<CheckDirEntity>)serializer.Deserialize(sr);
            }

            return retlist;
        }

        public override void Write(List<CheckDirEntity> list)
        {
            base.Write(list);

            XmlSerializer serializerSeries = new XmlSerializer(typeof(List<CheckDirEntity>));
            using (StreamWriter sw = new StreamWriter(SettingFile, false, Encoding.GetEncoding("shift-jis")))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add(String.Empty, String.Empty);
                serializerSeries.Serialize(sw, list, ns);
            }
        }
    }
}
