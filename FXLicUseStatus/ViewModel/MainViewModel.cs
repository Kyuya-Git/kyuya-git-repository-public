using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using FXLicUseStatus.Command;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Data;
using SystemCheckerPlugIn;
using System.Reflection;
using System.IO;

namespace FXLicUseStatus.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region --------- プロパティ、メンバー変数 ---------
        private ObservableCollection<string> _ComboItem = new ObservableCollection<string>();
        public ObservableCollection<string> ComboItem
        {
            get { return _ComboItem; }
            private set
            {
                _ComboItem = value;
                NotifyPropertyChanged("ComboItem");
            }
        }

        private int _ComboSelectedIndex;
        public int ComboSelectedIndex
        {
            get { return _ComboSelectedIndex; }
            private set
            {
                _ComboSelectedIndex = value;
                NotifyPropertyChanged("ComboSelectedIndex");
            }
        }

        private bool _CheckBoxChecked;
        public bool CheckBoxChecked
        {
            get { return _CheckBoxChecked; }
            private set
            {
                _CheckBoxChecked = value;
                NotifyPropertyChanged("CheckBoxChecked");
            }
        }

        public bool ControlEnabled
        {
            get { return ComboItem.Count > 0; }
        }

        private string _OutPutPath;
        public string OutPutPath
        {
            get { return _OutPutPath; }
            set { _OutPutPath = value; }
        }

        FXEnv[] Envs = new FXEnv[] 
        {
            new FXEnv(){DispName = "hoge DX", Envkey = "fugaFX4", SAVer = "17"},
            new FXEnv(){DispName = "hoge NX-Plus", Envkey = "fugaFX3", SAVer = "12"},
            new FXEnv(){DispName = "hoge DX ワークフロー", Envkey = "fugaWF4", SAVer = "17"},
            new FXEnv(){DispName = "hoge NX-Plus ワークフロー", Envkey = "fugaWF3", SAVer = "12"}
        };

        IntPtr handle;

        private readonly string LogFile = "FXLicUseStatus.log";
        #endregion

        #region --------- コマンド ---------
        // Execute()
        public ICommand ExecuteCommand
        {
            get;
            private set;
        }

        #endregion

        #region --------- メソッド ---------
        // ExecuteCommand
        private void Execute()
        {
            try
            {
                if (!Directory.Exists(OutPutPath))
                {
                    throw new DirectoryNotFoundException(OutPutPath + "フォルダーが見つかりません。");
                }
                FXEnv selecteditem = Envs.FirstOrDefault(env => env.Index == ComboSelectedIndex);
                Assembly asm = Assembly.Load("hogeServerInfo");
                Type myType = asm.GetType("hogeServerInfo.LicUseStatus");
                IFXSysChecker fx;
                fx = (IFXSysChecker)myType.InvokeMember("", BindingFlags.CreateInstance, null, null, null);
                fx.EnvVal[0] = selecteditem.Envkey;
                fx.EnvVal[1] = selecteditem.SAVer;
                fx.EnvVal[2] = selecteditem.DispName;
                CheckResult cr = fx.CheckExec();
                DataSet ds = fx.CheckLog;
                WriteToLogFile(ds);
                ShowMessage(LogFile + "に出力しました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            GC.Collect();
        }

        private bool SetComboItem()
        {
            bool ret = false;
            int Count = 0;
            foreach (FXEnv env in Envs)
            {
                if (RegKeyExists(env.Regkey))
                {
                    ComboItem.Add(env.DispName);
                    env.Index = Count++;
                    ret |= true;
                }
            }
            return ret;
        }

        private bool RegKeyExists(string key)
        {
            using (RegistryKey reg = Registry.LocalMachine.OpenSubKey(key))
            {
                return reg != null;
            }
        }

        private void WriteToLogFile(DataSet ds)
        {
            try
            {
                Encoding enc_sjis = Encoding.GetEncoding("Shift_JIS");
                StringBuilder sb_outtxt = new StringBuilder();

                sb_outtxt.AppendLine();
                sb_outtxt.AppendLine(string.Format("実行日時：{0}", DateTime.Now));
                foreach (DataTable dt in ds.Tables)
                {
                    if (dt.Columns.Count != 1)
                    {
                        int[] colmnwidth = new int[dt.Columns.Count];

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            colmnwidth[j] = enc_sjis.GetByteCount(dt.Columns[j].ColumnName);
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            for (int i = 0; i < dt.Columns.Count; i++)
                            {
                                string[] ss = row[i].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                foreach (string s in ss)
                                {
                                    if (colmnwidth[i] < enc_sjis.GetByteCount(s))
                                        colmnwidth[i] = enc_sjis.GetByteCount(s);
                                }
                            }
                        }

                        string colString = "\t|";
                        string upline = "\t-";
                        string line = "\t|";
                        bool bcol = false;
                        for (int i = 0; i < colmnwidth.Length; i++)
                        {
                            for (int j = 0; j < colmnwidth[i] + 1; j++)
                                upline += "-";

                            string colname = string.Empty;
                            if (dt.Columns[i].ColumnName != "Column" + (i + 1))
                            {
                                colname = dt.Columns[i].ColumnName;
                            }
                            colString += PadRightBytes(colname, colmnwidth[i]) + "|";

                            for (int j = 0; j < colmnwidth[i]; j++)
                                line += "-";
                            line += "|";

                        }
                        for (int i = 0; i < colmnwidth.Length; i++)
                        {
                            if (dt.Columns[i].ColumnName != "Column" + (i + 1))
                            {
                                bcol = true;
                                break;
                            }
                        }
                        sb_outtxt.AppendLine(upline);
                        if (bcol)
                        {
                            sb_outtxt.AppendLine(colString);
                            sb_outtxt.AppendLine(line);
                        }
                        enc_sjis.GetByteCount(colString);

                        foreach (DataRow row in dt.Rows)
                        {
                            int maxline = 0;
                            for (int i = 0; i < colmnwidth.Length; i++)
                            {
                                string[] ss = row[i].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                if (maxline < ss.Length)
                                    maxline = ss.Length;
                            }

                            string[,] list = new string[maxline, dt.Columns.Count];
                            for (int i = 0; i < colmnwidth.Length; i++)
                            {
                                string[] ss = row[i].ToString().Split(new string[] { "\r\n" }, StringSplitOptions.None);
                                for (int j = 0; j < ss.Length; j++)
                                {
                                    list[j, i] = ss[j];
                                }
                            }

                            for (int i = 0; i < maxline; i++)
                            {
                                string output = "\t|";
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    output += PadRightBytes(list[i, j], colmnwidth[j]) + "|";
                                }
                                sb_outtxt.AppendLine(output);
                            }
                        }
                        sb_outtxt.AppendLine(upline);
                    }
                    else
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            string detail = row[0].ToString().Replace("\r\n", "\r\n\t");
                            sb_outtxt.AppendLine("\t" + detail);
                        }
                    }
                }
                using (StreamWriter sw = new StreamWriter(Path.Combine(OutPutPath, LogFile), !CheckBoxChecked, enc_sjis))
                {
                    sw.WriteLine(sb_outtxt.ToString());
                }
            }
            catch (Exception ex)
            {
                string err = "ログファイルの書き込みに失敗しました。\r\n" + ex.Message + Environment.NewLine;
                err += "ログファイルの排他状況等を確認してもう一度実行してください。";
                throw new Exception(err);
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
            IntPtr hInstance = WindowsAPI.GetWindowLong(handle, (int)WindowsAPI.WindowLongParam.GWLP_HINSTANCE);
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
                WindowsAPI.GetWindowRect(handle, out rectOwner);
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

        #endregion

        #region --------- コンストラクタ ---------
        public MainViewModel(IntPtr _handle)
        {
            handle = _handle;
            ExecuteCommand = new RelayCommand(Execute);
            if (SetComboItem())
            {
                ComboSelectedIndex = 0;
            }
            else
            {
                string ReqAPSV = "";
                foreach (FXEnv env in Envs)
                {
                    ReqAPSV += string.Format("・{0} fugaサーバー\n", env.DispName);
                }
                MessageBox.Show("以下のいづれかがインストールされている必要があります。\r\n" + ReqAPSV, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //初期値をセット
            OutPutPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CheckBoxChecked = true;
        }
        #endregion

        #region INotifyPropertyChanged メンバー
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
    class FXEnv
    {
        public string DispName = "";
        public string Envkey = "";
        public string SAVer = "";
        public int Index = 999;
        public string Regkey
        {
            get
            {
                return string.Format(@"SOFTWARE\hoge\{0}\huga", Envkey);
            }
        }
    }
}
