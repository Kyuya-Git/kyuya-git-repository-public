namespace DirChecker
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ButtonSet = new System.Windows.Forms.Button();
            this.GroupBoxRecycleBin = new System.Windows.Forms.GroupBox();
            this.CheckBoxExportLog_RecycleBin = new System.Windows.Forms.CheckBox();
            this.CheckBoxRecycleBin = new System.Windows.Forms.CheckBox();
            this.GroupBoxDownloads = new System.Windows.Forms.GroupBox();
            this.CheckBoxExportLog_Downloads = new System.Windows.Forms.CheckBox();
            this.CheckBoxDownloads = new System.Windows.Forms.CheckBox();
            this.ButtonImportSetting = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemDeleteSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.ログを開くToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpenLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteLog = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExecute = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.CheckBoxCountDesktopFiles = new System.Windows.Forms.CheckBox();
            this.TrackBarCountDesktopFiles = new System.Windows.Forms.TrackBar();
            this.LabelCountDesktopFiles = new System.Windows.Forms.Label();
            this.CheckBoxDesktopMaxCountExportLog = new System.Windows.Forms.CheckBox();
            this.GroupBoxRecycleBin.SuspendLayout();
            this.GroupBoxDownloads.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarCountDesktopFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonSet
            // 
            this.ButtonSet.Location = new System.Drawing.Point(314, 192);
            this.ButtonSet.Name = "ButtonSet";
            this.ButtonSet.Size = new System.Drawing.Size(75, 23);
            this.ButtonSet.TabIndex = 1;
            this.ButtonSet.Text = "設定";
            this.ButtonSet.UseVisualStyleBackColor = true;
            this.ButtonSet.Click += new System.EventHandler(this.ButtonSet_Click);
            // 
            // GroupBoxRecycleBin
            // 
            this.GroupBoxRecycleBin.Controls.Add(this.CheckBoxExportLog_RecycleBin);
            this.GroupBoxRecycleBin.Controls.Add(this.CheckBoxRecycleBin);
            this.GroupBoxRecycleBin.Location = new System.Drawing.Point(15, 35);
            this.GroupBoxRecycleBin.Name = "GroupBoxRecycleBin";
            this.GroupBoxRecycleBin.Size = new System.Drawing.Size(174, 100);
            this.GroupBoxRecycleBin.TabIndex = 2;
            this.GroupBoxRecycleBin.TabStop = false;
            this.GroupBoxRecycleBin.Text = "ごみ箱";
            // 
            // CheckBoxExportLog_RecycleBin
            // 
            this.CheckBoxExportLog_RecycleBin.AutoSize = true;
            this.CheckBoxExportLog_RecycleBin.Enabled = false;
            this.CheckBoxExportLog_RecycleBin.Location = new System.Drawing.Point(18, 61);
            this.CheckBoxExportLog_RecycleBin.Name = "CheckBoxExportLog_RecycleBin";
            this.CheckBoxExportLog_RecycleBin.Size = new System.Drawing.Size(94, 16);
            this.CheckBoxExportLog_RecycleBin.TabIndex = 1;
            this.CheckBoxExportLog_RecycleBin.Text = "ログを出力する";
            this.CheckBoxExportLog_RecycleBin.UseVisualStyleBackColor = true;
            // 
            // CheckBoxRecycleBin
            // 
            this.CheckBoxRecycleBin.AutoSize = true;
            this.CheckBoxRecycleBin.Location = new System.Drawing.Point(18, 28);
            this.CheckBoxRecycleBin.Name = "CheckBoxRecycleBin";
            this.CheckBoxRecycleBin.Size = new System.Drawing.Size(134, 16);
            this.CheckBoxRecycleBin.TabIndex = 0;
            this.CheckBoxRecycleBin.Text = "サインイン時に確認する";
            this.CheckBoxRecycleBin.UseVisualStyleBackColor = true;
            this.CheckBoxRecycleBin.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // GroupBoxDownloads
            // 
            this.GroupBoxDownloads.Controls.Add(this.CheckBoxExportLog_Downloads);
            this.GroupBoxDownloads.Controls.Add(this.CheckBoxDownloads);
            this.GroupBoxDownloads.Location = new System.Drawing.Point(215, 35);
            this.GroupBoxDownloads.Name = "GroupBoxDownloads";
            this.GroupBoxDownloads.Size = new System.Drawing.Size(174, 100);
            this.GroupBoxDownloads.TabIndex = 3;
            this.GroupBoxDownloads.TabStop = false;
            this.GroupBoxDownloads.Text = "ダウンロードフォルダー";
            // 
            // CheckBoxExportLog_Downloads
            // 
            this.CheckBoxExportLog_Downloads.AutoSize = true;
            this.CheckBoxExportLog_Downloads.Enabled = false;
            this.CheckBoxExportLog_Downloads.Location = new System.Drawing.Point(18, 63);
            this.CheckBoxExportLog_Downloads.Name = "CheckBoxExportLog_Downloads";
            this.CheckBoxExportLog_Downloads.Size = new System.Drawing.Size(94, 16);
            this.CheckBoxExportLog_Downloads.TabIndex = 1;
            this.CheckBoxExportLog_Downloads.Text = "ログを出力する";
            this.CheckBoxExportLog_Downloads.UseVisualStyleBackColor = true;
            // 
            // CheckBoxDownloads
            // 
            this.CheckBoxDownloads.AutoSize = true;
            this.CheckBoxDownloads.Location = new System.Drawing.Point(18, 30);
            this.CheckBoxDownloads.Name = "CheckBoxDownloads";
            this.CheckBoxDownloads.Size = new System.Drawing.Size(134, 16);
            this.CheckBoxDownloads.TabIndex = 0;
            this.CheckBoxDownloads.Text = "サインイン時に確認する";
            this.CheckBoxDownloads.UseVisualStyleBackColor = true;
            this.CheckBoxDownloads.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // ButtonImportSetting
            // 
            this.ButtonImportSetting.Location = new System.Drawing.Point(134, 192);
            this.ButtonImportSetting.Name = "ButtonImportSetting";
            this.ButtonImportSetting.Size = new System.Drawing.Size(152, 23);
            this.ButtonImportSetting.TabIndex = 0;
            this.ButtonImportSetting.Text = "既存の設定を読み込む";
            this.ButtonImportSetting.UseVisualStyleBackColor = true;
            this.ButtonImportSetting.Click += new System.EventHandler(this.ButtonImportSetting_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDeleteSetting,
            this.ログを開くToolStripMenuItem,
            this.ToolStripMenuItemExecute});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(414, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemDeleteSetting
            // 
            this.ToolStripMenuItemDeleteSetting.Name = "ToolStripMenuItemDeleteSetting";
            this.ToolStripMenuItemDeleteSetting.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.ToolStripMenuItemDeleteSetting.Size = new System.Drawing.Size(143, 20);
            this.ToolStripMenuItemDeleteSetting.Text = "設定を削除する(Ctrl + D)";
            this.ToolStripMenuItemDeleteSetting.Click += new System.EventHandler(this.ToolStripMenuItemDeleteSetting_Click);
            // 
            // ログを開くToolStripMenuItem
            // 
            this.ログを開くToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpenLog,
            this.ToolStripMenuItemDeleteLog});
            this.ログを開くToolStripMenuItem.Name = "ログを開くToolStripMenuItem";
            this.ログを開くToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.ログを開くToolStripMenuItem.Text = "ログの操作";
            // 
            // ToolStripMenuItemOpenLog
            // 
            this.ToolStripMenuItemOpenLog.Name = "ToolStripMenuItemOpenLog";
            this.ToolStripMenuItemOpenLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.ToolStripMenuItemOpenLog.Size = new System.Drawing.Size(198, 22);
            this.ToolStripMenuItemOpenLog.Text = "ログを開く(O)";
            this.ToolStripMenuItemOpenLog.Click += new System.EventHandler(this.ToolStripMenuItemOpenLog_Click);
            // 
            // ToolStripMenuItemDeleteLog
            // 
            this.ToolStripMenuItemDeleteLog.Name = "ToolStripMenuItemDeleteLog";
            this.ToolStripMenuItemDeleteLog.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D)));
            this.ToolStripMenuItemDeleteLog.Size = new System.Drawing.Size(198, 22);
            this.ToolStripMenuItemDeleteLog.Text = "ログを削除する(D)";
            this.ToolStripMenuItemDeleteLog.Click += new System.EventHandler(this.ToolStripMenuItemDeleteLog_Click);
            // 
            // ToolStripMenuItemExecute
            // 
            this.ToolStripMenuItemExecute.Name = "ToolStripMenuItemExecute";
            this.ToolStripMenuItemExecute.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.ToolStripMenuItemExecute.Size = new System.Drawing.Size(58, 20);
            this.ToolStripMenuItemExecute.Text = "実行(R)";
            this.ToolStripMenuItemExecute.Click += new System.EventHandler(this.ToolStripMenuItemExecute_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // CheckBoxCountDesktopFiles
            // 
            this.CheckBoxCountDesktopFiles.AutoSize = true;
            this.CheckBoxCountDesktopFiles.Location = new System.Drawing.Point(12, 150);
            this.CheckBoxCountDesktopFiles.Name = "CheckBoxCountDesktopFiles";
            this.CheckBoxCountDesktopFiles.Size = new System.Drawing.Size(230, 16);
            this.CheckBoxCountDesktopFiles.TabIndex = 2;
            this.CheckBoxCountDesktopFiles.Text = "Desktopのファイル数が多い場合に警告する";
            this.CheckBoxCountDesktopFiles.UseVisualStyleBackColor = true;
            this.CheckBoxCountDesktopFiles.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // TrackBarCountDesktopFiles
            // 
            this.TrackBarCountDesktopFiles.Enabled = false;
            this.TrackBarCountDesktopFiles.Location = new System.Drawing.Point(247, 141);
            this.TrackBarCountDesktopFiles.Maximum = 20;
            this.TrackBarCountDesktopFiles.Minimum = 1;
            this.TrackBarCountDesktopFiles.Name = "TrackBarCountDesktopFiles";
            this.TrackBarCountDesktopFiles.Size = new System.Drawing.Size(120, 45);
            this.TrackBarCountDesktopFiles.TabIndex = 5;
            this.TrackBarCountDesktopFiles.TickFrequency = 2;
            this.TrackBarCountDesktopFiles.Value = 1;
            this.TrackBarCountDesktopFiles.ValueChanged += new System.EventHandler(this.TrackBarCountDesktopFiles_ValueChanged);
            // 
            // LabelCountDesktopFiles
            // 
            this.LabelCountDesktopFiles.AutoSize = true;
            this.LabelCountDesktopFiles.Location = new System.Drawing.Point(378, 151);
            this.LabelCountDesktopFiles.Name = "LabelCountDesktopFiles";
            this.LabelCountDesktopFiles.Size = new System.Drawing.Size(11, 12);
            this.LabelCountDesktopFiles.TabIndex = 2;
            this.LabelCountDesktopFiles.Text = "1";
            // 
            // CheckBoxDesktopMaxCountExportLog
            // 
            this.CheckBoxDesktopMaxCountExportLog.AutoSize = true;
            this.CheckBoxDesktopMaxCountExportLog.Location = new System.Drawing.Point(12, 178);
            this.CheckBoxDesktopMaxCountExportLog.Name = "CheckBoxDesktopMaxCountExportLog";
            this.CheckBoxDesktopMaxCountExportLog.Size = new System.Drawing.Size(94, 16);
            this.CheckBoxDesktopMaxCountExportLog.TabIndex = 6;
            this.CheckBoxDesktopMaxCountExportLog.Text = "ログを出力する";
            this.CheckBoxDesktopMaxCountExportLog.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(414, 236);
            this.Controls.Add(this.CheckBoxDesktopMaxCountExportLog);
            this.Controls.Add(this.LabelCountDesktopFiles);
            this.Controls.Add(this.TrackBarCountDesktopFiles);
            this.Controls.Add(this.CheckBoxCountDesktopFiles);
            this.Controls.Add(this.ButtonImportSetting);
            this.Controls.Add(this.GroupBoxDownloads);
            this.Controls.Add(this.GroupBoxRecycleBin);
            this.Controls.Add(this.ButtonSet);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(430, 275);
            this.MinimumSize = new System.Drawing.Size(430, 275);
            this.Name = "MainForm";
            this.Text = "ディレクトリチェッカー";
            this.GroupBoxRecycleBin.ResumeLayout(false);
            this.GroupBoxRecycleBin.PerformLayout();
            this.GroupBoxDownloads.ResumeLayout(false);
            this.GroupBoxDownloads.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarCountDesktopFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSet;
        private System.Windows.Forms.GroupBox GroupBoxRecycleBin;
        private System.Windows.Forms.CheckBox CheckBoxExportLog_RecycleBin;
        private System.Windows.Forms.CheckBox CheckBoxRecycleBin;
        private System.Windows.Forms.GroupBox GroupBoxDownloads;
        private System.Windows.Forms.CheckBox CheckBoxExportLog_Downloads;
        private System.Windows.Forms.CheckBox CheckBoxDownloads;
        private System.Windows.Forms.Button ButtonImportSetting;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteSetting;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem ログを開くToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpenLog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteLog;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExecute;
        private System.Windows.Forms.CheckBox CheckBoxCountDesktopFiles;
        private System.Windows.Forms.TrackBar TrackBarCountDesktopFiles;
        private System.Windows.Forms.Label LabelCountDesktopFiles;
        private System.Windows.Forms.CheckBox CheckBoxDesktopMaxCountExportLog;
    }
}

