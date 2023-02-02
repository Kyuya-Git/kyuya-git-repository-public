using System.Collections.Generic;

namespace CmnMstGenerator
{
    public class TableInfo
    {
        public string Name { get; set; }
        public List<Field> FieldList { get; set; }
        public List<string> PrimaryKeyList { get; set; }
        public List<IndexInfo> IndexList { get; set; }

        //テスト
        public int LastRowIndex { get; set; }

        public TableInfo()
        {
            FieldList = new List<Field>();
            PrimaryKeyList = new List<string>();
            IndexList = new List<IndexInfo>();
        }
    }

    public class Field
    {
        private string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _DataType;
        public string DataType {
            get { return _DataType; }
            set { _DataType = value; }
        }

        private int _Digits;
        public int Digits {
            get { return _Digits; }
            set { _Digits = value; }
        }

        private int _DigitsDicimal;
        public int DigitsDicimal {
            get { return _DigitsDicimal; }
            set { _DigitsDicimal = value; }
        }

        private bool _Nullable;
        public bool Nullable {
            get { return _Nullable; }
            set { _Nullable = value; }
        }

        private bool _SetDefault;
        public bool SetDefault {
            get { return _SetDefault; }
            set { _SetDefault = value; }
        }

        private string _DefaultValue;
        public string DefaultValue {
            get { return _DefaultValue; }
            set { _DefaultValue = value; }
        }
    }

    public class IndexInfo
    {
        public string KeyName { get; set; }
        public bool IsUnique { get; set; }
        public List<string> FieldNameList { get; set; }
        public IndexInfo()
        {
            FieldNameList = new List<string>();
        }
    }
}
