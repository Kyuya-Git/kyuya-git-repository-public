using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NXGenponChecker
{
    public class RecordEntity
    {
        public string BaseFolderName = "";
        public string BaseFolderFullName = "";
        public List<TargetFileInfo> AppSvr;
        public List<TargetFileInfo> CtrlSvr;

        public RecordEntity()
        {
            AppSvr = new List<TargetFileInfo>();
            CtrlSvr = new List<TargetFileInfo>();
        }
    }

    public class TargetFileInfo
    {
        public string Path;
        public bool Reflected;
    }
}
