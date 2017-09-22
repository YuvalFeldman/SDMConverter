using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.FileWizard;
using SDM.Models;
using SDM.Models.Enums;
using SDM.Models.ReportModels;
using SDM.Utilities.DataConverter;
using SDM.Utilities.DataImporter;

namespace SDM.DAL.FileSystemController
{
    public class FileSystemController : IFileSystemController
    {
        private readonly IFileWizard _fileWizard;
        private readonly OpenFileDialog _openFileDialog;
        private readonly SaveFileDialog _saveFileDialog;
        private readonly IDataConverter _dataConverter;
        private readonly IDataImporter _dataImporter;

        private const string DbPath = @".\Database\database.csv";

        public FileSystemController(IFileWizard fileWizard, IDataConverter dataConverter, IDataImporter dataImporter)
        {
            _fileWizard = fileWizard;
            _dataConverter = dataConverter;
            _dataImporter = dataImporter;
            _openFileDialog = new OpenFileDialog{ Filter = @"CSV files (*.csv)|*.csv" };
            _saveFileDialog = new SaveFileDialog { Filter = @"CSV files (*.csv)|*.csv" };
        }

        public List<ClientModelRow> ReadClientLog()
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            var filePath = _openFileDialog.FileName;
            var fileContent = _fileWizard.ReadFile(filePath);
            var parsedFileContent = _dataConverter.ConvertCsvToClientDataModel(fileContent);

            return parsedFileContent;
        }

        public List<CenturionModelRow> ReadCenturionLog()
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            var filePath = _openFileDialog.FileName;
            var fileContent = _fileWizard.ReadFile(filePath);
            var parsedFileContent = _dataConverter.ConvertCsvToCenturionModel(fileContent);

            return parsedFileContent;
        }

        public void LogData(List<ClientModelRow> data)
        {
            var logFolder = $".\\ImportLogs\\{ImportTypes.ClientData}";

            var existingLogs = _fileWizard.GetFileNamesInDirectory(logFolder);
            var fileCounter = existingLogs.Count > 0
                ? existingLogs.Select(name => int.Parse(name.Split('_')[0])).Max()
                : 0;

            var logName = $"{fileCounter}_{ImportTypes.ClientData}_{(int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}";
            var fullFilePath = $"{logFolder}\\{logName}.csv";

            var csvData = _dataConverter.ConvertClientDataModelToCsv(data);

            _fileWizard.WriteToFile(fullFilePath, csvData);
        }

        public void LogData(List<CenturionModelRow> data)
        {
            var logFolder = $".\\ImportLogs\\{ImportTypes.CenturionDebtCollection}";

            var existingLogs = _fileWizard.GetFileNamesInDirectory(logFolder);
            var fileCounter = existingLogs.Count > 0
                ? existingLogs.Select(name => int.Parse(name.Split('_')[0])).Max()
                : 0;

            var logName = $"{fileCounter}_{ImportTypes.CenturionDebtCollection}_{(int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}";
            var fullFilePath = $"{logFolder}\\{logName}.csv";

            var csvData = _dataConverter.ConvertCenturionModelToCsv(data);

            _fileWizard.WriteToFile(fullFilePath, csvData);
        }

        public void WriteToFile(List<ClientModelRow> data)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filePath = _openFileDialog.FileName;
            var csvFileContent = _dataConverter.ConvertClientDataModelToCsv(data);
            _fileWizard.WriteToFile(filePath, csvFileContent);
        }

        public void WriteToFile(List<CenturionModelRow> data)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filePath = _openFileDialog.FileName;
            var csvFileContent = _dataConverter.ConvertCenturionModelToCsv(data);
            _fileWizard.WriteToFile(filePath, csvFileContent);
        }

        public void WriteToFile(List<FullDatabaseRow> data)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filePath = _openFileDialog.FileName;
            var csvFileContent = _dataConverter.ConvertFullDatabaseToCsv(data);
            _fileWizard.WriteToFile(filePath, csvFileContent);
        }

        public void WriteToFile(List<SummedDatabasePartner> data)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filePath = _openFileDialog.FileName;
            var csvFileContent = _dataConverter.ConvertSummedDatabaseToCsv(data);
            _fileWizard.WriteToFile(filePath, csvFileContent);
        }
    }
}