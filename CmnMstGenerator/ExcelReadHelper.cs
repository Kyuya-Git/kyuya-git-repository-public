using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;

namespace CmnMstGenerator
{
    public class ExcelReadHelper
    {
        public static object GetValue(IWorkbook _workBook, CurrentPosition _cp)
        {
            IRow row = _workBook.GetSheetAt(_cp.SheetIndex).GetRow(_cp.RowIndex);
            ICell cell = row == null ? null : row.GetCell(_cp.ColumnIndex);
            if (cell == null)
                return "";

            //セルの型に応じたプロパティを参照する
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    else
                        return cell.NumericCellValue;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Formula:
                    //return cell.CellFormula;
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Blank:
                    return "";
                default:
                    return null;
            }
        }
    }
}
