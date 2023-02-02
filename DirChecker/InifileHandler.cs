using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DirChecker
{
    public static class InifileHandler
    {
        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileString")]
        private static extern uint
          _GetPrivateProfileString(string lpAppName,
          string lpKeyName, string lpDefault,
          StringBuilder lpReturnedString, uint nSize,
          string lpFileName);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileInt")]
        private static extern uint
          _GetPrivateProfileInt(string lpAppName,
          string lpKeyName, int nDefault, string lpFileName);

        [DllImport("KERNEL32.DLL")]
        private static extern uint WritePrivateProfileString(
          string lpAppName,
          string lpKeyName,
          string lpString,
          string lpFileName);

        public static string GetPrivateProfileString(string AppName, string KeyName, string DefaultValue, string IniFileName, uint BufferSize = 260)
        {
            StringBuilder buff = new StringBuilder((int)BufferSize);
            _GetPrivateProfileString(AppName, KeyName, DefaultValue, buff, BufferSize, IniFileName);
            if (string.IsNullOrWhiteSpace(buff.ToString()))
                return DefaultValue;
            else
                return buff.ToString();
        }

        public static int GetPrivateProfileInt(string AppName, string KeyName, int DefaultValue, string IniFileName)
        {
            return (int)_GetPrivateProfileInt(AppName, KeyName, DefaultValue, IniFileName);
        }

        public static bool WritePrivateProfileString(string AppName, string KeyName, object sValue, string IniFileName)
        {
            return WritePrivateProfileString(AppName, KeyName, sValue.ToString(), IniFileName) != 0;
        }
    }
}
