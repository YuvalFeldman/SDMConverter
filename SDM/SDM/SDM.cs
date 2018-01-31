using System;
using System.Windows.Forms;
using SDM.DAL.ReportsDal;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Utilities.ReportRetriever;

namespace SDM.SDM
{
    public class SDM : ISDM
    {
        private readonly IReportsDal _reportsDal;
        private readonly IReportRetriever _reportRetriever;

        public SDM(IReportsDal reportsDal, IReportRetriever reportRetriever)
        {
            _reportsDal = reportsDal;
            _reportRetriever = reportRetriever;
        }

        public void ImportClientReport(string clientId)
        {
            try
            {
                _reportsDal.LogData(ReportTypes.ClientReport, clientId);
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
                _reportsDal.LogData(ReportTypes.CenturionReport);
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
                var latencyConversionModel = useConversionTable ? _reportsDal.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _reportsDal.ReadClientLogs(latencyConversionModel);
                var centurionModels = _reportsDal.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);
                return;
                //_reportsDal.WriteToFile(fullDeptReport);
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
                var latencyConversionModel = useConversionTable ? _reportsDal.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _reportsDal.ReadClientLogs(latencyConversionModel);
                var centurionModels = _reportsDal.ReadCenturionLogs();
                var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);

                var summedDeptReport = _reportRetriever.GetSummedDebtReport(fullDeptReport);

                _reportsDal.WriteToFile(summedDeptReport);
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
                _reportsDal.DeleteReport(ReportTypes.ClientReport);
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
                _reportsDal.DeleteReport(ReportTypes.CenturionReport);
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
                _reportsDal.ImportLatencyConversionTable();
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
                var latencyConversionModel = useConversionTable ? _reportsDal.ReadLatencyConversionTable() : new LatencyConversionModel();
                var clientModels = _reportsDal.ReadClientLogs(latencyConversionModel);
                var centurionModels = _reportsDal.ReadCenturionLogs();
                _reportRetriever.GetInvoiceNumberIssues(clientModels, centurionModels);
            }
            catch (Exception e)
            {
                MessageBox.Show($"Encountered an issue getting invoice number issues: {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}