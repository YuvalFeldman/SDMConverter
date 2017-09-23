using SDM.DAL.FileSystemController;
using SDM.Models.Enums;
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

        public void ImportClientReport()
        {
            _fileSystemController.LogData(ReportTypes.ClientReport);
        }

        public void ImportcenturionReport()
        {
            _fileSystemController.LogData(ReportTypes.ClientReport);
        }

        public void ExportFullDebtReport()
        {
            var clientModels = _fileSystemController.ReadClientLogs();
            var centurionModels = _fileSystemController.ReadCenturionLogs();
            var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);
            _fileSystemController.WriteToFile(fullDeptReport);
        }

        public void ExportSummedDebtReport()
        {
            var clientModels = _fileSystemController.ReadClientLogs();
            var centurionModels = _fileSystemController.ReadCenturionLogs();
            var fullDeptReport = _reportRetriever.GetFullDebtReport(clientModels, centurionModels);

            var summedDeptReport = _reportRetriever.GetSummedDebtReport(fullDeptReport);

            _fileSystemController.WriteToFile(summedDeptReport);
        }

        public void DeleteClientReport()
        {
            _fileSystemController.DeleteReport(ReportTypes.ClientReport);
        }

        public void DeleteCenturionReport()
        {
            _fileSystemController.DeleteReport(ReportTypes.CenturionReport);
        }
    }
}