using System.Collections.Generic;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        string GetSaveDialogFilePath();

        string GetOpenDialogFilePath();

        string GetDirectoryPath();

        List<string> ReadFileContents(string path);

        void WriteDataToFile(string path, List<string> data);

        void CopyFile(string originFilePath, string newFilePath);

        void CreateFile(string path);

        void CreateDirectory(string path);

        void DeleteFile(string path);

        List<string> GetAllFileNamesInDirectory(string path);

        void AppendClientId(string path, string id);
    }
}
