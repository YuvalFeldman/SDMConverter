using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        ClientReportModel ReadClientLog();

        CenturionReportModel ReadCenturionLog();

        void LogData(ClientReportModel data);

        void LogData(CenturionReportModel data);

        void WriteToFile(FullDatabseModel data);

        void WriteToFile(SummedDatabaseModel data);

        void WriteToFile(SummedDatabasePartner data);
    }
}
