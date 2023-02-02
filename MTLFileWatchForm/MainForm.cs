using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLFileWatchForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            BkgWorker.DoWork += DoWork;
            BkgWorker.RunWorkerCompleted += RunWorkerCompleted;
        }

        bool executing = false;
        private static System.IO.FileSystemWatcher watcher = null;

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            if (!executing)
            {
                ChangeStatus();
                BkgWorker.RunWorkerAsync();
            }
            else
            {
                BkgWorker.CancelAsync();
            }
        }

        private void ButtonClear_Click(object sender, EventArgs e)
        {
            ListBoxResult.Items.Clear();
        }

        void ChangeStatus()
        {
            executing = !executing;
            if (executing)
            {
                ButtonSwitch.Text = "停止";
                ButtonSwitch.BackColor = Color.LightCoral;
                LabelFilter.Enabled = false;
                TextBoxFilter.Enabled = false;
            }
            else
            {
                ButtonSwitch.Text = "開始";
                ButtonSwitch.BackColor = Color.LightGreen;
                LabelFilter.Enabled = true;
                TextBoxFilter.Enabled = true;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddListBoxItem("★" + e.Error.Message);
            }
            AddListBoxItem("監視を終了しました。");
            executing = !executing;
            Invoke((MethodInvoker)(() =>
            {
                ButtonSwitch.Text = "開始";
                ButtonSwitch.BackColor = Color.LightGreen;
                LabelFilter.Enabled = true;
                TextBoxFilter.Enabled = true;
            }));
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            AddListBoxItem("監視を開始します。");
            watcher = new System.IO.FileSystemWatcher();
            //監視するディレクトリを指定
            if (!System.IO.Directory.Exists(TextBoxTargetFolder.Text))
                throw new Exception("指定されたフォルダーは存在しません。");
            watcher.Path = TextBoxTargetFolder.Text;
            //最終アクセス日時、最終更新日時、ファイル、フォルダ名の変更を監視する
            watcher.NotifyFilter =
                (System.IO.NotifyFilters.LastAccess
                | System.IO.NotifyFilters.LastWrite
                | System.IO.NotifyFilters.FileName
                | System.IO.NotifyFilters.DirectoryName);
            //すべてのファイルを監視
            watcher.Filter = "";
            //UIのスレッドにマーシャリングする

            //イベントハンドラの追加
            watcher.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);
            watcher.Created += new System.IO.FileSystemEventHandler(watcher_Changed);
            watcher.Deleted += new System.IO.FileSystemEventHandler(watcher_Changed);
            watcher.Renamed += new System.IO.RenamedEventHandler(watcher_Renamed);

            //監視を開始する
            watcher.EnableRaisingEvents = true;
            while (!BkgWorker.CancellationPending)
            {

            }
            watcher.EnableRaisingEvents = false;
            watcher.Dispose();
            watcher = null;
            e.Cancel = true;
            return;

        }

        private void watcher_Changed(System.Object source, System.IO.FileSystemEventArgs e)
        {
            string msg = "";
            switch (e.ChangeType)
            {
                case System.IO.WatcherChangeTypes.Changed:
                    msg = "ファイル 「" + e.FullPath + "」が変更されました。";
                    break;
                case System.IO.WatcherChangeTypes.Created:
                    msg = "ファイル 「" + e.FullPath + "」が作成されました。";
                    break;
                case System.IO.WatcherChangeTypes.Deleted:
                    msg = "ファイル 「" + e.FullPath + "」が削除されました。";
                    break;
            }
            AddListBoxItem(msg);
        }

        private void watcher_Renamed(System.Object source, System.IO.RenamedEventArgs e)
        {
            AddListBoxItem("ファイル 「" + e.FullPath + "」の名前が変更されました。");
        }

        private void AddListBoxItem(string msg)
        {
            Invoke((MethodInvoker)(() =>
            {
                ListBoxResult.Items.Add(string.Format("{0} {1}", DateTime.Now, msg));
            }));
        }
    }
}
