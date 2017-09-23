using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SDM.DAL.FileWizard;
using SDM.Models.Enums;
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

        public List<ClientReportModel> ReadClientLogs()
        {
            return _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.ClientReport}")
                .Select(filePath => _fileWizard.ReadFileContents(filePath))
                .Select(fileContent => _dataConverter.ConvertCsvToClientDataModel(fileContent))
                .ToList();
        }

        public List<CenturionReportModel> ReadCenturionLogs()
        {
            return _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ReportTypes.CenturionReport}")
                .Select(filePath => _fileWizard.ReadFileContents(filePath))
                .Select(fileContent => _dataConverter.ConvertCsvToCenturionModel(fileContent))
                .ToList();
        }

        public void WriteToFile(FullDatabaseModel data)
        {
            var path = _fileWizard.GetSaveDialogFilePath();

            var fullDatabaseCsv = _dataConverter.ConvertToCsv(data);
            _fileWizard.WriteDataToFile(path, fullDatabaseCsv);
        }

        public void WriteToFile(SummedDatabaseModel data)
        {
            var path = _fileWizard.GetDirectoryPath();

            var summedDatabaseCsv = _dataConverter.ConvertToCsv(data);
            foreach (var partnerCsv in summedDatabaseCsv)
            {
                _fileWizard.WriteDataToFile($"{path}\\{partnerCsv.Key}", partnerCsv.Value);
            }
        }

        public void LogData(ReportTypes reportType)
        {
            var path = _fileWizard.GetSaveDialogFilePath();

            var logFolder = $".\\ImportLogs\\{reportType}";
            var reportName = Path.GetFileName(path);
            var newFilePath = $"{logFolder}\\{reportName}";

            if (File.Exists(newFilePath))
            {
                throw new Exception($"Report with the name {Path.GetFileNameWithoutExtension(path)} already exists");
            }

            _fileWizard.CreateDirectory(logFolder);
            _fileWizard.CopyFileToReportLogFolder(path, newFilePath);
        }

        public void DeleteReport(ReportTypes reportType)
        {
            var filePath = _fileWizard.GetOpenDialogFilePath($".\\ImportLogs\\{reportType}");
            _fileWizard.DeleteFile(filePath);
        }
    }
}