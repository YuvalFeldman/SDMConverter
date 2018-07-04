using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SDM.DAL.ReportsDal;
using SDM.Forms.ContentForms.ExportMenuForms;
using SDM.Forms.ContentForms.ImportForms;
using SDM.Utilities.Calculators.FullReportCalculator;
using SDM.Utilities.Calculators.IssuesReportCalculator;
using SDM.Utilities.Calculators.SummedReportCalculator;

namespace SDM.Forms
{
    public partial class SdmForm : Form
    {
        private readonly IFullReportCalculator _fullReportCalculator;
        private readonly ISummedReportCalculator _summedReportCalculator;
        private readonly IReportsDal _reportsDal;
        private readonly IIssuesReportCalculator _issuesReportCalculator;

        private readonly FullExportMenu _fullExportMenu;
        private readonly SummedExportMenu _summedExportMenu;
        private readonly ImportCenturionForm _importCenturionForm;
        private readonly ImportClientForm _importClientForm;
        private readonly ImportLatencyForm _importLatencyForm;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        private readonly BindingSource _exportOptionsBindingSource = new BindingSource();
        private readonly BindingSource _importOptionsBindingSource = new BindingSource();

        private List<string> _fullReport = new List<string>();
        private Dictionary<string, List<string>> _summedReport = new Dictionary<string, List<string>>();
        private List<string> _report = new List<string>();
        private List<string> _issues = new List<string>();

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public SdmForm(FullExportMenu fullExportMenu, 
            SummedExportMenu summedExportMenu, 
            ImportCenturionForm importCenturionForm,
            ImportClientForm importClientForm, 
            ImportLatencyForm importLatencyForm, IFullReportCalculator fullReportCalculator, ISummedReportCalculator summedReportCalculator, IReportsDal reportsDal, IIssuesReportCalculator issuesReportCalculator)
        {
            _fullExportMenu = fullExportMenu;
            _summedExportMenu = summedExportMenu;
            _importCenturionForm = importCenturionForm;
            _importClientForm = importClientForm;
            _importLatencyForm = importLatencyForm;
            _fullReportCalculator = fullReportCalculator;
            _summedReportCalculator = summedReportCalculator;
            _reportsDal = reportsDal;
            _issuesReportCalculator = issuesReportCalculator;

            InitializeComponent();

            ExportMenuPanel.SuspendLayout();
            ImportPanel.SuspendLayout();

            _exportOptionsBindingSource.DataSource = new List<string> {"Full Report", "Summed Report"};
            ExportTypeComboBox.DataSource = _exportOptionsBindingSource;

            _importOptionsBindingSource.DataSource = new List<string> {"Centurion Report", "Client Report", "Latency Conversion"};
            ImportTypeComboBox.DataSource = _importOptionsBindingSource;

            ExportMenuPanel.Controls.Add(_fullExportMenu);
            ExportMenuPanel.Controls.Add(_summedExportMenu);

            ImportPanel.Controls.Add(_importCenturionForm);
            ImportPanel.Controls.Add(_importClientForm);
            ImportPanel.Controls.Add(_importLatencyForm);

            UpdateImportPanel();
            UpdateExportMenuForm();

            _summedExportMenu.summedTablesComboBox.SelectedIndexChanged += this.ChangedChosenSummedReport;
            _importCenturionForm.updateUsagesButton.Click += (s, e) =>
            {
                GetUpdatedReport();
                UpdateExcelContentPanel();
            };
            _importClientForm.updateUsagesButton.Click += (s, e) =>
            {
                GetUpdatedReport();
                UpdateExcelContentPanel();
            };
            _importLatencyForm.updateUsagesButton.Click += (s, e) =>
            {
                GetUpdatedReport();
                UpdateExcelContentPanel();
            };
            _fullExportMenu.ExportFullReportButton.Click += this.ExportSinlgeReport;
            _summedExportMenu.exportSummedReportButton.Click += this.ExportSinlgeReport;
            _summedExportMenu.exportAllReports.Click += this.ExportAllSummedReports;

            ExcelContentPanel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            _fullExportMenu.ExportIssuesButton.Click += (s, e) => ExportIssuesReport();
            _summedExportMenu.ExportIssuesButton.Click += (s, e) => ExportIssuesReport();
        }

        private void TopBarMenu_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MouseEnterMinimizeButton(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = System.Drawing.Color.DarkOrange;
        }

        private void MouseLeaveMinimizeButton(object sender, EventArgs e)
        {
            MinimizeButton.ForeColor = System.Drawing.Color.GhostWhite;
        }

