using System.Collections.Generic;
using SDM.Models;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        List<ClientModel> ReadClientLog();

        List<CenturionModel> ReadCenturionLog();

        void LogData(List<ClientModel> data);

        void LogData(List<CenturionModel> data);

        void WriteToFile(List<ClientModel> data);

        void WriteToFile(List<CenturionModel> data);

        void WriteToFile(List<FullDatabaseRow> data);

        void WriteToFile(List<SummedDatabasePartner> data);
    }
}
