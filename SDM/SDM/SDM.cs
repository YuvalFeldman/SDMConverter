using System.Collections.Generic;
using SDM.DAL.FileSystemController;
using SDM.Models;
using SDM.Utilities.DataImporter;
using SDM.Utilities.SummedReportRetriever;

namespace SDM.SDM
{
    public class SDM : ISDM
    {
        private readonly IFileSystemController _fileSystemController;
        private readonly ISummedReportRetriever _summedReportRetriever;
        private readonly IDataImporter _dataImporter;

        public SDM(IFileSystemController fileSystemController, ISummedReportRetriever summedReportRetriever, IDataImporter dataImporter)
        {
            _fileSystemController = fileSystemController;
            _summedReportRetriever = summedReportRetriever;
            _dataImporter = dataImporter;
        }

        public void ImportClientReport()
        {
            var data = _fileSystemController.ReadClientLog();
            _fileSystemController.LogData(data);
            _dataImporter.UpdateDatabase(data);
        }

        public void ImportcenturionReport()
        {
            var data = _fileSystemController.ReadCenturionLog();
            _fileSystemController.LogData(data);
            _dataImporter.UpdateDatabase(data);
        }

        public void ExportFullDebtReport()
        {
            var report = _summedReportRetriever.GetFullDebtReport();
            _fileSystemController.WriteToFile(report);
        }

        public void ExportSummedDebtReport()
        {
            var report = _summedReportRetriever.GetSummedDebtReport();
            _fileSystemController.WriteToFile(report);
        }

        public void DeleteClientReport()
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCenturionReport()
        {
            throw new System.NotImplementedException();
        }
    }
}