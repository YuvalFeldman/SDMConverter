using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.logsDal;
using SDM.Models.Enums;

namespace SDM.Forms.ContentForms.ImportForms
{
    public partial class ImportLatencyForm : Form
    {
        private readonly ISdmlogsDal _logsDal;
        private List<RadioButton> _latencyTables = new List<RadioButton>();

        public ImportLatencyForm(ISdmlogsDal logsDal)
        {
            _logsDal = logsDal;
            this.TopLevel = false;
            InitializeComponent();
            UpdateFilesContentPanel();
        }

        private void ImportLatencyConversionTableButton_Click(object sender, EventArgs e)
        {
            var tableName = _logsDal.ImportReport(ReportTypes.LatencyConversionTable);
            if (!string.IsNullOrEmpty(tableName))
            {
                UpdateFilesContentPanel();
            }
        }

        private void updateUsagesButton_Click(object sender, EventArgs e)
        {

        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            var logsToDelete = _latencyTables
                .Where(log => log.Checked)
                .Select(log => log.Text)
                .ToList();

            if (!logsToDelete.Any())
            {
                return;
            }

            foreach (var log in logsToDelete)
            {
                _logsDal.DeleteReport(ReportTypes.LatencyConversionTable, log);
            }
            UpdateFilesContentPanel();
        }
        private void UpdateFilesContentPanel()
        {
            var latencyTableNames = _logsDal.GetLogNames(ReportTypes.LatencyConversionTable);

            FilesContentPanel.Controls.Clear();

            foreach (var latencyTable in latencyTableNames)
            {
                var latencyRadioButton = new RadioButton { Text = latencyTable };
                latencyRadioButton.Show();
                FilesContentPanel.Controls.Add(latencyRadioButton);
                _latencyTables.Add(latencyRadioButton);
            }
        }
    }
}
