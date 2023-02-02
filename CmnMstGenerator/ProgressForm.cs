using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using NPOI.SS.UserModel;
using System.ComponentModel;

namespace CmnMstGenerator
{
    public partial class ProgressForm : Form
    {
        string basePath;
        string[] targetFiles;
        string outputPath;
        public CurrentPosition cp;

        public ProgressForm(string _basePath, string[] _targetFiles, string _outputPath)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            basePath = _basePath;
            targetFiles = _targetFiles;
            this.ProgressBar.Minimum = 0;
            this.ProgressBar.Maximum = GetTargetSheetsCount();
            outputPath = _outputPath;

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.WorkerSupportsCancellation = true;

            //bgWorkerのデリゲートにメソッドを登録
            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }
        private int GetTargetSheetsCount()
        {
            int cntSheets = 0;
            foreach (string file in targetFiles)
            {
                string pathExcel = Path.Combine(basePath, file);
                IWorkbook book;
                using (FileStream excelFile = new FileStream(pathExcel, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    book = WorkbookFactory.Create(excelFile);
                }

                for (int i = 0; i < book.NumberOfSheets; i++)
                {
                    string sheetName = book.GetSheetName(i);
                    if (sheetName.StartsWith("●")) cntSheets++;
                }
            }
            return cntSheets;
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TableInfo> result = new List<TableInfo>();
            bgWorker.ReportProgress(0, "ProgressBar|処理を開始します。");

            //対象のファイルを１つずつ読み込む
            for (int i = 0; i < targetFiles.Length; i++)
            {
                cp = new CurrentPosition();
                cp.FileName = targetFiles[i];
                bgWorker.ReportProgress(0, string.Format("Form|{0}{1}を処理中({2}/{3})", "進捗状況...", cp.FileName, i + 1, targetFiles.Length));

                IWorkbook book;
                using (FileStream excelFile = new FileStream(Path.Combine(basePath, cp.FileName), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    book = WorkbookFactory.Create(excelFile);
                }

                for (int j = 0; j < book.NumberOfSheets; j++) //「j」= シートのインデックス
                {
                    //［キャンセル］ボタンがクリックされた場合は中止する
                    if (bgWorker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    cp.SheetIndex = j;
                    cp.SheetName = book.GetSheetName(j);

                    //「●」で始まるシートのみ処理する
                    if (!cp.SheetName.StartsWith("●"))
                        continue;

                    string reportMsg = string.Format("ProgressBar|{0}を処理中({1}/{2})...",
                        cp.SheetName, //{0}
                        this.ProgressBar.Value + 1, //{1}
                        this.ProgressBar.Maximum); //{2}
                    bgWorker.ReportProgress(this.ProgressBar.Value + 1, reportMsg);

                    #region シート読み込み
                    TableInfo ti = new TableInfo();

                    cp.RowIndex = 0;
                    cp.ColumnIndex = 0;
                    string tag = (string)ExcelReadHelper.GetValue(book, cp);
                    //先頭のセルが「STOP」の行まで1行づつ読み込む
                    while (tag.ToUpper() != "STOP")
                    {
                        if (tag.ToUpper() == "T")
                        {
                            cp.ColumnIndex = 5;
                            ti.Name = (string)ExcelReadHelper.GetValue(book, cp);
                        }
                        else if (tag.ToUpper() == "F")
                        {
                            Field field = new Field();
                            string value = "";
                            for (int k = 5; k < 12; k++) //フィールドごとにセルF列～L列を取得する
                            {
                                cp.ColumnIndex = k;
                                value = ExcelReadHelper.GetValue(book, cp).ToString();
                                switch (k)
                                {
                                    case 5://FieldName
                                        field.Name = string.IsNullOrEmpty(value) ? "" : value;
                                        break;
                                    case 6://型
                                        field.DataType = string.IsNullOrEmpty(value) ? "" : value;
                                        break;
                                    case 7://桁数
                                        field.Digits = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                                        break;
                                    case 8://小数桁
                                        field.DigitsDicimal = string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
                                        break;
                                    case 9://NULL
                                        field.Nullable = string.IsNullOrEmpty(value) ? true : value != "不可";
                                        break;
                                    case 10://DEF
                                        field.SetDefault = string.IsNullOrEmpty(value) ? false : value == "有";
                                        break;
                                    case 11://値
                                        field.DefaultValue = string.IsNullOrEmpty(value) ? "" : value;
                                        break;
                                }
                            }
                            ti.FieldList.Add(field);
                        }
                        else if (tag.ToUpper() == "P")
                        {
                            cp.ColumnIndex = 5;
                            ti.PrimaryKeyList.Add(ExcelReadHelper.GetValue(book, cp).ToString());
                        }
                        else if (tag.ToUpper() == "K")
                        {
                            IndexInfo idxInfo = new IndexInfo();
                            cp.ColumnIndex = 5;
                            idxInfo.KeyName = ExcelReadHelper.GetValue(book, cp).ToString();
                            cp.ColumnIndex = 6;
                            string isUnique = ExcelReadHelper.GetValue(book, cp).ToString();
                            idxInfo.IsUnique = string.IsNullOrEmpty(isUnique) ? false : isUnique == "不可";

                            cp.RowIndex++;
                            cp.ColumnIndex = 0;
                            string tag2 = ExcelReadHelper.GetValue(book, cp).ToString();
                            while (tag2.ToUpper() == "I")
                            {
                                cp.ColumnIndex = 5;
                                idxInfo.FieldNameList.Add(ExcelReadHelper.GetValue(book, cp).ToString());
                                cp.RowIndex++;
                                cp.ColumnIndex = 0;
                                tag2 = ExcelReadHelper.GetValue(book, cp).ToString();
                            }
                            ti.IndexList.Add(idxInfo);
                            cp.RowIndex--;
                        }
                        cp.RowIndex++;
                        cp.ColumnIndex = 0;
                        tag = (string)ExcelReadHelper.GetValue(book, cp);
                    }
                    result.Add(ti);
                    #endregion
                }
            }
            e.Result = result;
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // 要素0に対象のコントロールが、要素1にメッセージが格納されている。
            string[] arry = e.UserState.ToString().Split('|');
            if (arry[0].ToUpper() == "FORM")
            {
                this.Text = arry[1];
            }
            else if (arry[0].ToUpper() == "PROGRESSBAR")
            {
                //プログレスバーの値を変更する
                if (e.ProgressPercentage < this.ProgressBar.Minimum)
                {
                    this.ProgressBar.Value = this.ProgressBar.Minimum;
                }
                else if (this.ProgressBar.Maximum < e.ProgressPercentage)
                {
                    this.ProgressBar.Value = this.ProgressBar.Maximum;
                }
                else
                {
                    this.ProgressBar.Value = e.ProgressPercentage;
                }
                //メッセージのテキストを変更する
                this.LabelProgress.Text = arry[1];
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this._error = e.Error;
                this.DialogResult = DialogResult.Abort;
            }
            else if (e.Cancelled)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this._result = e.Result;
                this.DialogResult = DialogResult.OK;
            }
            this.Close();
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            this.bgWorker.RunWorkerAsync();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            ButtonCancel.Enabled = false;
            bgWorker.CancelAsync();
        }

        #region MainFormにExcelから取得したデータを受け渡すためのプロパティ
        private object _result = null;
        public object Result
        {
            get
            {
                return this._result;
            }
        }
        #endregion

        #region エラーが発生した場合にMainFormにエラーを受け渡すためのプロパティ
        private Exception _error = null;
        public Exception Error
        {
            get
            {
                return this._error;
            }
        }
        #endregion
    }
}
