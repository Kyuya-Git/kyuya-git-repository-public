using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;

namespace EnableFeatures
{
    public class FeatureManager
    {
        public static List<FeatureEntity> GetFeatures()
        {
            //dism.exeを実行しすべての機能の有効化状態を取得する
            List<KeyValuePair<string, bool>> allFeatures = new List<KeyValuePair<string, bool>>();

            using (Process pr = new Process())
            {
                pr.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
                pr.StartInfo.Arguments =
                    string.Format("/c %windir%\\{0}\\dism.exe /online /get-features /format:table /english",
                                    Environment.Is64BitOperatingSystem ? "sysnative" : "system32");
                pr.StartInfo.RedirectStandardOutput = true;
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);

                pr.Start();

                while (!pr.StandardOutput.EndOfStream)
                {
                    string tmp = pr.StandardOutput.ReadLine();
                    if (string.IsNullOrWhiteSpace(tmp))
                        continue;

                    string[] values = tmp.Split('|');
                    if (values.Length != 2)
                        continue;

                    if (values[1].Trim().ToUpper() == "Enabled".ToUpper())
                    {
                        allFeatures.Add(new KeyValuePair<string, bool>(values[0].Trim(), true));
                    }
                    else if (values[1].Trim().ToUpper() == "Disabled".ToUpper())
                    {
                        allFeatures.Add(new KeyValuePair<string, bool>(values[0].Trim(), false));
                    }
                }
            }

            //すべての機能の有効化状態（allFeatures）と対象機能（INI）を比較して対象機能に絞ったリストを返す
            List<FeatureEntity> targetFeatures = new List<FeatureEntity>();

            //INIファイルを読み込む
            string iniFilePth = Path.Combine(new FileInfo(System.Reflection.Assembly.GetEntryAssembly().Location).DirectoryName, "EnableFeatures.ini");

            string sec = "Features";
            int FeatureCount = IniFileHandler.GetIntValue(sec, "Count", iniFilePth);

            if (FeatureCount < 1) throw new Exception("INIファイルからの対象機能数の取得に失敗しました。");

            for (int i = 1; i < FeatureCount + 1; i++)
            {
                string val = IniFileHandler.GetStringValue(sec, string.Format("Feature{0:00}", i), iniFilePth);

                if (string.IsNullOrEmpty(val)) throw new Exception("INIファイルに誤りがある可能性があります。");

                string[] splitVal = val.Split('|');

                if (splitVal.Length < 2) throw new Exception(string.Format("INIファイルに誤りがある可能性があります（{0:00}）。", i));

                var tgt = allFeatures.FirstOrDefault(f => f.Key.ToUpper() == splitVal[0].ToUpper());

                if (tgt.Key == null) throw new Exception(string.Format("対象の機能が実行されているOSで見つかりませんでした（{0}）。", splitVal[1]));

                targetFeatures.Add(new FeatureEntity()
                {
                    FeaturekeyName = splitVal[0],
                    FeatureNameJp = splitVal[1],
                    Enabled = tgt.Value
                });
            }

            return targetFeatures;
        }

        public static bool EnableFeature(string featureName, FXLogger logger)
        {
            System.Threading.Thread.Sleep(2000);
            logger.Write("[FeatureManager] [EnableFeature] {0}を有効化します。", featureName);
            using (Process pr = new Process())
            {
                pr.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe");
                pr.StartInfo.Arguments = string.Format("/c %windir%\\{0}\\dism.exe /online /enable-feature /featurename:{1} /all /norestart >>\"{2}\"",
                                    Environment.Is64BitOperatingSystem ? "sysnative" : "system32", featureName, logger.FilePath);
                pr.StartInfo.RedirectStandardOutput = true;
                pr.StartInfo.CreateNoWindow = true;
                pr.StartInfo.UseShellExecute = false;
                pr.StartInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.System);
                pr.Start();
                pr.WaitForExit();
                return pr.ExitCode == 0;
            }
        }

        public static bool NET35Installed()
        {
            using (RegistryKey net35Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5"))
            {
                //.NET Framework 3.5がインターネットされている場合はnet35Keyがnullにならない
                return net35Key != null;
            }
        }
    }
}
