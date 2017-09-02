using System.Collections.Generic;
using SDM.DAL.FileSystemController;
using SDM.Models;
using SDM.Utilities.DataImporter;
using SDM.Utilities.ReportRetriever;

namespace SDM.SDM
{
    public class SDM : ISDM
    {
        private readonly IFileSystemController _fileSystemController;
        private readonly IReportRetriever _reportRetriever;
        private readonly IDataImporter _dataImporter;

        public SDM(IFileSystemController fileSystemController, IReportRetriever reportRetriever, IDataImporter dataImporter)
        {
            _fileSystemController = fileSystemController;
            _reportRetriever = reportRetriever;
            _dataImporter = dataImporter;
        }

        public void ImportClientData()
        {
            AddDataToDb(ImportTypes.ClientData);
        }

        public void ImportcenturionDebtCollection()
        {
            AddDataToDb(ImportTypes.CenturionDebtCollection);
        }

        public void GetDebtReport()
        {
            var database = _fileSystemController.GetDatabase();
            _fileSystemController.WriteToFile(database);
        }

        public void GetSummedDebtReport()
        {
            var database = _fileSystemController.GetDatabase();
            var summedDatabase = _reportRetriever.GetSummedDebtReport(database);
            _fileSystemController.WriteToFile(summedDatabase);
        }

        private void AddDataToDb(ImportTypes importType)
        {
            var log = _fileSystemController.ReadFile();
            _fileSystemController.LogData(log, importType);

            var currentDatabase = _fileSystemController.GetDatabase();
            var updatedDatabase = GetUpdatedDatabase(currentDatabase, log, importType);
            _fileSystemController.AddDataToDb(updatedDatabase);
        }

        private List<List<string>> GetUpdatedDatabase(List<List<string>> database, List<List<string>> newData,
            ImportTypes importType)
        {
            return importType.Equals(ImportTypes.ClientData) ?
                _dataImporter.GetDbUpdatedWithClientData(database, newData) :
                _dataImporter.GetDbUpdatedWithcenturionDebtCollection(database, newData);
        }
    }
}