using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace SP_Transfer_Tool
{
    public partial class frmMain : Form
    {
        public Dictionary<string,string> dicSUMapping = new Dictionary<string, string>();

        public frmMain()
        {
            InitializeComponent();
            if (File.Exists("SUMapping.csv"))
            {
                var mappingItems =  File.ReadAllLines("SUMapping.csv");

                for (int i = 1; i < mappingItems.Length; i++)
                {
                    var item = mappingItems[i];
                    var mapping = item.Split(new char[] { ',' });
                    if (mapping.Length == 2)
                    {
                        if (!string.IsNullOrWhiteSpace(mapping[0]) && !string.IsNullOrWhiteSpace(mapping[1]))
                        {
                            dicSUMapping[mapping[0].Trim('\"')] = mapping[1].Trim('\"');
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("SU Mapping file does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            this.Text = this.Text + " V" + this.GetType().Assembly.GetName().Version;

        }
        
        private void BtnSelectFile_Click(object sender, EventArgs e)
        {
            var tbx = tbxSRCFile;

            if (sender == btnSelectSRCFile)
            {
                tbx = tbxSRCFile;
            }
            else
            {
                tbx = tbxTargetFile;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Excel File|*.xlsx";
            ofd.FileName = tbx.Text;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbx.Text = ofd.FileName;
            }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            
                if (!File.Exists(tbxSRCFile.Text))
                {
                    MessageBox.Show("Please select Source File!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }

                if (!File.Exists(tbxTargetFile.Text))
                {
                    MessageBox.Show("Please select Target File!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                
                }

                btnTransfer.Enabled = false;
                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        WriteLog("Start reading source file...");
                        DataTable
                            dtSource = ReadToDataTable(tbxSRCFile.Text, true); //First Row is used as Column header
                        WriteLog("Complete reading source file, row count:" + dtSource.Rows.Count + 1);

                        WriteLog("Start reading target file.");
                        DataTable
                            dtTarget = ReadToDataTable(tbxTargetFile.Text,
                                false); //First Row is not used as Column header
                        WriteLog("Complete reading target file, row count:" + dtTarget.Rows.Count);
                        List<string> dateList = new List<string>();


                        WriteLog("Start parsing date to search from target file...");
                        for (int colIndex = 14; colIndex < dtTarget.Columns.Count; colIndex++)
                        {
                            var date = dtTarget.Rows[1][colIndex].ToString();
                            var splitDate = date.Split(new char[] {'/'});
                            if (splitDate.Length == 3)
                                dateList.Add("20" + splitDate[2] + splitDate[0] + splitDate[1]);
                            else
                                dateList.Add(date);
                        }

                        WriteLog("Complete parsing date to search from target file. Count:" + dateList.Count);


                        WriteLog("Start fetching rows to search from target file...");
                        var targetRows = dtTarget.AsEnumerable()
                            .Where(p => p.Field<string>("Column9") == "OEM Ship Request");
                        WriteLog("Complete fetching rows to search from target file.Row Count:" + targetRows.Count());


                        this.InvokeIfRequired(P =>
                        {
                            progressBar1.Visible = true;
                            progressBar1.Maximum = targetRows.Count();
                            progressBar1.Value = 0;
                            lblProgress.Visible = true;
                            lblProgress.Text = string.Format("{0}/{1}", 0, progressBar1.Maximum);
                        });

                        XSSFWorkbook wk;
                        using (FileStream fs = File.Open(tbxTargetFile.Text, FileMode.Open,
                            FileAccess.Read, FileShare.ReadWrite))
                        {
                            wk = new XSSFWorkbook(fs);
                        }

                        var ws = wk.GetSheetAt(0);

                        foreach (DataRow targetRow in targetRows)
                        {

                            string supplierCode = targetRow["Column0"].ToString();
                            string partNo = targetRow["Column3"].ToString();
                            string supplierName = string.Empty;
                            WriteLog("Start searching Supplier Code:" + supplierCode + ", Part No:" + partNo + "...");
                            if (dicSUMapping.ContainsKey(supplierCode))
                            {
                                supplierName = dicSUMapping[supplierCode];
                                

                                var mactchedSRCRows = dtSource.AsEnumerable()
                                    .Where(p =>
                                    {

                                        if (string.IsNullOrWhiteSpace(p.Field<string>("Supplier")))
                                        {
                                            return false;
                                        }


                                        if (string.IsNullOrWhiteSpace(p.Field<string>("APN")))
                                        {
                                            return false;
                                        }

                                        return p.Field<string>("Supplier") == supplierName &&
                                               (p.Field<string>("APN").IndexOf('-') > 0
                                                   ? p.Field<string>("APN")
                                                       .Substring(0, p.Field<string>("APN").IndexOf('-'))
                                                   : p.Field<string>("APN")) ==
                                               partNo
                                               && p.Field<string>("Date") == "Demand";
                                    }).ToList();

                                if (mactchedSRCRows.Any())
                                {
                                    WriteLog("Supplier Code:" + supplierCode + ", Part No:" + partNo +
                                             ",Supplier Name:" + supplierName +
                                             " found in source, Start getting data...");
                                    var matchedRow = mactchedSRCRows[0];

                                    int factor = matchedRow["Unit"].ToString().ToUpper().Trim()== "KPC" ? 1000 : 1;
                                    int firstWorkDayColumn = -1;

                                    for (int colIndex = 14; colIndex < dtTarget.Columns.Count; colIndex++)
                                    {
                                        var date = dateList[colIndex - 14];

                                        //if (!string.IsNullOrWhiteSpace(date))
                                        //{
                                        //    DateTime dtCurrent = DateTime.ParseExact(date, "yyyyMMdd",
                                        //        System.Globalization.CultureInfo.CurrentCulture);
                                        //    if (firstWorkDayColumn == -1 && dtCurrent.DayOfWeek != DayOfWeek.Saturday &&
                                        //        dtCurrent.DayOfWeek != DayOfWeek.Sunday)
                                        //    {
                                        //        firstWorkDayColumn = colIndex;
                                        //    }

                                        //}

                                        if (dtSource.Columns.Contains(date))
                                        {
                                            targetRow[colIndex] = matchedRow[date];
                                            WriteToCell(ws, dtTarget.Rows.IndexOf(targetRow), colIndex,
                                                matchedRow[date].ToString(), factor);
                                        }
                                    }



                                    for (int colIndex = 17; colIndex < dtSource.Columns.Count; colIndex++)
                                    {
                                        var date = dtSource.Columns[colIndex].ColumnName.Trim();

                                        if (!string.IsNullOrWhiteSpace(date) && dateList.Contains(date))
                                        {
                                            DateTime dtCurrent = DateTime.ParseExact(date, "yyyyMMdd",
                                                System.Globalization.CultureInfo.CurrentCulture);
                                            if (firstWorkDayColumn == -1 && dtCurrent.DayOfWeek != DayOfWeek.Saturday &&
                                                dtCurrent.DayOfWeek != DayOfWeek.Sunday)
                                            {
                                                firstWorkDayColumn = dateList.IndexOf(date) + 14;
                                                break;
                                                
                                            }

                                        }

                                    }



                                    var strVMIStk = matchedRow[9].ToString();
                                    var dVMIStk = 0d;
                                    double.TryParse(strVMIStk, out dVMIStk);

                                    if (firstWorkDayColumn != -1)
                                    {

                                        var targetEDIVMIS = dtTarget.AsEnumerable()
                                            .Where(p => p.Field<string>("Column9") == "EDI VMI" && p.Field<string>("Column0") == supplierCode && p.Field<string>("Column3") == partNo).ToArray();
                                        if (targetEDIVMIS.Any())
                                        {
                                            WriteToCell(ws, dtTarget.Rows.IndexOf(targetEDIVMIS[0]), firstWorkDayColumn,
                                                dVMIStk.ToString(), factor);
                                        }
                                    }


                                    WriteLog("Supplier Code:" + supplierCode + ", Part No:" + partNo +
                                             ",Supplier Name:" + supplierName + " data transferred.");
                                }
                                else
                                {

                                    WriteLog("Supplier Code:" + supplierCode + ", Part No:" + partNo + " not found.");
                                }
                            }

                            else
                            {
                                WriteLog("Supplier code:" + supplierCode + " does not exist in mapping table.");
                            }


                            progressBar1.InvokeIfRequired(p => p.PerformStep());
                            lblProgress.InvokeIfRequired(p =>
                                p.Text = string.Format("{0}/{1}", progressBar1.Value, progressBar1.Maximum));
                        }

                        WriteLog("All data transferred, writing to target file.");
                        File.Copy(tbxTargetFile.Text, tbxTargetFile.Text + ".bak", true);
                        File.Delete(tbxTargetFile.Text);
                        using (FileStream fs = File.Open(tbxTargetFile.Text, FileMode.OpenOrCreate,
                            FileAccess.ReadWrite))
                        {
                            wk.Write(fs);
                        }
                    }
                    catch (Exception exception)
                    {
                        WriteLog(exception.Message);
                    }
                    finally
                    {
                        btnTransfer.InvokeIfRequired(p => { btnTransfer.Enabled = true; });
                    }
                });

        }

        public void WriteToCell(ISheet sheet, int row, int col, string value,int factor)
        {
            try
            {
                double qty =0;
                double.TryParse(value, out qty);
                if (sheet.GetRow(row) == null)
                {
                    sheet.CreateRow(row);
                }

                if (sheet.GetRow(row).GetCell(col) == null)
                {
                    sheet.GetRow(row).CreateCell(col);
                }
                sheet.GetRow(row).GetCell(col).SetCellValue(qty* factor);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void WriteLog(string logMessage)
        {
            this.tbxLog.InvokeIfRequired(p => p.Text = DateTime.Now + " :  " + logMessage + Environment.NewLine + p.Text);
        }

        private DataTable ReadToDataTable(string targetFile, bool UseHeaderRow)
        {
            using (var stream = new FileStream(targetFile, FileMode.Open))
            {
                using (var targetReader = ExcelDataReader.ExcelReaderFactory
                    .CreateReader(stream))
                {
                    return targetReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = UseHeaderRow
                        }
                    }).Tables[0];
                }
            }
        }


    }
}
