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
            var data = _fileSystemController.ReadClientLog();
            _fileSystemController.LogData(data);
            _dataImporter.UpdateDatabase(data);
        }

        public void ImportcenturionDebtCollection()
        {
            var data = _fileSystemController.ReadCenturionLog();
            _fileSystemController.LogData(data);
            _dataImporter.UpdateDatabase(data);
        }

        public void GetFullDebtReport()
        {
            var report = _reportRetriever.GetFullDebtReport();
            _fileSystemController.WriteToFile(report);
        }

        public void GetSummedDebtReport()
        {
            var report = _reportRetriever.GetSummedDebtReport();
            _fileSystemController.WriteToFile(report);
        }
    }
}