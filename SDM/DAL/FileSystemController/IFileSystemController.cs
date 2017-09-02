using System.Collections.Generic;
using SDM.Models;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        List<List<string>> ReadFile();

        void LogData(List<List<string>> data, ImportTypes importType);

        void AddDataToDb(List<List<string>> updatedDatabase);

        void RebootDbFromLogs();

        List<List<string>> GetDatabase();

        void WriteToFile(List<List<string>> data);
    }
}
