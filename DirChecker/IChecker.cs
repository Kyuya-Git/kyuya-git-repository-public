using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell32;
using System.Diagnostics;
using System.IO;

namespace DirChecker
{
    public enum CheckResult
    {
        NOTIFY_YES,
        NOTIFY_NO,
        NOTIFY_ERROR
    }

    public abstract class CheckerBase
    {
        internal string TargetName { get; set; }
        private ILogger Logger;
        public void SetLogger(string _targetName, ILogger _logger)
        {
            TargetName = _targetName;
            Logger = _logger;
        }

        #region DesktopChecker用のプロパティ
        internal int DesktopFilesMaxCount;
        public void SetDesktopFilesMaxCount(int _DesktopFilesMaxCount)
        {
            DesktopFilesMaxCount = _DesktopFilesMaxCount;
        }
        #endregion

        #region DirectoryChecker用のプロパティ
        internal string TargetDirectory;
        public void SetTargetDirectory(string _TargetDirectory)
        {
            TargetDirectory = _TargetDirectory;
        }
        #endregion

        internal virtual void LogWrite(string msg, params object[] args)
        {
            if (Logger != null)
            {
                Logger.WriteLog(DateTime.Now + " " + string.Format(msg, args));
            }
        }

        public virtual CheckResult ExecuteCheck()
        {
            LogWrite("●{0}", TargetName);
            return CheckResult.NOTIFY_NO;
        }

        public abstract void BaloonClickEvent(object sender, EventArgs e);
        public Exception exception;
    }

    public class RecycleBinChecker : CheckerBase
    {
        public override CheckResult ExecuteCheck()
        {
            try
            {
                base.ExecuteCheck();
                ShellClass shl = new ShellClass();
                Folder fol = shl.NameSpace(10);
                if (fol.Items().Count != 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (FolderItem FolderItem in fol.Items()) //すべてのファイルの情報を収集する
                    {
                        sb.AppendLine();
                        sb.AppendLine("名前:" + fol.GetDetailsOf(FolderItem, 0));
                        sb.AppendLine("元の場所:" + fol.GetDetailsOf(FolderItem, 1));
                        sb.AppendLine("削除した日:" + fol.GetDetailsOf(FolderItem, 2));
                        sb.AppendLine("サイズ:" + fol.GetDetailsOf(FolderItem, 3));
                        sb.AppendLine("種類:" + fol.GetDetailsOf(FolderItem, 4));
                        sb.AppendLine("更新日時:" + fol.GetDetailsOf(FolderItem, 5));
                        sb.AppendLine("作成日時:" + fol.GetDetailsOf(FolderItem, 6));
                    }
                    LogWrite(sb.ToString());
                    return CheckResult.NOTIFY_YES;
                }

                LogWrite("ごみ箱の中身は空です。\r\n");
                return CheckResult.NOTIFY_NO;
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
                exception = ex;
                return CheckResult.NOTIFY_ERROR;
            }
        }

        public override void BaloonClickEvent(object sender, EventArgs e)
        {
            Process.Start("explorer", "shell:RecycleBinFolder");
        }
    }

    public class DesktopChecker : CheckerBase
    {
        public override CheckResult ExecuteCheck()
        {
            try
            {
                base.ExecuteCheck();
                DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                FileInfo[] fis = di.GetFiles();
                DirectoryInfo[] dis = di.GetDirectories();
                StringBuilder sb = new StringBuilder();
                int countFiles = 0;
                StringBuilder filesInfosb = new StringBuilder();
                filesInfosb.AppendLine("[ファイル]");
                foreach (FileInfo fi in fis)
                {
                    if (fi.Name == "desktop.ini")
                        continue;
                    filesInfosb.AppendLine("・" + fi.Name);
                    countFiles++;
                }
                filesInfosb.AppendLine("[フォルダ]");
                foreach (DirectoryInfo _di in dis)
                {
                    //出力されたログを見やすくするファイルごとの行間を1行空ける
                    filesInfosb.AppendLine("・" + _di.Name);
                    countFiles++;
                }
                sb.AppendLine("カウント：" + countFiles);
                LogWrite(sb.ToString() + filesInfosb.ToString());
                if (countFiles >= DesktopFilesMaxCount)
                {
                    return CheckResult.NOTIFY_YES;
                }
                else
                {
                    return CheckResult.NOTIFY_NO;
                }
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
                exception = ex;
                return CheckResult.NOTIFY_ERROR;
            }
        }
        public override void BaloonClickEvent(object sender, EventArgs e)
        {
            Process.Start("file://" + Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
        }
    }
    public class DirectoryChecker : CheckerBase
    {
        public override CheckResult ExecuteCheck()
        {
            try
            {
                base.ExecuteCheck();
                DirectoryInfo di = new DirectoryInfo(TargetDirectory);
                FileInfo[] fis = di.GetFiles();
                if (fis.Count() > 0) //サブディレクトリはチェック対象外
                {
                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    foreach (FileInfo fi in fis)
                    {
                        if (fi.Name == "desktop.ini")
                        {
                            continue;
                        }

                        sb.AppendLine();
                        sb.AppendLine("パス：" + fi.FullName);

                        //エクスプローラーの表示と合わせるために最大の整数に丸める
                        sb.AppendLine("サイズ：" + string.Format("{0:#,0}", Math.Ceiling((decimal)fi.Length / 1024) + "KB"));
                        sb.AppendLine(string.Format("更新日時：{0} {1}", fi.LastWriteTime.ToShortDateString(), fi.LastWriteTime.ToShortTimeString()));
                        count++;
                    }

                    if (count > 0)
                    {
                        LogWrite("ファイルが存在します。\r\n" + sb.ToString());
                        return CheckResult.NOTIFY_YES;
                    }
                }
                LogWrite("ファイルは存在しません。\r\n");
                return CheckResult.NOTIFY_NO;
            }
            catch (Exception ex)
            {
                LogWrite(ex.ToString());
                exception = ex;
                return CheckResult.NOTIFY_ERROR;
            }
        }
        public override void BaloonClickEvent(object sender, EventArgs e)
        {
            Process.Start("file://" + TargetDirectory);
        }
    }
}
