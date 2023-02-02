using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NXGenponChecker
{
    public partial class MainForm : Form
    {
        List<RecordEntity> updateList;
        string[] aryTarget = new string[2] { "AppSvr", "CtrlSvr" };

        public MainForm()
        {
            InitializeComponent();
        }
        System.Diagnostics.Stopwatch sw;
        private void ButtonExec_Click(object sender, EventArgs e)
        {
            string checkResult = ISTargetDirValid();

            if (checkResult != "")
            {
                MessageBox.Show(checkResult, "実行チェックエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewResult.Items.Clear();

            updateList = new List<RecordEntity>();
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;

            bgWorker.DoWork += (s, bge) =>
            {
                sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                DirectoryInfo di_update = new DirectoryInfo(TextBoxUpdate.Text);
                int Max = di_update.GetDirectories().Count();
                int now = 0;
                foreach (DirectoryInfo dir in di_update.GetDirectories())
                {
                    now++;
                    if (dir.GetFiles("*", SearchOption.AllDirectories).Count() < 1) continue;

                    RecordEntity rec = new RecordEntity();
                    rec.BaseFolderName = dir.Name;
                    rec.BaseFolderFullName = dir.FullName;
                    bgWorker.ReportProgress(0, string.Format("「{0}」を検証しています（{1}）。", rec.BaseFolderName, string.Format("{0}/{1}", now, Max)));

                    //AppSvrフォルダーとCtrlSvrフォルダーをチェックする
                    foreach (string svrName in aryTarget)
                    {
                        string targetDir = Path.Combine(rec.BaseFolderFullName, svrName);
                        if (!Directory.Exists(targetDir))
                            continue;

                        string[] files = Directory.GetFiles(targetDir, "*", SearchOption.AllDirectories);

                        if (files.Count() == 0)
                            continue;

                        foreach (string file in files)
                        {
                            string genponFile = TextBoxGenpon.Text + file.Replace(rec.BaseFolderFullName, "");
                            bool reflected = false;
                            if (File.Exists(genponFile))
                            {
                                reflected = file.Compare(genponFile);
                            }

                            if (svrName == "AppSvr")
                                rec.AppSvr.Add(new TargetFileInfo() { Path = file.Replace(rec.BaseFolderFullName, ""), Reflected = reflected });
                            else
                                rec.CtrlSvr.Add(new TargetFileInfo() { Path = file.Replace(rec.BaseFolderFullName, ""), Reflected = reflected });
                        }
                    }
                    updateList.Add(rec);
                    ListViewItem item = new ListViewItem(rec.BaseFolderName);

                    bool IsOK = !(rec.AppSvr.Exists(t => !t.Reflected)) && !(rec.CtrlSvr.Exists(t => !t.Reflected));
                    item.SubItems.Add(IsOK ? "○" : "×");

                    //原本に反映されていないファイルがあると判定された場合は背景色をピンク色にする
                    if (item.SubItems[1].Text == "×")
                        item.BackColor = Color.Pink;

                    Invoke((MethodInvoker)(() =>
                    {
                        ListViewResult.Items.Add(item);
                    }));
                }
            };

            bgWorker.RunWorkerCompleted += (s, rwce) =>
            {
                sw.Stop();
                if (rwce.Error != null)
                {
                    MessageBox.Show("★" + rwce.Error.Message);
                }
                else
                {
                    TimeSpan ts = sw.Elapsed;
                    MessageBox.Show(string.Format("{0:00}分{1:00}秒{2:000}ミリ秒", ts.Minutes, ts.Seconds, ts.Milliseconds), "実行結果");
                }
                ButtonExec.Enabled = true;
                LabelProgress.Text = "...";
            };

            bgWorker.ProgressChanged += (s, pce) =>
            {
                LabelProgress.Text = (string)pce.UserState;
            };

            ButtonExec.Enabled = false;
            bgWorker.RunWorkerAsync();
        }

        string ISTargetDirValid()
        {
            //原本は指定されたフォルダが存在すること、その直下にAppSvr、CtrlSvrが存在する場合にOKとする
            bool update = true, genpon = false;

            genpon = Directory.Exists(TextBoxGenpon.Text) &&
                     Directory.Exists(Path.Combine(TextBoxGenpon.Text, aryTarget[0])) &&
                     Directory.Exists(Path.Combine(TextBoxGenpon.Text, aryTarget[1]));

            DirectoryInfo di_update = new DirectoryInfo(TextBoxUpdate.Text);

            if (!genpon)
                return "原本フォルダーが見つからないか、AppSvrまたはCtrlSvrが存在しない可能性があります。";

            //受付はいづれかのフォルダがNGであればエラーとしたいのでupdateの論理積をセットする（いづれかのフォルダに問題があればNG）
            foreach (DirectoryInfo dir in di_update.GetDirectories())
            {
                //ファイルが１つもないフォルダはチェック対象外とする
                if (dir.GetFiles("*", SearchOption.AllDirectories).Count() < 1) continue;

                //受付なのでAPPSVR、CTRLSVRのいづれかがあればOKとする（両方が無ければNG）
                var eachFolder = dir.GetDirectories().ToList();
                update &= (eachFolder.Exists(f => f.Name.ToUpper() == aryTarget[0].ToUpper()) || eachFolder.Exists(f => f.Name.ToUpper() == aryTarget[1].ToUpper()));

                //問題があった場合はチェックを中止する
                if (!update)
                {
                    return string.Format("受付の\"{0}\"フォルダーにAppSvrまたはCtrlSvrが存在しない可能性があります。", dir.Name); ;
                }
            }

            return "";
        }

        private void ListViewResult_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ListViewResult.Items.Count != 0)
            {
                ListView lview = (ListView)sender;
                var record = updateList.FirstOrDefault(l => l.BaseFolderName.ToUpper() == lview.SelectedItems[0].Text.ToUpper());
                FileListsForm fl = new FileListsForm(record);
                fl.ShowDialog();
            }
        }
    }
}
