﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                .Select(filePath => _fileWizard.ReadFileContents(filePath))
                .Select(fileContent => _dataConverter.ConvertCsvToClientDataModel(fileContent, latencyConversionModel))
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

        public void LogData(ReportTypes reportType, string clientId = null)
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

            if (!string.IsNullOrEmpty(clientId))
            {
                var fileContent = _fileWizard.ReadFileContents(newFilePath);
                fileContent[0] = $"{fileContent[0]}, client id";
                for (var i = 1; i < fileContent.Count; i++)
                {
                    fileContent[i] = $"{fileContent[i]}, {clientId}";
                }

                File.WriteAllLines(newFilePath, fileContent);
            }
        }

        public void DeleteReport(ReportTypes reportType)
        {
            var filePath = _fileWizard.GetOpenDialogFilePath($".\\ImportLogs\\{reportType}");
            _fileWizard.DeleteFile(filePath);
        }

        public LatencyConversionModel ReadLatencyConversionTable()
        {
            var path = _fileWizard.GetOpenDialogFilePath($".\\ImportLogs\\LatencyConversionTable");
            var latencyConversionTableContent = _fileWizard.ReadFileContents(path);

            return _dataConverter.ConvertCsvToLatencyConversionModel(latencyConversionTableContent);
        }

    }
}