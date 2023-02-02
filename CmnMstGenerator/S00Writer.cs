using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace CmnMstGenerator
{
    public static class S00Writer
    {
        public static void WriteS00(string _outPath, List<TableInfo> _tiList)
        {
            #region 埋め込みリソースから基となる文字列を取得する
            Assembly myAssembly = Assembly.GetExecutingAssembly();

            //TABLEDEF
            Stream strm = myAssembly.GetManifestResourceStream("CmnMstGenerator.Resource.TABLEDEF.txt");
            StreamReader sr = new StreamReader(strm, Encoding.GetEncoding("shift-jis"));
            string TableDefineBase = sr.ReadToEnd();

            //PRIMARYKEY
            strm = myAssembly.GetManifestResourceStream("CmnMstGenerator.Resource.PRIMARYKEY.txt");
            sr = new StreamReader(strm, Encoding.GetEncoding("shift-jis"));
            string PrimaryKeyDefineBase = sr.ReadToEnd();

            //INDEXDEF
            strm = myAssembly.GetManifestResourceStream("CmnMstGenerator.Resource.INDEXDEF.txt");
            sr = new StreamReader(strm, Encoding.GetEncoding("shift-jis"));
            string IndexDefineBase = sr.ReadToEnd();
            #endregion

            #region 出力するsqlを作成する
            StringBuilder sql = new StringBuilder();

            foreach (TableInfo ti in _tiList)
            {
                sql.AppendLine();

                //TABLEDEF BEGIN
                string tableDefSql = TableDefineBase;
                tableDefSql = tableDefSql.Replace("[TABLENAME]", ti.Name);

                StringBuilder fieldSql = new StringBuilder(); //
                //[FIELDDEFINE]部を作成する
                foreach (Field f in ti.FieldList)
                {
                    string rec = "    ";
                    rec += f.Name;

                    //30字目からデータ型の記述をスタートする（超える場合は半角を1つ空けて次の文字から）            
                    int blankCount = rec.Length > 28 ? 1 : 29 - rec.Length;
                    for (int i = 0; i < blankCount; i++)
                    {
                        rec += " ";
                    }

                    bool setDigits = false;
                    string dataType = f.DataType.ConvertDataType(ref setDigits);
                    //testCode
                    if (dataType == "INT")
                    {
                        dataType = f.Digits.ConvertIntegerType();
                    }
                    //testCode end
                    rec += dataType;

                    string digits = "";
                    if (setDigits)
                    {
                        digits += f.Digits;
                        digits += dataType == "NUMERIC" ? "," + f.DigitsDicimal : "";
                        digits = string.Format("({0})", digits);
                    }
                    rec += digits;

                    if (f.SetDefault)
                    {
                        if (f.DataType.ToUpper() == "DEFDATE2")
                        {
                            //()つきと無しが混在しているので2回置換をする
                            f.DefaultValue = f.DefaultValue.Replace("now()", "now");
                            f.DefaultValue = f.DefaultValue.Replace("now", "CURRENT_TIMESTAMP");
                        }
                        else if (f.DataType.ToUpper() == "CHAR" || f.DataType.ToUpper() == "VARCHAR")
                        {
                            //CHAR、VARCHARの場合は''で括る
                            f.DefaultValue = string.Format("\'{0}\'", f.DefaultValue);
                        }
                        rec += string.Format(" default {0}", f.DefaultValue);
                    }

                    if (!f.Nullable)
                    {
                        rec += " not null";
                    }

                    if (f.DataType.ToString() == "AUTONUM")
                    {
                        rec += " default autoincrement";
                    }

                    rec += ",";
                    fieldSql.AppendLine(rec);
                }

                //[FIELDDEFINE]の「constraint～」部を作成する
                if (ti.PrimaryKeyList.Count() > 0)
                {
                    string primaryKeyDefSql = PrimaryKeyDefineBase.Replace("[TABLENAME]", ti.Name);
                    primaryKeyDefSql = primaryKeyDefSql.Replace("[PRIMARYKEY]", String.Join(",", ti.PrimaryKeyList.ToArray()));
                    fieldSql.AppendLine("    " + primaryKeyDefSql);
                }

                tableDefSql = tableDefSql.Replace("[FIELDDEFINE]", fieldSql.ToString().TrimEnd('\r', '\n').TrimEnd(','));

                sql.AppendLine(tableDefSql);
                //TABLEDEF END

                //INDEXDEF BEGIN
                if (ti.IndexList.Count() > 0)
                {
                    foreach (IndexInfo idxInfo in ti.IndexList)
                    {
                        string indexDefSql = IndexDefineBase;
                        indexDefSql = indexDefSql.Replace("[TABLENAME]", ti.Name);
                        indexDefSql = indexDefSql.Replace("[UNIQUE]", idxInfo.IsUnique ? "unique " : "");
                        indexDefSql = indexDefSql.Replace("[KEYNAME]", idxInfo.KeyName);
                        indexDefSql = indexDefSql.Replace("[FIELDS]", String.Join(",", idxInfo.FieldNameList.ToArray()));
                        sql.AppendLine(indexDefSql);
                    }
                }
                //INDEXDEF END
            }
            #endregion

            #region sqlの出力を行う

            string outPath = Path.Combine(_outPath, "0000-共通マスタ.S00");
            using (StreamWriter sw = new StreamWriter(outPath, false, Encoding.GetEncoding("Shift_JIS")))
            {
                sw.Write(sql.ToString());
            }

            #endregion
        }

        private static string ConvertDataType(this string _baseStr, ref bool _setDigists)
        {
            string rtnStr = _baseStr;
            switch (_baseStr.ToUpper())
            {
                case "NUMERIC":
                case "AUTONUM":
                    rtnStr = "NUMERIC";
                    _setDigists = true;
                    break;
                case "NUMERIC2":
                    rtnStr = "INT";
                    break;
                case "VARCHAR":
                case "CHAR":
                    _setDigists = true;
                    break;
                case "DATE2":
                case "DEFDATE2":
                    rtnStr = "TIMESTAMP";
                    break;
                case "BLOB":
                    rtnStr = "IMAGE";
                    break;
            }
            return rtnStr;
        }

        private static string ConvertIntegerType(this int digits)
        {
            if (0 <= digits && digits <= 2)
            {
                return "TINYINT";
            }
            else if (3 <= digits && digits <= 4)
            {
                return "SMALLINT";
            }
            else if (5 <= digits && digits <= 8)
            {
                return "INT";
            }
            else if (9 <= digits)
            {
                return "BIGINT";
            }
            else
            {
                return "INT";
            }
        }
    }
}
