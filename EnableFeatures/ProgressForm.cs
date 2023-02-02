using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EnableFeatures
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private BackgroundWorker _worker = new BackgroundWorker();
        public BackgroundWorker Worker
        {
            get { return _worker; }
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            _worker.RunWorkerAsync();
        }
    }
}
