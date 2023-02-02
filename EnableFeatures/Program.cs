using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnableFeatures
{
    static class Program
    {
        //第一引数が「/s」の場合はサイレントモード
        static bool modeSilent = (Environment.GetCommandLineArgs().Length > 1) && (Environment.GetCommandLineArgs()[1].ToLower() == "/s");

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //%temp%\EnableFeatures_yyyyMMdd_HHmm.logにログを出力する。
            string fileName = string.Format("EnableFeatures_{0}.log", DateTime.Now.ToString("yyyyMMdd_HHmm", System.Globalization.DateTimeFormatInfo.InvariantInfo));
            string logFile = System.IO.Path.Combine(System.IO.Path.GetTempPath(), fileName);
            FXLogger logger = new FXLogger(logFile);

            logger.Write("機能の有効化ツールを開始します。");
            //2重起動の防止
            try
            {
                System.Threading.Mutex mu = new System.Threading.Mutex(false, @"Global\EnableFeatures");
                if (mu.WaitOne(0, false) == false) throw new Exception();
            }
            catch
            {
                ShowMessage("既に起動しているため終了します。", "エラー", MessageBoxIcon.Error);
                logger.WriteError("既に起動しているため終了します。");
                Environment.ExitCode = (int)ErrorCode.ERROR_MUTEX;
                logger.Write("機能の有効化ツールを終了します（{0}）。", Environment.ExitCode);
                return;
            }

            //機能の有効化をする上で.NET Framework3.5がインストールされている必要があるので無ければエラーにする。
            if (!FeatureManager.NET35Installed())
            {
                ShowMessage(".NET Framework3.5をインストールしてください。", "エラー", MessageBoxIcon.Error);
                logger.WriteError(".NET Framework3.5をインストールしてください。");
                Environment.ExitCode = (int)ErrorCode.ERROR_NO_NET35;
                logger.Write("機能の有効化ツールを終了します（{0}）。", Environment.ExitCode);
                return;
            }

            Application.Run(new MainForm(logger));
            logger.Write("機能の有効化ツールを終了します（{0}）。", Environment.ExitCode);
        }

        private static void ShowMessage(string msg, string caption, MessageBoxIcon icon)
        {
            if (!modeSilent) MessageBox.Show(msg, caption, MessageBoxButtons.OK, icon);
        }
    }
}
