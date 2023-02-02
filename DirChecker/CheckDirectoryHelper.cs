using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace DirChecker
{
    public static class CheckDirectoryHelper
    {
        public static void ExecuteDirCheck(this List<CheckDirEntity> cdelist)
        {
            TextLogger txtLogger = new TextLogger();
            foreach (CheckDirEntity cde in cdelist)
            {
                //チェック対象外であればスキップする
                if (!cde.CheckDir)
                {
                    if (cde.ExportLog) //チェック対象外でもログの出力設定をしている場合はスキップしたことをログに出力
                    {
                        txtLogger.WriteLog(string.Format("チェック対象外なのでスキップします：{0}", cde.name));
                    }
                    continue;
                }

                CheckerBase checker;
                CheckResult checkResult = CheckResult.NOTIFY_NO;

                switch (cde.name)
                {
                    case "ごみ箱":
                        checker = new RecycleBinChecker();
                        break;
                    case "デスクトップ":
                        checker = new DesktopChecker();
                        checker.SetDesktopFilesMaxCount(cde.DesktopFilesMaxCount);
                        break;
                    default: //上記以外（フォルダー系）は共通メソッドを呼び出す
                        checker = new DirectoryChecker();
                        checker.SetTargetDirectory(cde.directory);
                        break;
                }

                //ログを出力する場合はILoggerをセットする
                if (cde.ExportLog)
                {
                    checker.SetLogger(cde.name, txtLogger);
                }

                checkResult = checker.ExecuteCheck();

                //チェックを実行した結果フラグが立っていなければループを継続する
                if (checkResult == CheckResult.NOTIFY_NO)
                {
                    continue;
                }

                string baloonTxt = "■" + cde.name + Environment.NewLine;
                if (checkResult == CheckResult.NOTIFY_ERROR)
                {
                    baloonTxt += checker.exception != null ? checker.exception.Message : "エラーが発生しました。";
                }
                else
                {
                    baloonTxt += "ファイルが存在します。\r\nクリックするとフォルダを開きます。";
                }

                Icon icon = (Icon)new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetObject("notifyIcon1.Icon");
                NotifyIcon notifyIcon = new NotifyIcon();
                notifyIcon.BalloonTipTitle = "おしらせ";
                notifyIcon.BalloonTipText = baloonTxt;
                notifyIcon.Icon = icon;
                notifyIcon.Visible = true;
                notifyIcon.BalloonTipClicked += checker.BaloonClickEvent;
                notifyIcon.ShowBalloonTip(3000);
            }
        }
    }
}
