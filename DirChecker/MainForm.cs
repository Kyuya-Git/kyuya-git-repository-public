using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Microsoft.Win32;

namespace DirChecker
{
    public partial class MainForm : Form
    {
        //フォルダチェック用のクラスのオブジェクトを格納するためのListをインスタンス化する
        List<CheckDirEntity> m_cdelist = new List<CheckDirEntity>();

        //デフォルトの確認対象
        string[] m_defaultlist = new string[] { "ごみ箱", "ダウンロード", "デスクトップ" };

        //フォームの表示あり
        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonImportSetting_Click(object sender, EventArgs e)
        {
            try
            {
                ReadConfig();

                //UIに設定内容を反映する
                CopyEntityToControl();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "設定ファイル読み込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ButtonImportSetting.BackColor = Color.Red; //読み込みに失敗した場合はボタンの色を赤色にする
                return;
            }
            ButtonImportSetting.BackColor = Color.LightGreen;
        }

        private void CopyEntityToControl()
        {
            m_cdelist.ForEach(cde =>
           {
               if (cde.name == "ごみ箱")
               {
                   CheckBoxRecycleBin.Checked = cde.CheckDir;
                   CheckBoxExportLog_RecycleBin.Checked = cde.ExportLog;
               }
               if (cde.name.ToLower() == "ダウンロード")
               {
                   CheckBoxDownloads.Checked = cde.CheckDir;
                   CheckBoxExportLog_Downloads.Checked = cde.ExportLog;
               }
               if (cde.name.ToLower() == "デスクトップ")
               {
                   CheckBoxCountDesktopFiles.Checked = cde.CheckDir;
                   CheckBoxDesktopMaxCountExportLog.Checked = cde.CheckDir;
                   TrackBarCountDesktopFiles.Value = cde.DesktopFilesMaxCount;
               }
           });
        }

        private void ButtonSet_Click(object sender, EventArgs e)
        {
            SetControlToEntity(); //UIで設定した内容をオブジェクトに代入する
            if (!m_cdelist.Exists(s => s.CheckDir))
            {
                DialogResult dr = ShowMessage("いずれにも「サインイン時に確認する」にチェックが入っていません。\r\n設定しますか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes)
                {
                    return;
                }
            }

            try
            {
                WriteConfig();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:\r\n{1}", ex.GetType(), ex.Message);
                ShowMessage(msg, "設定ファイル書き込みエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SetCurrentVersionRunHKLM();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:\r\n{1}", ex.GetType(), ex.Message);
                ShowMessage(msg, "レジストリ設定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShowMessage("設定しました。", "設定完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SetControlToEntity()
        {
            //cdeオブジェクトのnameプロパティにdefaultlistに設定した文字列を持つ要素が存在しなければ追加する
            foreach (string def in m_defaultlist)
            {
                if (!m_cdelist.Exists(s => s.name == def))
                {
                    m_cdelist.Add(new CheckDirEntity { name = def, directory = "" });
                }
            }

            //UIで設定した内容をcdeオブジェクトにセットする
            m_cdelist.ForEach(cde =>
            {
                if (cde.name == "ごみ箱")
                {
                    cde.CheckDir = CheckBoxRecycleBin.Checked;
                    cde.ExportLog = CheckBoxExportLog_RecycleBin.Checked;
                }
                if (cde.name.ToLower() == "ダウンロード")
                {
                    cde.directory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
                    cde.CheckDir = CheckBoxDownloads.Checked;
                    cde.ExportLog = CheckBoxExportLog_Downloads.Checked;
                }
                if (cde.name.ToLower() == "デスクトップ")
                {
                    cde.CheckDir = CheckBoxCountDesktopFiles.Checked;
                    cde.ExportLog = CheckBoxDesktopMaxCountExportLog.Checked;
                    cde.DesktopFilesMaxCount = int.Parse(LabelCountDesktopFiles.Text);
                }
            });
        }

        private void ToolStripMenuItemDeleteSetting_Click(object sender, EventArgs e)
        {
            DialogResult dr = ShowMessage("設定を削除しますか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr == DialogResult.No)
                return;
            try
            {
                DeleteCurrentVersionRunHKLM();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:\r\n{1}", ex.GetType(), ex.Message);
                ShowMessage(msg, "設定削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ShowMessage("設定を削除しました。", "設定削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReadConfig()
        {
            FileStreamer fs = new IniFileStreamer();
            m_cdelist = fs.Read();
        }

        private void WriteConfig()
        {
            FileStreamer fs = new IniFileStreamer();
            fs.Write(m_cdelist);
        }

        const string regkey = @"Software\Microsoft\Windows\CurrentVersion\Run";
        private void SetCurrentVersionRunHKLM()
        {
            //Runキーを開く
            using (RegistryKey subkey = Registry.LocalMachine.OpenSubKey(regkey, true))
            {
                //値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
                //適当な引数を与えないとフォームが表示されてしまうのでとりあえず「arg」を与える
                subkey.SetValue(Application.ProductName, Application.ExecutablePath + " arg");
            }
        }

        private void DeleteCurrentVersionRunHKLM()
        {
            using (RegistryKey subkey = Registry.LocalMachine.OpenSubKey(regkey, true))
            {
                subkey.DeleteValue(Application.ProductName);
            }
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            CheckBox ch = (CheckBox)sender;
            if (ch.Name == "CheckBoxRecycleBin")
                CheckBoxExportLog_RecycleBin.Enabled = ch.Checked;
            else if (ch.Name == "CheckBoxDownloads")
                CheckBoxExportLog_Downloads.Enabled = ch.Checked;
            else if (ch.Name == "CheckBoxCountDesktopFiles")
                TrackBarCountDesktopFiles.Enabled = ch.Checked;
        }

        private void ToolStripMenuItemOpenLog_Click(object sender, EventArgs e)
        {
            try
            {
                ILogger Logger = new TextLogger();
                Logger.OpenLog();
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}:\r\n{1}", ex.GetType(), ex.Message);
                ShowMessage(msg, "ログオープンエラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItemDeleteLog_Click(object sender, EventArgs e)
        {
            DialogResult dr = ShowMessage("ログを削除しますか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            try
            {
                ILogger Logger = new TextLogger();
                Logger.DeleteLog();
                ShowMessage("ログの削除が完了しました。", "ログの削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "ログ削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToolStripMenuItemExecute_Click(object sender, EventArgs e)
        {
            DialogResult dr = ShowMessage("実行しますか。", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dr != DialogResult.Yes)
            {
                return;
            }

            SetControlToEntity();
            m_cdelist.ExecuteDirCheck();
        }

        private void TrackBarCountDesktopFiles_ValueChanged(object sender, EventArgs e)
        {
            LabelCountDesktopFiles.Text = TrackBarCountDesktopFiles.Value.ToString();
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
