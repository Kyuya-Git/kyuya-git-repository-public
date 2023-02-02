using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DirChecker
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0) //引数がない場合は設定画面を表示
            {
                //管理者として実行していないとレジストリ操作でエラーになるため起動時に警告する
                if (!IsAdministrator())
                {
                    DialogResult dr = MessageBox.Show("管理者として実行していません。\r\n設定時にエラーとなる可能性がありますが続けますか？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dr == DialogResult.No)
                    {
                        Environment.Exit(1);
                    }
                }
                Application.Run(new MainForm());
            }
            else //引数がある場合はメイン処理
            {
                try
                {
                    //設定ファイルを読み込む
                    FileStreamer fs = new IniFileStreamer();
                    var list = fs.Read();

                    //読み込んだ設定を基に環境のチェックを実行する
                    list.ExecuteDirCheck();
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    TextLogger Logger = new TextLogger();
                    Logger.WriteLog(ex.ToString());
                    Environment.Exit(-1);
                }
            }
        }

        private static bool IsAdministrator()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
    }
}
