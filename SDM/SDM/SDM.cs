﻿using System;
using System.Windows.Forms;
using SDM.DAL.FileSystemController;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Utilities.ReportRetriever;

namespace SDM.SDM
{
    public class SDM : ISDM
    {
        private readonly IFileSystemController _fileSystemController;
        private readonly IReportRetriever _reportRetriever;

        public SDM(IFileSystemController fileSystemController, IReportRetriever reportRetriever)
        {
            _fileSystemController = fileSystemController;
            _reportRetriever = reportRetriever;
        }

        public void ImportClientReport(string clientId)
        {
            try
            {
                _fileSystemController.LogData(ReportTypes.ClientReport, clientId);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue importing client report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void ImportcenturionReport()
        {
            try
            {
                _fileSystemController.LogData(ReportTypes.CenturionReport);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue importing Centurion report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportFullDebtReport(bool useConversionTable)
        {
            try
            {
                var latencyConversionModel = useConversionTable ? _fileSystemController.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _fileSystemController.ReadClientLogs(latencyConversionModel);
                var centurionModels = _fileSystemController.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);
                _fileSystemController.WriteToFile(fullDeptReport);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue exporting full dept report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportSummedDebtReport(bool useConversionTable)
        {
            try
            {
                var latencyConversionModel = useConversionTable ? _fileSystemController.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _fileSystemController.ReadClientLogs(latencyConversionModel);
                var centurionModels = _fileSystemController.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);

                var summedDeptReport = _reportRetriever.GetSummedDebtReport(fullDeptReport);

                _fileSystemController.WriteToFile(summedDeptReport);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue exporting summed dept report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteClientReport()
        {
            try
            {
                _fileSystemController.DeleteReport(ReportTypes.ClientReport);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue deleting client report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteCenturionReport()
        {
            try
            {
                _fileSystemController.DeleteReport(ReportTypes.CenturionReport);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue deleting Centurion report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetLatencyConversionTable()
        {
            try
            {
                _fileSystemController.ImportLatencyConversionTable();
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue importing latency conversion table : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GetReportInvoiceIdIssues(bool useConversionTable)
        {
            try
            {
                var latencyConversionModel = useConversionTable ? _fileSystemController.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _fileSystemController.ReadClientLogs(latencyConversionModel);
                var centurionModels = _fileSystemController.ReadCenturionLogs();
                _reportRetriever.GetInvoiceNumberIssues(clientModels, centurionModels);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue getting invoice number issues: {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}