using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NXGenponChecker
{
    public partial class FileListsForm : Form
    {
        public FileListsForm()
        {
            InitializeComponent();
        }

        RecordEntity rec;

        public FileListsForm(RecordEntity _rec)
            : this()
        {
            rec = _rec;
            rec.AppSvr.ForEach(f =>
            {
                ListViewItem item = new ListViewItem(f.Path.Replace(rec.BaseFolderFullName, "").TrimStart('\\'));
                item.SubItems.Add(f.Reflected ? "OK" : "NG");
                ListViewNGList.Items.Add(item);
            });

            rec.CtrlSvr.ForEach(f =>
            {
                ListViewItem item = new ListViewItem(f.Path.Replace(rec.BaseFolderFullName, "").TrimStart('\\'));
                item.SubItems.Add(f.Reflected ? "OK" : "NG");
                ListViewNGList.Items.Add(item);
            });

            foreach (ListViewItem i in ListViewNGList.Items)
            {
                if (i.SubItems[1].Text == "NG")
                {
                    i.BackColor = Color.Pink;
                }
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListViewNGList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListView lview = (ListView)sender;
            //lview.SelectedItems[0].Text.ToUpper()
            string selectedrec = lview.SelectedItems[0].Text.ToUpper();
            string targetPath = string.Empty;
            targetPath = Path.Combine(rec.BaseFolderFullName, selectedrec);

            if (File.Exists(targetPath)) 
            {
                DirectoryInfo di = new DirectoryInfo(targetPath);
                System.Diagnostics.Process.Start("EXPLORER.EXE", di.Parent.FullName);
            }
        }
    }
}
