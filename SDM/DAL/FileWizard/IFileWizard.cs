using System.Collections.Generic;

namespace SDM.DAL.FileWizard
{
    public interface IFileWizard
    {
        string GetSaveDialogFilePath();

        string GetOpenDialogFilePath(string limitToDirectory = null);

        string GetDirectoryPath();

        List<string> ReadFileContents(string path);

        List<string> GetFileNamesInDirectory(string path);

        void WriteDataToFile(string path, List<string> data);

        void CopyFileToReportLogFolder(string filePath, string newFilePath);

        void CreateFile(string path);

        void CreateDirectory(string path);

        void DeleteFile(string path);

        void DeleteDirectory(string path);
    }
}
