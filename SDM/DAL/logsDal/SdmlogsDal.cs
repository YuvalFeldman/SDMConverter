using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var logNames = _fileSystemController
                .GetAllFileNamesInDirectory($"{LogsFolder}\\{reportType}")
                .Select(Path.GetFileNameWithoutExtension)
                .ToList();
            return logNames;
        }

        public string ImportReport(ReportTypes reportType)
        {
            var logFilePath = _fileSystemController.GetOpenDialogFilePath();
            if (!string.IsNullOrEmpty(logFilePath))
            {
                _fileSystemController.CopyFile(logFilePath, $"{LogsFolder}\\{reportType}\\{Path.GetFileName(logFilePath)}");
                return Path.GetFileNameWithoutExtension(logFilePath);
            }

            return string.Empty;
        }

        public void DeleteReport(ReportTypes reportType, string reportName)
        {
            _fileSystemController.DeleteFile($"{LogsFolder}\\{reportType}\\{reportName}.csv");
        }

        public CenturionLog GetCenturionLog(string reportName)
        {
            var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.CenturionReport}\\{reportName}.csv");
            var centurionLog = _dataConverter.ConvertCsvToCenturionModel(logContent);
            return centurionLog;
        }

        public ClientLog GetClientLog(string reportName, LatencyConversionModel conversionModel)
        {
            var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.ClientReport}\\{reportName}.csv");
            var clientLog = _dataConverter.ConvertCsvToClientDataModel(logContent, conversionModel);
            return clientLog;
        }

        public LatencyConversionModel GetReportLatencyLog(string reportName)
        {
            if (string.IsNullOrEmpty(reportName))
            {
                return new LatencyConversionModel();
            }
            var logContent = _fileSystemController.ReadFileContents($"{LogsFolder}\\{ReportTypes.LatencyConversionTable}\\{reportName}.csv");
            var latencyConversionTable = _dataConverter.ConvertCsvToLatencyConversionModel(logContent);
            return latencyConversionTable;
        }

        public void AddClientIdToClientReport(string reportName, string id)
        {
            _fileSystemController.AppendClientId($"{LogsFolder}\\{ReportTypes.ClientReport}\\{reportName}.csv", id);
        }

        //public List<ClientLog> ReadClientLogs(LatencyConversionModel latencyConversionModel)
        //{
        //    return _fileSystemController.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.ClientReport}")
        //        .Select(filePath => _fileSystemController.ReadFileContents($".\\ImportLogs\\{ReportTypes.ClientReport}\\{filePath}"))
        //        .Select(fileContent => _dataConverter.ConvertCsvToClientDataModel(fileContent, latencyConversionModel))
        //        .ToList();
        //}

        //public List<CenturionLog> ReadCenturionLogs()
        //{
        //    return _fileSystemController.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.CenturionReport}")
        //        .Where(path => path != null)
        //        .Select(filePath => _fileSystemController.ReadFileContents($".\\ImportLogs\\{ReportTypes.CenturionReport}\\{filePath}"))
        //        .Select(fileContent => _dataConverter.ConvertCsvToCenturionModel(fileContent))
        //        .ToList();
        //}

        //public void WriteToFile(List<string> data)
        //{
        //    var path = _fileSystemController.GetSaveDialogFilePath();
        //    if (path == null)
        //    {
        //        return;
        //    }

        //    _fileSystemController.WriteDataToFile(path, data);
        //}

        //public void WriteToFile(FullDatabaseModel data)
        //{
        //    var path = _fileSystemController.GetSaveDialogFilePath();
        //    if (path == null)
        //    {
        //        return;
        //    }

        //    var fullDatabaseCsv = _dataConverter.ConvertToCsv(data);
        //    _fileSystemController.WriteDataToFile(path, fullDatabaseCsv);

        //    MessageBox.Show("Full dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        //}

        //public void WriteToFile(SummedDatabaseModel data)
        //{
        //    var path = _fileSystemController.GetDirectoryPath();
        //    if (path == null)
        //    {
        //        return;
        //    }

        //    var summedDatabaseCsv = _dataConverter.ConvertToCsv(data);
        //    foreach (var partnerCsv in summedDatabaseCsv)
        //    {
        //        _fileSystemController.WriteDataToFile($"{path}\\{partnerCsv.Key}.csv", partnerCsv.Value);
        //    }

        //    MessageBox.Show("Summed dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        //}

        //public void LogData(ReportTypes reportType, string clientId = null)
        //{
        //    var path = _fileSystemController.GetOpenDialogFilePath();
        //    if (path == null)
        //    {
        //        return;
        //    }
        //    var logFolder = $".\\ImportLogs\\{reportType}";
        //    var reportName = Path.GetFileName(path);
        //    var newFilePath = $"{logFolder}\\{reportName}";

        //    if (File.Exists(newFilePath))
        //    {
        //        _fileSystemController.DeleteFile(newFilePath);
        //    }

        //    _fileSystemController.CreateDirectory(logFolder);
        //    _fileSystemController.CopyFile(path, newFilePath);

        //    if (!string.IsNullOrEmpty(clientId))
        //    {
        //        var fileContent = _fileSystemController.ReadFileContents(newFilePath);
        //        if (fileContent == null || !fileContent.Any())
        //        {
        //            return;
        //        }
        //        fileContent[0] = $"{fileContent[0]}, client id";
        //        for (var i = 1; i < fileContent.Count; i++)
        //        {
        //            fileContent[i] = $"{fileContent[i]}, {clientId}";
        //        }

        //        File.WriteAllLines(newFilePath, fileContent, Encoding.GetEncoding("windows-1255"));
        //    }
        //    MessageBox.Show(
        //        reportType == ReportTypes.ClientReport
        //            ? "Client report import completed successfully"
        //            : "Centurion report import completed successfully", "Reports manager", MessageBoxButtons.OK,
        //        MessageBoxIcon.None);
        //}

        //public void DeleteReport(ReportTypes reportType)
        //{
        //    var filePath = _fileSystemController.GetOpenDialogFilePath($".\\ImportLogs\\{reportType}\\");
        //    var deleteResult = _fileSystemController.DeleteFile(filePath);

        //    if (deleteResult)
        //    {
        //        MessageBox.Show("Report deleted successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        //    }
        //}

        //public void ImportLatencyConversionTable()
        //{
        //    if (!Directory.Exists($".\\ImportLogs\\LatencyConversionTable"))
        //    {
        //        Directory.CreateDirectory($".\\ImportLogs\\LatencyConversionTable");
        //    }
        //    if (!File.Exists($".\\ImportLogs\\LatencyConversionTable\\latencyConversionTable.csv"))
        //    {
        //        var fileState = File.Create($".\\ImportLogs\\LatencyConversionTable\\latencyConversionTable.csv");
        //        fileState.Close();
        //    }
        //    var path = _fileSystemController.GetOpenDialogFilePath();
        //    File.Copy(path, ".\\ImportLogs\\LatencyConversionTable\\latencyConversionTable.csv", true);

        //    MessageBox.Show("Latency conversion table imported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        //}

        //public LatencyConversionModel ReadLatencyConversionTable()
        //{
        //    var latencyConversionTableContent = _fileSystemController.ReadFileContents($".\\ImportLogs\\LatencyConversionTable\\latencyConversionTable.csv");
        //    if (latencyConversionTableContent == null)
        //    {
        //        return new LatencyConversionModel();
        //    }

        //    var convertedLatencyTable = _dataConverter.ConvertCsvToLatencyConversionModel(latencyConversionTableContent);
        //    return convertedLatencyTable;
        //}
    }
}