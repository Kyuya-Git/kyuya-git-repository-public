using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CmnMstGenerator
{
    public class CurrentPosition
    {
        public string FileName { get; set; }
        public int SheetIndex { get; set; }
        public string SheetName { get; set; }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }

        public CurrentPosition()
        {
            FileName = "";
            SheetName = "";
            RowIndex = 0;
            ColumnIndex = 0;
        }
    }
}
