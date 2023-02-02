namespace NXGenponChecker
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
            this.ListViewResult = new System.Windows.Forms.ListView();
            this.ColumnHeaderFolderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderReflected = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TextBoxUpdate = new System.Windows.Forms.TextBox();
            this.TextBoxGenpon = new System.Windows.Forms.TextBox();
            this.LabelUpdate = new System.Windows.Forms.Label();
            this.LabelGenponn = new System.Windows.Forms.Label();
            this.ButtonExec = new System.Windows.Forms.Button();
            this.LabelProgress = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ListViewResult
            // 
            this.ListViewResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderFolderName,
            this.ColumnHeaderReflected});
            this.ListViewResult.FullRowSelect = true;
            this.ListViewResult.GridLines = true;
            this.ListViewResult.Location = new System.Drawing.Point(12, 12);
            this.ListViewResult.Name = "ListViewResult";
            this.ListViewResult.Size = new System.Drawing.Size(419, 184);
            this.ListViewResult.TabIndex = 0;
            this.ListViewResult.UseCompatibleStateImageBehavior = false;
            this.ListViewResult.View = System.Windows.Forms.View.Details;
            this.ListViewResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewResult_MouseDoubleClick);
            // 
            // ColumnHeaderFolderName
            // 
            this.ColumnHeaderFolderName.Text = "フォルダー名";
            this.ColumnHeaderFolderName.Width = 335;
            // 
            // ColumnHeaderReflected
            // 
            this.ColumnHeaderReflected.Text = "提出済み";
            this.ColumnHeaderReflected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextBoxUpdate
            // 
            this.TextBoxUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxUpdate.Location = new System.Drawing.Point(77, 211);
            this.TextBoxUpdate.Name = "TextBoxUpdate";
            this.TextBoxUpdate.Size = new System.Drawing.Size(354, 19);
            this.TextBoxUpdate.TabIndex = 1;
            // 
            // TextBoxGenpon
            // 
            this.TextBoxGenpon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxGenpon.Location = new System.Drawing.Point(77, 236);
            this.TextBoxGenpon.Name = "TextBoxGenpon";
            this.TextBoxGenpon.Size = new System.Drawing.Size(354, 19);
            this.TextBoxGenpon.TabIndex = 2;
            // 
            // LabelUpdate
            // 
            this.LabelUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelUpdate.Location = new System.Drawing.Point(12, 209);
            this.LabelUpdate.Name = "LabelUpdate";
            this.LabelUpdate.Size = new System.Drawing.Size(59, 23);
            this.LabelUpdate.TabIndex = 3;
            this.LabelUpdate.Text = "受付：";
            this.LabelUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LabelGenponn
            // 
            this.LabelGenponn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelGenponn.Location = new System.Drawing.Point(14, 234);
            this.LabelGenponn.Name = "LabelGenponn";
            this.LabelGenponn.Size = new System.Drawing.Size(57, 19);
            this.LabelGenponn.TabIndex = 4;
            this.LabelGenponn.Text = "原本：";
            this.LabelGenponn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ButtonExec
            // 
            this.ButtonExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonExec.Location = new System.Drawing.Point(356, 261);
            this.ButtonExec.Name = "ButtonExec";
            this.ButtonExec.Size = new System.Drawing.Size(75, 23);
            this.ButtonExec.TabIndex = 5;
            this.ButtonExec.Text = "実行";
            this.ButtonExec.UseVisualStyleBackColor = true;
            this.ButtonExec.Click += new System.EventHandler(this.ButtonExec_Click);
            // 
            // LabelProgress
            // 
            this.LabelProgress.Location = new System.Drawing.Point(10, 258);
            this.LabelProgress.Name = "LabelProgress";
            this.LabelProgress.Size = new System.Drawing.Size(340, 26);
            this.LabelProgress.TabIndex = 6;
            this.LabelProgress.Text = "...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 292);
            this.Controls.Add(this.LabelProgress);
            this.Controls.Add(this.ButtonExec);
            this.Controls.Add(this.LabelGenponn);
            this.Controls.Add(this.LabelUpdate);
            this.Controls.Add(this.TextBoxGenpon);
            this.Controls.Add(this.TextBoxUpdate);
            this.Controls.Add(this.ListViewResult);
            this.MaximumSize = new System.Drawing.Size(459, 331);
            this.MinimumSize = new System.Drawing.Size(459, 331);
            this.Name = "MainForm";
            this.Text = "NXGenponChecker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ListViewResult;
        private System.Windows.Forms.ColumnHeader ColumnHeaderFolderName;
        private System.Windows.Forms.ColumnHeader ColumnHeaderReflected;
        private System.Windows.Forms.TextBox TextBoxUpdate;
        private System.Windows.Forms.TextBox TextBoxGenpon;
        private System.Windows.Forms.Label LabelUpdate;
        private System.Windows.Forms.Label LabelGenponn;
        private System.Windows.Forms.Button ButtonExec;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.Label LabelProgress;
    }
}

