using System.Collections.Generic;
using SDM.Models.Enums;
using SDM.Models.ReportModels;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        List<ClientReportModel> ReadClientLogs();

        List<CenturionReportModel> ReadCenturionLogs();

        void LogData(ReportTypes reportType);

        void WriteToFile(FullDatabaseModel data);

        void WriteToFile(SummedDatabaseModel data);

        void DeleteReport(ReportTypes reportType);
    }
}