        private void MouseEnterCloseButton(object sender, EventArgs e)
        {
            closeButton.ForeColor = System.Drawing.Color.DarkOrange;
        }

        private void MouseLeaveCloseButton(object sender, EventArgs e)
        {
            closeButton.ForeColor = System.Drawing.Color.GhostWhite;
        }

        private void ExportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateExportMenuForm();
        }

        private void UpdateExportMenuForm()
        {
            if (ExportTypeComboBox.SelectedIndex.Equals(0))
            {
                _fullExportMenu.Show();
                _summedExportMenu.Hide();
            }
            else
            {
                _fullExportMenu.Hide();
                _summedExportMenu.Show();
            }

            GetUpdatedReport();
            UpdateExcelContentPanel();
        }

        private void ImportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateImportPanel();
        }

        private void UpdateImportPanel()
        {
            if (ImportTypeComboBox.SelectedIndex.Equals(0))
            {
                _importCenturionForm.Show();
                _importClientForm.Hide();
                _importLatencyForm.Hide();
            }
            else if (ImportTypeComboBox.SelectedIndex.Equals(1))
            {
                _importCenturionForm.Hide();
                _importClientForm.Show();
                _importLatencyForm.Hide();
            }
            else
            {
                _importCenturionForm.Hide();
                _importClientForm.Hide();
                _importLatencyForm.Show();
            }
        }

        private void UpdateExcelContentPanel()
        {
            ExcelContentPanel.Columns.Clear();
            ExcelContentPanel.Rows.Clear();
            if (_report == null || !_report.Any())
            {
                return;
            }
            var numberOfRows = _report.Count;
            var firstRow = _report[0].Split(',');
            var numberOfColumns = firstRow.Length;
            ExcelContentPanel.ColumnCount = numberOfColumns;
            for (var i = 0; i < numberOfColumns; i++)
            {
                ExcelContentPanel.Columns[i].Name = firstRow[i];
            }
            for (var j = 1; j < numberOfRows; j++)
            {
                ExcelContentPanel.Rows.Add(_report[j].Split(',').ToArray());
                ExcelContentPanel.Rows[j - 1].HeaderCell.Value = (j - 1).ToString();
            }
        }

        private void GetUpdatedReport()
        {
            var centurionLogNames = _importCenturionForm.GetSelectedCenturionLogs();
            var clientLogNames = _importClientForm.GetSelectedClientLogs();
            var latencyConversionTableName = _importLatencyForm.GetSelectedLatencyConversionTables();

            if (ExportTypeComboBox.SelectedIndex.Equals(0))
            {
                var fullReportAndIssues = _fullReportCalculator.GetFullReport(centurionLogNames, clientLogNames, latencyConversionTableName);
                _fullReport = fullReportAndIssues.Item1;
                var additionalIssues = fullReportAndIssues.Item2;
                _report = _fullReport;
                _issues = _issuesReportCalculator.GetReportIssues(clientLogNames, additionalIssues, latencyConversionTableName);
            }
            else
            {
                var summedReportAndIssues = _summedReportCalculator.GetSummedReport(centurionLogNames, clientLogNames, latencyConversionTableName);
                _summedReport = summedReportAndIssues.Item1;
                var additionalIssues = summedReportAndIssues.Item2;
                _report = _summedReport.Any() ? _summedReport[_summedReport.Keys.First()] : new List<string>();
                _summedExportMenu.UpdateSummedComboBoxOptions(_summedReport.Keys.ToList());
                _issues = _issuesReportCalculator.GetReportIssues(clientLogNames, additionalIssues, latencyConversionTableName);
            }
        }

        private void ChangedChosenSummedReport(object sender, EventArgs e)
        {
            var summedReportsSelectedIndex = _summedExportMenu.summedTablesComboBox.SelectedIndex;
            if (summedReportsSelectedIndex < 0 || summedReportsSelectedIndex > _summedExportMenu.SummedTables.Count)
            {
                return;
            }
            _report = _summedReport[_summedExportMenu.SummedTables[summedReportsSelectedIndex]];
            UpdateExcelContentPanel();
        }

        private void ExportSinlgeReport(object sender, EventArgs e)
        {
            _reportsDal.ExportReport(_report);
        }

        private void ExportAllSummedReports(object sender, EventArgs e)
        {
            _reportsDal.ExportReports(_summedReport);
        }

        private void ExportIssuesReport()
        {
            GetUpdatedReport();
            _reportsDal.ExportIssuesReport(_issues);
        }
    }
}