using System;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text;

namespace CmnMstGenerator
{
    public partial class MainForm : Form
    {
        string[] targetFiles;
        const string FILENAME_S00 = "0000-共通マスタ.S00";
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                //TargetList.xmlから対象ファイルを取得する
                ReadXmlFile();
            }
            catch (Exception ex)
            {
                ShowMessage("TargetList.xmlの読み込みでエラーが発生しました。\r\n" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            targetFiles.ToList().ForEach(t =>
            {
                ListViewTargetFiles.Items.Add(t);
            });

            //何故か水平スクロールバーが表示されてしまうので垂直スクロールバー分だけカラム幅を縮める
            ListViewTargetFiles.Columns[0].Width = ListViewTargetFiles.Width - 4 - SystemInformation.VerticalScrollBarWidth;
        }

        private void ButtonExecute_Click(object sender, EventArgs e)
        {
            ButtonExecute.Enabled = false;
            #region 入力されたパスに誤りなどがないかを事前にチェック
            string msg = InputedPathCheck();
            if (msg != "")
            {
                ShowMessage(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ButtonExecute.Enabled = true;
                return;
            }
            msg = CheckTargetFilesExist();
            if (msg != "")
            {
                ShowMessage(msg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ButtonExecute.Enabled = true;
                return;
            }

            if (File.Exists(Path.Combine(TextBoxExportFolder.Text, FILENAME_S00)))
            {
                string confirmMsg = string.Format("指定されたフォルダーには既に{0}が存在します。\r\n既存のファイルは削除されますがよろしいですか。", FILENAME_S00);
                DialogResult dr = ShowMessage(confirmMsg, "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes)
                {
                    ButtonExecute.Enabled = true;
                    return;
                }
            }
            #endregion

            //ProgressForm.ShowDialog()でExcelファイルから必要な情報の収集を開始する。
            using (ProgressForm pf = new ProgressForm(TextBoxImportFolder.Text, targetFiles, TextBoxExportFolder.Text))
            {
                DialogResult result = pf.ShowDialog(this);
                if (result == DialogResult.Cancel)
                {
                    ShowMessage("キャンセルされました。", "キャンセル", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (result == DialogResult.Abort)
                {
                    //エラー情報を取得する
                    Exception ex = pf.Error;
                    CurrentPosition cp = pf.cp;
                    string errorMsg = string.Format("ファイルの読み込みでエラーが発生しました：{0}\r\nファイル：{1}\r\nシート：{2}\r\n行：{3}\r\n列：{4}",
                        ex.Message, //{0}
                        cp.FileName, //{1}
                        cp.SheetName, //{2}
                        cp.RowIndex + 1, //{3} プログラムのカウントは0からなのでプラス１する
                        cp.ColumnIndex + 1); //{4} プログラムのカウントは0からなのでプラス１する
                    ShowMessage(errorMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (result == DialogResult.OK)
                {
                    //Excelファイルから読み込んだ情報を取得する
                    List<TableInfo> tiList = (List<TableInfo>)pf.Result;
                    try
                    {
                        //読み込んだ情報を基にS00ファイルに書き出す。
                        S00Writer.WriteS00(TextBoxExportFolder.Text, tiList);
                    }
                    catch (Exception ex)
                    {
                        ShowMessage(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    ShowMessage("処理が終了しました。", "終了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            ButtonExecute.Enabled = true;
        }

        private string InputedPathCheck()
        {
            string msg = "";
            if (!Directory.Exists(TextBoxImportFolder.Text))
                msg = "・取り込み先のフォルダが存在しません。\r\n";
            if (!Directory.Exists(TextBoxExportFolder.Text))
                msg += "・出力先のフォルダが存在しません。";
            return msg.Trim('\r', '\n');
        }
        private string CheckTargetFilesExist()
        {
            string files = "";
            targetFiles.ToList().ForEach(f =>
            {
                if (!File.Exists(Path.Combine(TextBoxImportFolder.Text, f)))
                    files += string.Format("・{0}が存在しません。\r\n", f);
            });
            return files.Trim('\r', '\n');
        }

        private void ButtonFolderBrowserDialog_Clicks(object sender, EventArgs e)
        {
            var button = (Button)sender;
            bool IsImport = button.Name == "ButtonImport";
            FolderBrowserDialog fb = new FolderBrowserDialog();

            if (IsImport)
                fb.Description = "取り込むファイルが格納されているフォルダを選択してください。";
            else
                fb.Description = "出力先のフォルダを選択してください。";

            if (fb.ShowDialog() != DialogResult.OK)
                return;

            if (IsImport)
                TextBoxImportFolder.Text = fb.SelectedPath;
            else
                TextBoxExportFolder.Text = fb.SelectedPath;
        }

        private void ReadXmlFile()
        {
            targetFiles = new string[] { };
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(@"TargetFilesList.xml");
            var files = xmlDoc.SelectNodes("TargetFiles/File");

            for (int i = 0; i < files.Count; i++)
            {
                Array.Resize(ref targetFiles, i + 1);
                targetFiles[i] = files[i].Attributes["value"].InnerText;
            }
        }

        private void UpdateStoredFiles(object sender, EventArgs e)
        {
            ListViewStoredFiles.Items.Clear();
            string path = TextBoxImportFolder.Text;
            if (!Directory.Exists(path))
            {
                ListViewPaintControl();
                return;
            }

            DirectoryInfo di = new DirectoryInfo(path);
            List<FileInfo> fiList = di.GetFiles().ToList();
            if (fiList.Count() == 0)
            {
                ListViewPaintControl();
                return;
            }

            string[] storedFiles = new string[fiList.Count];
            int i = 0;
            fiList.ForEach((s) =>
            {
                storedFiles[i] = s.Name;
                i++;
            });

            //対象のファイル、対象外のファイルに分ける
            var matchFiles = targetFiles.ToList().FindAll(storedFiles.ToList().Contains);
            var otherFiles = storedFiles.ToList().Except(targetFiles.ToList());

            //左と右のリストビューの対象ファイルの位置が同じになるよう対象ファイル→対象外ファイルの順でitemsに追加する
            matchFiles.ToList().ForEach(mf => ListViewStoredFiles.Items.Add(mf));
            otherFiles.ToList().ForEach(of => ListViewStoredFiles.Items.Add(of));

            ListViewPaintControl();

            ListViewStoredFiles.Columns[0].Width = ListViewStoredFiles.Width - 4 - SystemInformation.VerticalScrollBarWidth;
        }

        private void ListViewPaintControl()
        {
            //対象ファイルが見つかった場合は緑に、存在しない場合は赤に背景色を変更する
            foreach (ListViewItem tgtitem in ListViewTargetFiles.Items)
            {
                //もとから緑の場合に最後のif文の意味が無くなるので背景色の情報は一旦初期化する
                tgtitem.BackColor = System.Drawing.Color.Empty;

                //指定のフォルダーに対象のファイルがある場合は該当ファイルの背景色を緑にする
                foreach (ListViewItem strditem in ListViewStoredFiles.Items)
                {
                    if (tgtitem.Text == strditem.Text)
                    {
                        tgtitem.BackColor = System.Drawing.Color.LightGreen;
                        strditem.BackColor = System.Drawing.Color.LightGreen;
                    }
                }

                //見つからなかった場合（上記のロジックで緑にできなかった場合）背景色を赤にする
                if (tgtitem.BackColor != System.Drawing.Color.LightGreen)
                {
                    tgtitem.BackColor = System.Drawing.Color.LightCoral;
                }
            }
        }

        #region メッセージボックスを中央配置するためのメソッド
        DialogResult ShowMessage(string msg, string caption, MessageBoxButtons button, MessageBoxIcon icon)
        {
            SetHook();
            return MessageBox.Show(msg, caption, button, icon);
        }

        private String PadRightBytes(string source, int length)
        {
            if (string.IsNullOrEmpty(source)) source = string.Empty;
            return source + string.Empty.PadRight(length - Encoding.GetEncoding("Shift_JIS").GetByteCount(source));
        }

        private IntPtr HHook;
        private void SetHook()
        {
            // フック設定
            IntPtr hInstance = WindowsAPI.GetWindowLong(this.Handle, (int)WindowsAPI.WindowLongParam.GWLP_HINSTANCE);
            IntPtr threadId = WindowsAPI.GetCurrentThreadId();
            HHook = WindowsAPI.SetWindowsHookEx((int)WindowsAPI.HookType.WH_CBT, new WindowsAPI.HOOKPROC(CBTProc), hInstance, threadId);
        }

        private IntPtr CBTProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode == (int)WindowsAPI.HCBT.HCBT_ACTIVATE)
            {
                WindowsAPI.RECT rectOwner;
                WindowsAPI.RECT rectMsgBox;
                int x, y;

                // オーナーウィンドウの位置と大きさを取得
                WindowsAPI.GetWindowRect(this.Handle, out rectOwner);
                // MessageBoxの位置と大きさを取得
                WindowsAPI.GetWindowRect(wParam, out rectMsgBox);

                // MessageBoxの表示位置を計算
                x = rectOwner.Left + (rectOwner.Width - rectMsgBox.Width) / 2;
                y = rectOwner.Top + (rectOwner.Height - rectMsgBox.Height) / 2;

                //MessageBoxの位置を設定
                WindowsAPI.SetWindowPos(wParam, 0, x, y, 0, 0,
                  (uint)(WindowsAPI.SetWindowPosFlags.SWP_NOSIZE | WindowsAPI.SetWindowPosFlags.SWP_NOZORDER | WindowsAPI.SetWindowPosFlags.SWP_NOACTIVATE));

                // フック解除
                WindowsAPI.UnhookWindowsHookEx(HHook);
            }
            // 次のプロシージャへのポインタ
            return WindowsAPI.CallNextHookEx(HHook, nCode, wParam, lParam);
        }
        #endregion
    }
}
