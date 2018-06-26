using System.Collections.Generic;
using System.IO;
using System.Linq;
using SDM.DAL.FileSystemController;

namespace SDM.DAL.ReportsDal
{
    public class ReportsDal : IReportsDal
    {
        private readonly IFileSystemController _fileSystemController;

        public ReportsDal(IFileSystemController fileSystemController)
        {
            _fileSystemController = fileSystemController;
        }

        public void ExportReport(List<string> report, List<string> issues)
        {
            if (report == null || !report.Any())
            {
                return;
            }
            
            var reportDestination = _fileSystemController.GetSaveDialogFilePath();
            if (!string.IsNullOrEmpty(reportDestination))
            {
                _fileSystemController.WriteDataToFile(reportDestination, report);
                _fileSystemController.WriteDataToFile($"{Path.GetDirectoryName(reportDestination)}\\Issues.txt", issues);
            }
        }

        public void ExportReports(Dictionary<string, List<string>> reports, List<string> issues)
        {
            if (reports == null || !reports.Any())
            {
                return;
            }
            
            var reportDestination = _fileSystemController.GetDirectoryPath();
            if (string.IsNullOrEmpty(reportDestination))
            {
                return;
            }

            foreach (var report in reports)
            {
                if (string.IsNullOrEmpty(report.Key) || report.Value == null || !report.Value.Any())
                {
                    continue;
                }

                _fileSystemController.WriteDataToFile($"{reportDestination}\\{report.Key}.csv", report.Value);
            }

            _fileSystemController.WriteDataToFile($"{reportDestination}\\Issues.txt", issues);
        }
    }
}