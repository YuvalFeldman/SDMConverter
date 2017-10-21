using System;
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
        private LatencyConversionModel _latencyConversionModel = new LatencyConversionModel();

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

                MessageBox.Show("Client report import completed successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
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

                MessageBox.Show("Centurion report import completed successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue importing Centurion report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportFullDebtReport()
        {
            try
            {
                var clientModels = _fileSystemController.ReadClientLogs(_latencyConversionModel);
                var centurionModels = _fileSystemController.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);
                _fileSystemController.WriteToFile(fullDeptReport);

                MessageBox.Show("Full dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue exporting full dept report : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportSummedDebtReport()
        {
            try
            {
                var clientModels = _fileSystemController.ReadClientLogs(_latencyConversionModel);
                var centurionModels = _fileSystemController.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);

                var summedDeptReport = _reportRetriever.GetSummedDebtReport(fullDeptReport);

                _fileSystemController.WriteToFile(summedDeptReport);

                MessageBox.Show("Summed dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
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

                MessageBox.Show("Client report deleted successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
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

                MessageBox.Show("Centurion report deleted successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
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
                _latencyConversionModel = _fileSystemController.ReadLatencyConversionTable();

                MessageBox.Show("Latency conversion table imported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue importing latency conversion table : {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}