using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnableFeatures
{
    public partial class MainForm : Form
    {
        Bitmap[] bmp;
        List<FeatureEntity> targetFeatures;
        private FXLogger logger;
        readonly string formText = "機能の有効化ツール";
        static bool modeSilent = (Environment.GetCommandLineArgs().Length > 1) && (Environment.GetCommandLineArgs()[1].ToLower() == "/s");
        public MainForm(FXLogger _logger)
        {
            InitializeComponent();
            this.Text = formText;
            logger = _logger;
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            bmp = ResouceManager.GetImages();

            PictureBoxLogo.Image = bmp[(int)ImageID.LOGO_GDX];

            //.NETの不具合？で水平スクロールバーが表示されるため、垂直スクロールバーの幅の分セットされているコントロールを左にずらす
            int vertScrollWidth = SystemInformation.VerticalScrollBarWidth;
            TableLayoutPanelContainer.Padding = new Padding(0, 0, vertScrollWidth, 0);
            ButtonCancel.Enabled = false;

            #region 有効化対象の機能の取得
            Exception except = null;
            try
            {
                ProgressForm pf = new ProgressForm();
                pf.Worker.DoWork += (s, e) =>
                {
                    targetFeatures = FeatureManager.GetFeatures();
                };

                pf.Worker.RunWorkerCompleted += (s, e) =>
                {
                    if (e.Error != null)
                    {
                        except = e.Error;
                    }
                    pf.Close();
                };
                pf.ShowDialog();

                if (except != null)
                    throw except;

                if (!targetFeatures.Exists(t => t.Enabled == false))
                {
                    ShowMessage("すべての機能が有効になっているため終了します。", "情報", MessageBoxIcon.Information);
                    Environment.ExitCode = (int)ErrorCode.ERROR_SUCCESS;

                    //コンストラクタでthis.Close()を呼び出すとProgram.cs.Application.Run()で破棄された
                    //オブジェクトにアクセスしエラーになってしまうのでLoadイベントでClose()が実行されるようにする。
                    Load -= new System.EventHandler(this.MainForm_Load);
                    Load += (s, e) => this.Close();
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message, "エラー", MessageBoxIcon.Error);
                logger.WriteError(ex.StackTrace);
                Environment.ExitCode = (int)ErrorCode.ERROR_READINIERROR;
                Load -= new System.EventHandler(this.MainForm_Load);
                Load += (s, e) => this.Close();
            }
            #endregion
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < targetFeatures.Count + 1; i++)
            {
                //LabelFeatureName
                Control[] lbl = this.Controls.Find(string.Format("LabelFeatureName{0:00}", i), true);
                if (lbl.Length > 0)
                {
                    ((Label)lbl[0]).Text = targetFeatures[i - 1].FeatureNameJp;
                }
                else
                {
                    ShowMessage(string.Format("LabelFeatureName{0:00}が見つかりませんでした。", i), "エラー", MessageBoxIcon.Error);
                    Environment.ExitCode = (int)ErrorCode.ERROR_CONTROLINVALID;
                    this.Close();
                }

                //PictureBoxStatus
                Control[] pbs = this.Controls.Find(string.Format("PictureBoxStatus{0:00}", i), true);
                if (pbs.Length > 0)
                {
                    ((PictureBox)pbs[0]).Image = targetFeatures[i - 1].Enabled ? bmp[(int)ImageID.STATUS_OK] : bmp[(int)ImageID.STATUS_ATTENTION];
                }
                else
                {
                    ShowMessage(string.Format("PictureBoxStatus{0:00}が見つかりませんでした。", i), "エラー", MessageBoxIcon.Error);
                    Environment.ExitCode = (int)ErrorCode.ERROR_CONTROLINVALID;
                    this.Close();
                }
            }

            //サイレントモードの場合は実行を開始する
            if (modeSilent)
            {
                ButtonBegin_Click(null, null);
            }
        }

        BackgroundWorker bg;
        private void ButtonBegin_Click(object sender, EventArgs e)
        {
            ButtonCancel.Enabled = true;
            ButtonBegin.Enabled = false;

            bg = new BackgroundWorker();
            bg.WorkerSupportsCancellation = true;
            bg.DoWork += (s, dwe) =>
            {
                for (int i = 1; i < targetFeatures.Count + 1; i++)
                {
                    //キャンセルボタンがクリックされた場合は処理を中止する
                    if (bg.CancellationPending)
                    {
                        dwe.Cancel = true;
                        return;
                    }

                    //有効化済みの機能はスキップする
                    if (targetFeatures[i - 1].Enabled)
                    {
                        continue;
                    }

                    //対象のコントロール（行）にアクセスするためのインデックス作成する
                    string index = string.Format("{0:00}", i);

                    //対象のコントロールのオブジェクトを取得する
                    Control[] pb = this.Controls.Find("ProgressBarEnable" + index, true);
                    Control[] pbs = this.Controls.Find("PictureBoxStatus" + index, true);

                    //対象のコントロールのオブジェクトが見つからなかった場合はエラーとする
                    if (pb.Length <= 0 || pbs.Length <= 0)
                    {
                        throw new Exception(string.Format("コントロールの取得に失敗しました（{0}）。", index));
                    }

                    //処理対象のプログレスバーを実行中の描写にする。
                    //フォーカスも移動し常に処理中の機能が表示されるようにする。
                    Invoke((MethodInvoker)(() =>
                    {
                        ((ProgressBar)pb[0]).Focus();
                        ((ProgressBar)pb[0]).Style = ProgressBarStyle.Marquee;
                    }));

                    #region 機能の有効化処理↓

                    var target = targetFeatures[i - 1];
                    bool succeeded = false;
                    try
                    {
                        succeeded = FeatureManager.EnableFeature(target.FeaturekeyName, logger);
                        if (!succeeded)
                        {
                            throw new Exception(string.Format("{0}の有効化でエラーが発生しました。\r\n{1}を確認してください。", target.FeatureNameJp, logger.FilePath));
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Invoke((MethodInvoker)(() =>
                        {
                            //機能の有効化結果に応じてイメージを切り替える
                            ((PictureBox)pbs[0]).Image = succeeded ? bmp[(int)ImageID.STATUS_OK] : bmp[(int)ImageID.STATUS_ERROR];
                            ((ProgressBar)pb[0]).Style = ProgressBarStyle.Blocks;
                        }));
                    }
                    #endregion

                    //機能の有効化が完了したら1列目のイメージを有効化済みを表すイメージに変更する
                    //プログレスバーもデフォルトの描写に戻す
                    Invoke((MethodInvoker)(() =>
                    {
                        ((PictureBox)pbs[0]).Image = bmp[(int)ImageID.STATUS_OK];
                        ((ProgressBar)pb[0]).Style = ProgressBarStyle.Blocks;
                    }));
                }
            };

            bg.RunWorkerCompleted += (s, rwce) =>
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        ButtonCancel.Enabled = false;
                        this.Text = formText; //【キャンセル】ボタンがクリックされた場合はフォームのテキストを変更しているので初期値に戻す
                    }));
                    if (rwce.Error != null)
                    {
                        ShowMessage(rwce.Error.Message, "エラー", MessageBoxIcon.Error);
                        Environment.ExitCode = (int)ErrorCode.ERROR_ON_ENABLE;
                    }
                    else if (rwce.Cancelled)
                    {
                        ShowMessage("【キャンセル】ボタンがクリックされました。", "キャンセル", MessageBoxIcon.Warning);
                        Environment.ExitCode = (int)ErrorCode.ERROR_CANCEL;
                    }
                    else
                    {
                        ShowMessage("機能の有効化が正常に終了しました。", "終了", MessageBoxIcon.Information);
                        Environment.ExitCode = (int)ErrorCode.ERROR_SUCCESS;
                    }

                    if (modeSilent)
                    {
                        this.Close();
                    }
                };
            bg.RunWorkerAsync();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            bg.CancelAsync();
            ButtonCancel.Enabled = false;
            this.Text += " キャンセル待機中...";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bg != null && bg.IsBusy)
            {
                //サイレントモードの場合でもメッセージボックスを表示する（dism.exeが途中で止まってしまうことがあるため）
                DialogResult dr = MessageBox.Show("機能の有効化が実行中です。終了しますか？\r\n" +
                                                  "起動中の処理が残る可能性があるため、コンピューターを再起動して下さい。"
                                                  , "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != System.Windows.Forms.DialogResult.Yes)
                {
                    e.Cancel = true;
                }
                logger.WriteError("★【×】ボタンにより中断されました。");
                Environment.ExitCode = (int)ErrorCode.ERROR_CANCEL;
            }
        }

        private void ShowMessage(string msg, string caption, MessageBoxIcon icon)
        {
            if (!modeSilent) MessageBox.Show(msg, caption, MessageBoxButtons.OK, icon);

            //念のためにメッセージの内容はログにも出力しておく
            if (icon == MessageBoxIcon.Error) logger.WriteError(msg);
            else logger.Write(msg);
        }
    }
}
