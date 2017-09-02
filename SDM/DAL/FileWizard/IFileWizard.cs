using System.Collections.Generic;

namespace SDM.DAL.FileWizard
{
    public interface IFileWizard
    {
        List<string> ReadFile(string path);

        List<string> GetFileNamesInDirectory(string path);

        void WriteToFile(string path, List<string> data);

        void CreateFile(string path);

        void CreateDirectory(string path);

        void DeleteFile(string path);

        void DeleteDirectory(string path);
    }
}
