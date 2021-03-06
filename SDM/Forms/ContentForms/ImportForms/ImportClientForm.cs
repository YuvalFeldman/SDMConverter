﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.logsDal;
using SDM.Models.Enums;

namespace SDM.Forms.ContentForms.ImportForms
{
    public partial class ImportClientForm : Form
    {
        private readonly ISdmlogsDal _logsDal;
        private List<CheckBox> _clientLogs = new List<CheckBox>();

        public ImportClientForm(ISdmlogsDal logsDal)
        {
            _logsDal = logsDal;
            this.TopLevel = false;
            InitializeComponent();
            UpdateFilesContentPanel();
        }

        private void ImportClientReportButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ClientIdTextBox.Text))
            {
                MessageBox.Show(@"Client id value cannot be empty");
                return;
            }
            var reportName = _logsDal.ImportReport(ReportTypes.ClientReport);
            if (!string.IsNullOrEmpty(reportName))
            {
                _logsDal.AddClientIdToClientReport(reportName, ClientIdTextBox.Text);
                UpdateFilesContentPanel();
            }
        }

        private void deleteSelectedButton_Click(object sender, EventArgs e)
        {
            var logsToDelete = _clientLogs
                .Where(log => log.Checked)
                .Select(log => log.Text)
                .ToList();

            if (!logsToDelete.Any())
            {
                return;
            }

            foreach (var log in logsToDelete)
            {
                _logsDal.DeleteReport(ReportTypes.ClientReport, log);
            }
            UpdateFilesContentPanel();
        }
        private void UpdateFilesContentPanel()
        {
            var clientLogNames = _logsDal.GetLogNames(ReportTypes.ClientReport);

            FilesContentPanel.Controls.Clear();

            foreach (var clientLog in clientLogNames)
            {
                var clientCheckBox = new CheckBox { Text = clientLog, Checked = true};
                clientCheckBox.Show();
                FilesContentPanel.Controls.Add(clientCheckBox);
                _clientLogs.Add(clientCheckBox);
            }
        }

        public List<string> GetSelectedClientLogs()
        {
            var selectedClientLogs = _clientLogs
                .Where(x => x.Checked)
                .Select(x => x.Text)
                .ToList();

            return selectedClientLogs;
        }
    }
}
