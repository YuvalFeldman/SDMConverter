using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.FileWizard;
using SDM.Models;
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

        public List<List<string>> ReadFile()
        {
            if (_openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            var filePath = _openFileDialog.FileName;
            var fileContent = _fileWizard.ReadFile(filePath);
            var parsedFileContent = _dataConverter.ConvertCsvToArray(fileContent);

            return parsedFileContent;
        }

        public void LogData(List<List<string>> data, ImportTypes importType)
        {
            var logFolder = $".\\ImportLogs\\{importType}";

            var existingLogs = _fileWizard.GetFileNamesInDirectory(logFolder);
            var fileCounter = existingLogs.Count > 0
                ? existingLogs.Select(name => int.Parse(name.Split('_')[0])).Max()
                : 0;

            var logName = $"{fileCounter}_{importType}_{(int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds}";
            var fullFilePath = $"{logFolder}\\{logName}.csv";

            var csvData = _dataConverter.ConvertArrayToCsv(data);

            _fileWizard.WriteToFile(fullFilePath, csvData);
        }

        public void AddDataToDb(List<List<string>> updatedDatabase)
        {
            var csvDbContent = _dataConverter.ConvertArrayToCsv(updatedDatabase);
            _fileWizard.WriteToFile(DbPath, csvDbContent);
        }

        public void RebootDbFromLogs()
        {
            var dbContent = GetDatabase();

            var clientDataLogPaths = _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ImportTypes.ClientData}");
            var clientDataLogs = clientDataLogPaths.Select(path => _fileWizard.ReadFile(path));

            dbContent = clientDataLogs.Select(clientDataLog => _dataConverter.ConvertCsvToArray(clientDataLog)).Aggregate(dbContent, (current, data) => _dataImporter.GetDbUpdatedWithClientData(current, data));

            var centurionDebtCollectionLogPaths = _fileWizard.GetFileNamesInDirectory($".\\ImportLogs\\{ImportTypes.CenturionDebtCollection}");
            var centurionDebtCollectionDataLogs = centurionDebtCollectionLogPaths.Select(path => _fileWizard.ReadFile(path));

            dbContent = centurionDebtCollectionDataLogs.Select(clientDataLog => _dataConverter.ConvertCsvToArray(clientDataLog)).Aggregate(dbContent, (current, data) => _dataImporter.GetDbUpdatedWithcenturionDebtCollection(current, data));

            var csvDbContent = _dataConverter.ConvertArrayToCsv(dbContent);

            _fileWizard.WriteToFile(DbPath, csvDbContent);
        }

        public List<List<string>> GetDatabase()
        {
            var dbContent = _fileWizard.ReadFile(DbPath);
            var parsedDb = _dataConverter.ConvertCsvToArray(dbContent);

            return parsedDb;
        }

        public void WriteToFile(List<List<string>> data)
        {
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var filePath = _openFileDialog.FileName;
            var csvFileContent = _dataConverter.ConvertArrayToCsv(data);
            _fileWizard.WriteToFile(filePath, csvFileContent);
        }
    }
}