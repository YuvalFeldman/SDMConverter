using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SDM.DAL.FileWizard
{
    public class FileWizard : IFileWizard
    {
        public List<string> ReadFile(string path)
        {
            var file = new List<string>();
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    file.Add(line);
                }
            }

            return file;
        }

        public void WriteToFile(string path, List<string> data)
        {
            CreateFile(path);
            File.WriteAllLines(path, data);
        }

        public void CreateFile(string path)
        {
            CreateDirectory(Path.GetDirectoryName(path));

            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public List<string> GetFileNamesInDirectory(string path)
        {
            return Directory.GetFiles(path, "*.csv").Select(Path.GetFileName).ToList();
        }
    }
}
