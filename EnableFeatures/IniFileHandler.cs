using System;
using System.Text;
using System.Runtime.InteropServices;

namespace EnableFeatures
{
    class IniFileHandler
    {
        [DllImport("KERNEL32.DLL")]
        private static extern uint
          GetPrivateProfileString(string lpAppName,
          string lpKeyName, string lpDefault,
          StringBuilder lpReturnedString, uint nSize,
          string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern uint
          GetPrivateProfileInt(string lpAppName,
          string lpKeyName, int nDefault, string lpFileName);

        public static string GetStringValue(string section, string key, string fileName)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(section, key, "", sb, (uint)sb.Capacity, fileName);
            return sb.ToString();
        }

        public static int GetIntValue(string section, string key, string fileName)
        {
            return (int)GetPrivateProfileInt(section, key, 0, fileName);
        }
    }
}
