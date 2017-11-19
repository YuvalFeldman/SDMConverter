using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SDM.DAL.FileWizard;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;
using SDM.Utilities.DataConverter;

namespace SDM.DAL.FileSystemController
{
    public class FileSystemController : IFileSystemController
    {
        private readonly IFileWizard _fileWizard;
        private readonly IDataConverter _dataConverter;

        public FileSystemController(IFileWizard fileWizard, IDataConverter dataConverter)
        {
            _fileWizard = fileWizard;
            _dataConverter = dataConverter;
        }

        public List<ClientReportModel> ReadClientLogs(LatencyConversionModel latencyConversionModel)
        {
            return _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.ClientReport}")
                .Select(filePath => _fileWizard.ReadFileContents($".\\ImportLogs\\{ReportTypes.ClientReport}\\{filePath}"))
                .Select(fileContent => _dataConverter.ConvertCsvToClientDataModel(fileContent, latencyConversionModel))
                .ToList();
        }

        public List<CenturionReportModel> ReadCenturionLogs()
        {
            return _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.CenturionReport}")
                .Where(path => path != null)
                .Select(filePath => _fileWizard.ReadFileContents($".\\ImportLogs\\{ReportTypes.CenturionReport}\\{filePath}"))
                .Select(fileContent => _dataConverter.ConvertCsvToCenturionModel(fileContent))
                .ToList();
        }

        public void WriteToFile(List<string> data)
        {
            var path = _fileWizard.GetSaveDialogFilePath();
            if (path == null)
            {
                return;
            }

            _fileWizard.WriteDataToFile(path, data);
        }

        public void WriteToFile(FullDatabaseModel data)
        {
            var path = _fileWizard.GetSaveDialogFilePath();
            if (path == null)
            {
                return;
            }

            var fullDatabaseCsv = _dataConverter.ConvertToCsv(data);
            _fileWizard.WriteDataToFile(path, fullDatabaseCsv);

            MessageBox.Show("Full dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public void WriteToFile(SummedDatabaseModel data)
        {
            var path = _fileWizard.GetDirectoryPath();
            if (path == null)
            {
                return;
            }

            var summedDatabaseCsv = _dataConverter.ConvertToCsv(data);
            foreach (var partnerCsv in summedDatabaseCsv)
            {
                _fileWizard.WriteDataToFile($"{path}\\{partnerCsv.Key}.csv", partnerCsv.Value);
            }

            MessageBox.Show("Summed dept report exported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        public void LogData(ReportTypes reportType, string clientId = null)
        {
            var path = _fileWizard.GetOpenDialogFilePath();
            if (path == null)
            {
                return;
            }
            var logFolder = $".\\ImportLogs\\{reportType}";
            var reportName = Path.GetFileName(path);
            var newFilePath = $"{logFolder}\\{reportName}";

            if (File.Exists(newFilePath))
            {
                _fileWizard.DeleteFile(newFilePath);
            }

            _fileWizard.CreateDirectory(logFolder);
            _fileWizard.CopyFileToReportLogFolder(path, newFilePath);

            if (!string.IsNullOrEmpty(clientId))
            {
                var fileContent = _fileWizard.ReadFileContents(newFilePath);
                if (fileContent == null || !fileContent.Any())
                {
                    return;
                }
                fileContent[0] = $"{fileContent[0]}, client id";
                for (var i = 1; i < fileContent.Count; i++)
                {
                    fileContent[i] = $"{fileContent[i]}, {clientId}";
                }

                File.WriteAllLines(newFilePath, fileContent, Encoding.GetEncoding("windows-1255"));
            }
            MessageBox.Show(
                reportType == ReportTypes.ClientReport
                    ? "Client report import completed successfully"
                    : "Centurion report import completed successfully", "Reports manager", MessageBoxButtons.OK,
                MessageBoxIcon.None);
        }

        public void DeleteReport(ReportTypes reportType)
        {
            var filePath = _fileWizard.GetOpenDialogFilePath($".\\ImportLogs\\{reportType}\\");
            var deleteResult = _fileWizard.DeleteFile(filePath);

            if (deleteResult)
            {
                MessageBox.Show("Report deleted successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        public LatencyConversionModel ReadLatencyConversionTable()
        {
            var path = _fileWizard.GetOpenDialogFilePath($".\\ImportLogs\\LatencyConversionTable");
            if (path == null)
            {
                return new LatencyConversionModel();
            }

            var latencyConversionTableContent = _fileWizard.ReadFileContents(path);
            var convertedLatencyTable = _dataConverter.ConvertCsvToLatencyConversionModel(latencyConversionTableContent);

            MessageBox.Show("Latency conversion table imported successfully", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.None);

            return convertedLatencyTable;
        }
    }
}