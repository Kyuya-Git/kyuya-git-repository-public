namespace DiskMon
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
            this.ButtonSwitch = new System.Windows.Forms.Button();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.BkgWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // ButtonSwitch
            // 
            this.ButtonSwitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ButtonSwitch.Location = new System.Drawing.Point(93, 56);
            this.ButtonSwitch.Name = "ButtonSwitch";
            this.ButtonSwitch.Size = new System.Drawing.Size(75, 23);
            this.ButtonSwitch.TabIndex = 0;
            this.ButtonSwitch.Text = "開始";
            this.ButtonSwitch.UseVisualStyleBackColor = false;
            this.ButtonSwitch.Click += new System.EventHandler(this.ButtonSwitch_Click);
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(106, 24);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(47, 12);
            this.LabelStatus.TabIndex = 2;
            this.LabelStatus.Text = "停止中...";
            // 
            // BkgWorker
            // 
            this.BkgWorker.WorkerSupportsCancellation = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 91);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.ButtonSwitch);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(263, 130);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(263, 130);
            this.Name = "MainForm";
            this.Text = "DiskMon";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonSwitch;
        private System.Windows.Forms.Label LabelStatus;
        private System.ComponentModel.BackgroundWorker BkgWorker;
    }
}

