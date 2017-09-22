using System.Collections.Generic;
using SDM.Models.Enums;

namespace SDM.DAL.FileWizard
{
    public interface IFileWizard
    {
        List<string> ReadFile(string path);

        List<string> GetFileNamesInDirectory(string path);

        void WriteToFile(string path, List<string> data);

        void CopyFileToReportLogFolder(string filePath, string newFilePath);

        void CreateFile(string path);

        void CreateDirectory(string path);

        void DeleteFile(string path);

        void DeleteDirectory(string path);
    }
}
