namespace NXGenponChecker
{
    partial class FileListsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ListViewNGList = new System.Windows.Forms.ListView();
            this.ColumnHeaderFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderIsOK = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ButtonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ListViewNGList
            // 
            this.ListViewNGList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewNGList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderFileName,
            this.ColumnHeaderIsOK});
            this.ListViewNGList.FullRowSelect = true;
            this.ListViewNGList.GridLines = true;
            this.ListViewNGList.Location = new System.Drawing.Point(12, 12);
            this.ListViewNGList.Name = "ListViewNGList";
            this.ListViewNGList.Size = new System.Drawing.Size(721, 151);
            this.ListViewNGList.TabIndex = 0;
            this.ListViewNGList.UseCompatibleStateImageBehavior = false;
            this.ListViewNGList.View = System.Windows.Forms.View.Details;
            this.ListViewNGList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewNGList_MouseDoubleClick);
            // 
            // ColumnHeaderFileName
            // 
            this.ColumnHeaderFileName.Text = "ファイル名";
            this.ColumnHeaderFileName.Width = 620;
            // 
            // ColumnHeaderIsOK
            // 
            this.ColumnHeaderIsOK.Text = "比較結果";
            this.ColumnHeaderIsOK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ButtonClose
            // 
            this.ButtonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonClose.Location = new System.Drawing.Point(658, 169);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(75, 23);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "閉じる";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // FileListsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(745, 204);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ListViewNGList);
            this.MaximumSize = new System.Drawing.Size(761, 243);
            this.MinimumSize = new System.Drawing.Size(761, 243);
            this.Name = "FileListsForm";
            this.Text = "tmp";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView ListViewNGList;
        private System.Windows.Forms.ColumnHeader ColumnHeaderFileName;
        private System.Windows.Forms.ColumnHeader ColumnHeaderIsOK;
        private System.Windows.Forms.Button ButtonClose;
    }
}