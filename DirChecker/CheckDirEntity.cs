using System;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using System.IO;

namespace DirChecker
{
    [XmlRoot("CheckDirEntity")]
    public class CheckDirEntity
    {
        [XmlAttribute("Name")]
        public string name { get; set; }
        [XmlElement("Directory")]
        public string directory { get; set; }
        [XmlElement("CheckDir")]
        public bool CheckDir { get; set; }
        [XmlElement("ExportLog")]
        public bool ExportLog { get; set; }
        [XmlElement("DesktopFilesMaxCount")]
        public int DesktopFilesMaxCount { get; set; }
    }
}
