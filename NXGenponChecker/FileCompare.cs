using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NXGenponChecker
{
    static class FileCompare
    {
        public static bool Compare(this string file1, string file2)
        {
            if (file1 == file2)
                return true;

            FileStream fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read);
            FileStream fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read);
            int byte1;
            int byte2;
            bool ret = false;

            try
            {
                if (fs1.Length == fs2.Length)
                {
                    do
                    {
                        byte1 = fs1.ReadByte();
                        byte2 = fs2.ReadByte();
                    }
                    while ((byte1 == byte2) && (byte1 != -1));

                    if (byte1 == byte2)
                        ret = true;
                }
            }
            catch
            {
            }
            finally
            {
                fs1.Close();
                fs2.Close();
            }

            return ret;
        }
    }
}
