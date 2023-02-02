namespace MTLFileWatchForm
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
            this.ListBoxResult = new System.Windows.Forms.ListBox();
            this.ButtonSwitch = new System.Windows.Forms.Button();
            this.ButtonClear = new System.Windows.Forms.Button();
            this.TextBoxTargetFolder = new System.Windows.Forms.TextBox();
            this.LabelTargetFolder = new System.Windows.Forms.Label();
            this.BkgWorker = new System.ComponentModel.BackgroundWorker();
            this.LabelFilter = new System.Windows.Forms.Label();
            this.TextBoxFilter = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ListBoxResult
            // 
            this.ListBoxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBoxResult.FormattingEnabled = true;
            this.ListBoxResult.ItemHeight = 12;
            this.ListBoxResult.Location = new System.Drawing.Point(12, 12);
            this.ListBoxResult.Name = "ListBoxResult";
            this.ListBoxResult.Size = new System.Drawing.Size(555, 112);
            this.ListBoxResult.TabIndex = 0;
            // 
            // ButtonSwitch
            // 
            this.ButtonSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSwitch.BackColor = System.Drawing.Color.LightGreen;
            this.ButtonSwitch.Location = new System.Drawing.Point(492, 200);
            this.ButtonSwitch.Name = "ButtonSwitch";
            this.ButtonSwitch.Size = new System.Drawing.Size(75, 23);
            this.ButtonSwitch.TabIndex = 1;
            this.ButtonSwitch.Text = "開始";
            this.ButtonSwitch.UseVisualStyleBackColor = false;
            this.ButtonSwitch.Click += new System.EventHandler(this.ButtonExec_Click);
            // 
            // ButtonClear
            // 
            this.ButtonClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClear.Location = new System.Drawing.Point(372, 200);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(75, 23);
            this.ButtonClear.TabIndex = 2;
            this.ButtonClear.Text = "履歴クリア";
            this.ButtonClear.UseVisualStyleBackColor = true;
            this.ButtonClear.Click += new System.EventHandler(this.ButtonClear_Click);
            // 
            // TextBoxTargetFolder
            // 
            this.TextBoxTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxTargetFolder.Location = new System.Drawing.Point(119, 140);
            this.TextBoxTargetFolder.Name = "TextBoxTargetFolder";
            this.TextBoxTargetFolder.Size = new System.Drawing.Size(448, 19);
            this.TextBoxTargetFolder.TabIndex = 3;
            // 
            // LabelTargetFolder
            // 
            this.LabelTargetFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTargetFolder.AutoSize = true;
            this.LabelTargetFolder.Location = new System.Drawing.Point(33, 143);
            this.LabelTargetFolder.Name = "LabelTargetFolder";
            this.LabelTargetFolder.Size = new System.Drawing.Size(80, 12);
            this.LabelTargetFolder.TabIndex = 4;
            this.LabelTargetFolder.Text = "対象フォルダー：";
            // 
            // BkgWorker
            // 
            this.BkgWorker.WorkerSupportsCancellation = true;
            // 
            // LabelFilter
            // 
            this.LabelFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelFilter.AutoSize = true;
            this.LabelFilter.Location = new System.Drawing.Point(312, 168);
            this.LabelFilter.Name = "LabelFilter";
            this.LabelFilter.Size = new System.Drawing.Size(54, 12);
            this.LabelFilter.TabIndex = 6;
            this.LabelFilter.Text = "フィルター：";
            // 
            // TextBoxFilter
            // 
            this.TextBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxFilter.Location = new System.Drawing.Point(372, 165);
            this.TextBoxFilter.Name = "TextBoxFilter";
            this.TextBoxFilter.Size = new System.Drawing.Size(195, 19);
            this.TextBoxFilter.TabIndex = 5;
            this.TextBoxFilter.Text = "*.*";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 235);
            this.Controls.Add(this.LabelFilter);
            this.Controls.Add(this.TextBoxFilter);
            this.Controls.Add(this.LabelTargetFolder);
            this.Controls.Add(this.TextBoxTargetFolder);
            this.Controls.Add(this.ButtonClear);
            this.Controls.Add(this.ButtonSwitch);
            this.Controls.Add(this.ListBoxResult);
            this.MinimumSize = new System.Drawing.Size(595, 274);
            this.Name = "MainForm";
            this.Text = "フォルダ監視ツール";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ListBoxResult;
        private System.Windows.Forms.Button ButtonSwitch;
        private System.Windows.Forms.Button ButtonClear;
        private System.Windows.Forms.TextBox TextBoxTargetFolder;
        private System.Windows.Forms.Label LabelTargetFolder;
        private System.ComponentModel.BackgroundWorker BkgWorker;
        private System.Windows.Forms.Label LabelFilter;
        private System.Windows.Forms.TextBox TextBoxFilter;
    }
}

