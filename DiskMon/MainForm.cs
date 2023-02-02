using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DiskMon
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        bool executing = false;
        private void ButtonSwitch_Click(object sender, EventArgs e)
        {
            if (!executing)
            {
                BkgWorker.RunWorkerAsync();
            }
            else
            {
                BkgWorker.CancelAsync();
            }
            ChangeStatus();
        }

        void ChangeStatus()
        {
            executing = !executing;
            if (executing)
            {
                LabelStatus.Text = "実行中...";
                ButtonSwitch.Text = "停止";
                ButtonSwitch.BackColor = Color.LightCoral;
            }
            else
            {
                LabelStatus.Text = "停止中...";
                ButtonSwitch.Text = "実行";
                ButtonSwitch.BackColor = Color.LightGreen;
            }
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (BkgWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                string[] drives = Directory.GetLogicalDrives();
                foreach (string drChar in drives)
                {
                    System.IO.DriveInfo drive = new System.IO.DriveInfo(drChar);
                    if (drive.DriveType == DriveType.Fixed)
                    {
                        Write(drChar, string.Format("{0},{1}", string.Format("{0:#.###}", (float)drive.AvailableFreeSpace / 1024 / 1024 / 1024), string.Format("{0:#.###}", (float)drive.TotalSize / 1024 / 1024 / 1024)));
                    }
                }
                System.Threading.Thread.Sleep(2000);
            }
        }

        private void Write(string letter, string msg)
        {
            try
            {
                string fileName = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName, string.Format("{0}_DriveSize.csv", letter.Substring(0, 1)));
                using (StreamWriter sw = new StreamWriter(fileName, true, Encoding.GetEncoding("Shift_JIS")))
                {
                    sw.WriteLine(string.Format("{0},{1}", DateTime.Now, msg));
                }
            }
            catch { }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("停止しました。出力されたファイルを回収してください。");
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            BkgWorker.DoWork += DoWork;
            BkgWorker.RunWorkerCompleted += RunWorkerCompleted;
        }
    }
}
