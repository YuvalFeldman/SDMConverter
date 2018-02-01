using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.logsDal;
using SDM.Models.Enums;

namespace SDM.Forms.ContentForms.ImportForms
{
    public partial class ImportCenturionForm : Form
    {
        private readonly ISdmlogsDal _logsDal;
        private List<CheckBox> _centurionLogs = new List<CheckBox>();
        public ImportCenturionForm(ISdmlogsDal logsDal)
        {
            _logsDal = logsDal;
            this.TopLevel = false;
            InitializeComponent();
            UpdateFilesContentPanel();
        }

        private void ImportCenturionButton_Click(object sender, EventArgs e)
        {
            var logName = _logsDal.ImportReport(ReportTypes.CenturionReport);
            if (!string.IsNullOrEmpty(logName))
            {
                UpdateFilesContentPanel();
            }
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            var logsToDelete = _centurionLogs
                .Where(log => log.Checked)
                .Select(log =>log.Text)
                .ToList();

            if (!logsToDelete.Any())
            {
                return;
            }

            foreach (var log in logsToDelete)
            {
                _logsDal.DeleteReport(ReportTypes.CenturionReport, log);
            }

            UpdateFilesContentPanel();
        }

        private void UpdateFilesContentPanel()
        {
            var centurionLogNames = _logsDal.GetLogNames(ReportTypes.CenturionReport);

            FilesContentPanel.Controls.Clear();

            foreach (var centurionLog in centurionLogNames)
            {
                var centurionCheckBox = new CheckBox{Text = centurionLog, Checked = true };
                centurionCheckBox.Show();
                FilesContentPanel.Controls.Add(centurionCheckBox);
                _centurionLogs.Add(centurionCheckBox);
            }
        }

        public List<string> GetSelectedCenturionLogs()
        {
            var selectedClientLogs = _centurionLogs
                .Where(x => x.Checked)
                .Select(x => x.Text)
                .ToList();

            return selectedClientLogs;
        }
    }
}
