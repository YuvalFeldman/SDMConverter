using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.FileSystemController;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;
using SDM.Utilities.DataConverter;

namespace SDM.DAL.logsDal
{
    public class SdmlogsDal : ISdmlogsDal
    {
        private readonly IFileSystemController _fileSystemController;
        private readonly IDataConverter _dataConverter;
        private const string LogsFolder = ".\\ImportLogs";

        public SdmlogsDal(IFileSystemController fileSystemController, IDataConverter dataConverter)
        {
            _fileSystemController = fileSystemController;
            _dataConverter = dataConverter;
        }

        public List<string> GetLogNames(ReportTypes reportType)
        {
            try
            {
                var logNames = _fileSystemController
                    .GetAllFileNamesInDirectory($"{LogsFolder}\\{reportType}")
                    .Select(Path.GetFileNameWithoutExtension)
                    .ToList();
                return logNames;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed getting log names, report type: {reportType}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public string ImportReport(ReportTypes reportType)
        {
            try
            {
                var logFilePath = _fileSystemController.GetOpenDialogFilePath();
                if (!string.IsNullOrEmpty(logFilePath))
                {
                    _fileSystemController.CopyFile(logFilePath, $"{LogsFolder}\\{reportType}\\{Path.GetFileName(logFilePath)}");
                    return Path.GetFileNameWithoutExtension(logFilePath);
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed importing report, report type: {reportType}. InnerMessage: {e.Message}");
                return string.Empty;
            }
        }

        public void DeleteReport(ReportTypes reportType, string reportName)
        {
            try
            {
                _fileSystemController.DeleteFile($"{LogsFolder}\\{reportType}\\{reportName}.csv");
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed deleting report, report name: {reportName}, report type: {reportType}. InnerMessage: {e.Message}");
            }
        }

        public Tuple<CenturionLog, List<string>>  GetCenturionLog(string reportName)
        {
            try
            {
                var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.CenturionReport}\\{reportName}.csv");
                var centurionLog = _dataConverter.ConvertCsvToCenturionModel(logContent);
                return centurionLog;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed getting centurion log: {reportName}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public Tuple<ClientLog, List<string>> GetClientLog(string reportName, LatencyConversionModel conversionModel)
        {
            try
            {
                var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.ClientReport}\\{reportName}.csv");
                var clientLog = _dataConverter.ConvertCsvToClientDataModel(logContent, conversionModel);
                return clientLog;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed getting client log: {reportName}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public LatencyConversionModel GetReportLatencyLog(string reportName)
        {
            try
            {
                if (string.IsNullOrEmpty(reportName))
                {
                    return new LatencyConversionModel();
                }
                var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.LatencyConversionTable}\\{reportName}.csv");
                var latencyConversionTable = _dataConverter.ConvertCsvToLatencyConversionModel(logContent);
                return latencyConversionTable;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed latency report: {reportName}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public void AddClientIdToClientReport(string reportName, string id)
        {
            _fileSystemController.AppendClientId($"{LogsFolder}\\{ReportTypes.ClientReport}\\{reportName}.csv", id);
        }
    }
}