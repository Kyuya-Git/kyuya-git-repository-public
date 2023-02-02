namespace CmnMstGenerator
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
            this.TextBoxImportFolder = new System.Windows.Forms.TextBox();
            this.LabelImport = new System.Windows.Forms.Label();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.LabelExport = new System.Windows.Forms.Label();
            this.TextBoxExportFolder = new System.Windows.Forms.TextBox();
            this.ButtonExport = new System.Windows.Forms.Button();
            this.ButtonExecute = new System.Windows.Forms.Button();
            this.LabelTargetFiles = new System.Windows.Forms.Label();
            this.LabelStoredFIles = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.ListViewTargetFiles = new System.Windows.Forms.ListView();
            this.ListViewStoredFiles = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // TextBoxImportFolder
            // 
            this.TextBoxImportFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxImportFolder.Location = new System.Drawing.Point(84, 12);
            this.TextBoxImportFolder.Name = "TextBoxImportFolder";
            this.TextBoxImportFolder.Size = new System.Drawing.Size(610, 19);
            this.TextBoxImportFolder.TabIndex = 1;
            this.TextBoxImportFolder.TextChanged += new System.EventHandler(this.UpdateStoredFiles);
            this.TextBoxImportFolder.Leave += new System.EventHandler(this.UpdateStoredFiles);
            // 
            // LabelImport
            // 
            this.LabelImport.AutoSize = true;
            this.LabelImport.Location = new System.Drawing.Point(12, 15);
            this.LabelImport.Name = "LabelImport";
            this.LabelImport.Size = new System.Drawing.Size(66, 12);
            this.LabelImport.TabIndex = 2;
            this.LabelImport.Text = "取り込み先：";
            // 
            // ButtonImport
            // 
            this.ButtonImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonImport.Location = new System.Drawing.Point(695, 12);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(27, 20);
            this.ButtonImport.TabIndex = 3;
            this.ButtonImport.Text = "...";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonFolderBrowserDialog_Clicks);
            // 
            // LabelExport
            // 
            this.LabelExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LabelExport.AutoSize = true;
            this.LabelExport.Location = new System.Drawing.Point(12, 188);
            this.LabelExport.Name = "LabelExport";
            this.LabelExport.Size = new System.Drawing.Size(59, 12);
            this.LabelExport.TabIndex = 4;
            this.LabelExport.Text = "出力先   ：";
            // 
            // TextBoxExportFolder
            // 
            this.TextBoxExportFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxExportFolder.Location = new System.Drawing.Point(84, 185);
            this.TextBoxExportFolder.Name = "TextBoxExportFolder";
            this.TextBoxExportFolder.Size = new System.Drawing.Size(610, 19);
            this.TextBoxExportFolder.TabIndex = 5;
            // 
            // ButtonExport
            // 
            this.ButtonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonExport.Location = new System.Drawing.Point(695, 184);
            this.ButtonExport.Name = "ButtonExport";
            this.ButtonExport.Size = new System.Drawing.Size(27, 20);
            this.ButtonExport.TabIndex = 6;
            this.ButtonExport.Text = "...";
            this.ButtonExport.UseVisualStyleBackColor = true;
            this.ButtonExport.Click += new System.EventHandler(this.ButtonFolderBrowserDialog_Clicks);
            // 
            // ButtonExecute
            // 
            this.ButtonExecute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonExecute.Location = new System.Drawing.Point(667, 214);
            this.ButtonExecute.Name = "ButtonExecute";
            this.ButtonExecute.Size = new System.Drawing.Size(55, 27);
            this.ButtonExecute.TabIndex = 7;
            this.ButtonExecute.Text = "実行";
            this.ButtonExecute.UseVisualStyleBackColor = true;
            this.ButtonExecute.Click += new System.EventHandler(this.ButtonExecute_Click);
            // 
            // LabelTargetFiles
            // 
            this.LabelTargetFiles.AutoSize = true;
            this.LabelTargetFiles.Location = new System.Drawing.Point(13, 45);
            this.LabelTargetFiles.Name = "LabelTargetFiles";
            this.LabelTargetFiles.Size = new System.Drawing.Size(69, 12);
            this.LabelTargetFiles.TabIndex = 9;
            this.LabelTargetFiles.Text = "TargetFiles：";
            // 
            // LabelStoredFIles
            // 
            this.LabelStoredFIles.AutoSize = true;
            this.LabelStoredFIles.Location = new System.Drawing.Point(370, 45);
            this.LabelStoredFIles.Name = "LabelStoredFIles";
            this.LabelStoredFIles.Size = new System.Drawing.Size(116, 12);
            this.LabelStoredFIles.TabIndex = 10;
            this.LabelStoredFIles.Text = "格納されているファイル：";
            // 
            // ListViewTargetFiles
            // 
            this.ListViewTargetFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewTargetFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.ListViewTargetFiles.FullRowSelect = true;
            this.ListViewTargetFiles.GridLines = true;
            this.ListViewTargetFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListViewTargetFiles.HideSelection = false;
            this.ListViewTargetFiles.Location = new System.Drawing.Point(15, 60);
            this.ListViewTargetFiles.Name = "ListViewTargetFiles";
            this.ListViewTargetFiles.Size = new System.Drawing.Size(350, 102);
            this.ListViewTargetFiles.TabIndex = 11;
            this.ListViewTargetFiles.UseCompatibleStateImageBehavior = false;
            this.ListViewTargetFiles.View = System.Windows.Forms.View.Details;
            // 
            // ListViewStoredFiles
            // 
            this.ListViewStoredFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewStoredFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeader});
            this.ListViewStoredFiles.FullRowSelect = true;
            this.ListViewStoredFiles.GridLines = true;
            this.ListViewStoredFiles.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ListViewStoredFiles.HideSelection = false;
            this.ListViewStoredFiles.Location = new System.Drawing.Point(372, 60);
            this.ListViewStoredFiles.Name = "ListViewStoredFiles";
            this.ListViewStoredFiles.Size = new System.Drawing.Size(350, 102);
            this.ListViewStoredFiles.TabIndex = 12;
            this.ListViewStoredFiles.UseCompatibleStateImageBehavior = false;
            this.ListViewStoredFiles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 350;
            // 
            // ColumnHeader
            // 
            this.ColumnHeader.Width = 350;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 253);
            this.Controls.Add(this.ListViewStoredFiles);
            this.Controls.Add(this.ListViewTargetFiles);
            this.Controls.Add(this.LabelStoredFIles);
            this.Controls.Add(this.LabelTargetFiles);
            this.Controls.Add(this.ButtonExecute);
            this.Controls.Add(this.ButtonExport);
            this.Controls.Add(this.TextBoxExportFolder);
            this.Controls.Add(this.LabelExport);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.LabelImport);
            this.Controls.Add(this.TextBoxImportFolder);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 500);
            this.MinimumSize = new System.Drawing.Size(750, 250);
            this.Name = "MainForm";
            this.Text = "CmnMstGenerator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBoxImportFolder;
        private System.Windows.Forms.Label LabelImport;
        private System.Windows.Forms.Button ButtonImport;
        private System.Windows.Forms.Label LabelExport;
        private System.Windows.Forms.TextBox TextBoxExportFolder;
        private System.Windows.Forms.Button ButtonExport;
        private System.Windows.Forms.Button ButtonExecute;
        private System.Windows.Forms.Label LabelTargetFiles;
        private System.Windows.Forms.Label LabelStoredFIles;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ListView ListViewTargetFiles;
        private System.Windows.Forms.ListView ListViewStoredFiles;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader ColumnHeader;
    }
}

