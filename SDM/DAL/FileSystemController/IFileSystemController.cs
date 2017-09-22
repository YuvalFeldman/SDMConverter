using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        List<ClientModelRow> ReadClientLog();

        List<CenturionModelRow> ReadCenturionLog();

        void LogData(List<ClientModelRow> data);

        void LogData(List<CenturionModelRow> data);

        void WriteToFile(List<ClientModelRow> data);

        void WriteToFile(List<CenturionModelRow> data);

        void WriteToFile(List<FullDatabaseRow> data);

        void WriteToFile(List<SummedDatabasePartner> data);
    }
}
