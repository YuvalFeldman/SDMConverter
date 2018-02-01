using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SDM.DAL.FileSystemController
{
    public class FileSystemController : IFileSystemController
    {
        private readonly OpenFileDialog _openFileDialogLimitDirectory;
        private readonly SaveFileDialog _saveFileDialog;
        private readonly FolderBrowserDialog _folderBrowserDialog;

        public FileSystemController(SaveFileDialog saveFileDialog, FolderBrowserDialog folderBrowserDialog, OpenFileDialog openFileDialogLimitDirectory, OpenFileDialog openFileDialogCenturionFile)
        {
            _saveFileDialog = saveFileDialog;
            _folderBrowserDialog = folderBrowserDialog;
            _openFileDialogLimitDirectory = openFileDialogLimitDirectory;

            _openFileDialogLimitDirectory = new OpenFileDialog { Filter = @"CSV files (*.csv)|*.csv" };
            _saveFileDialog = new SaveFileDialog { Filter = @"CSV files (*.csv)|*.csv" };
        }

        public List<string> ReadFileContents(string path)
        {
            if (string.IsNullOrEmpty(Path.GetFileName(path)))
            {
                return null;
            }
            if (!File.Exists(path))
            {
                throw new Exception($"File does not exist, file: {path}");
            }
            try
            {
                var file = new List<string>();
                using (var reader = new StreamReader(path, Encoding.GetEncoding("windows-1255")))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        file.Add(line);
                    }
                }

                return file;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading file data, file: {path}", ex);
            }
        }

        public void WriteDataToFile(string path, List<string> data)
        {
            CreateFile(path);
            File.WriteAllLines(path, data, Encoding.GetEncoding("windows-1255"));
        }

        public void CreateFile(string path)
        {
            CreateDirectory(Path.GetDirectoryName(path));

            if (!string.IsNullOrEmpty(path) && !File.Exists(path))
            {
                var file = File.Create(path);
                file.Close();
            }
        }

        public void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }
            var fullPath = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(fullPath) && !Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public void DeleteFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void DeleteDirectory(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public List<string> GetAllFileNamesInDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return new List<string>();
            }

            var filesInDirectory = Directory.GetFiles(path);
            return filesInDirectory.ToList();
        }

        public void AppendClientId(string path, string id)
        {
            if (!File.Exists(path))
            {
                return;
            }

            var updatedContent = new List<string>();
            var fileContent = ReadFileContents(path);
            var first = true;
            foreach (var line in fileContent)
            {
                if (first)
                {
                    first = false;
                    updatedContent.Add($"{line},client id");
                    continue;
                }

                updatedContent.Add($"{line},{id}");
            }

            WriteDataToFile(path, updatedContent);
        }

        public List<string> GetFileNamesInDirectory(string path)
        {
            return string.IsNullOrEmpty(path) && !File.Exists(path) ? new List<string>() : Directory.GetFiles(path, "*.csv").Select(Path.GetFileName).ToList();
        }

        public void CopyFile(string originFilePath, string newFilePath)
        {
            if (string.IsNullOrEmpty(originFilePath) || string.IsNullOrEmpty(newFilePath) || !File.Exists(originFilePath))
            {
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
            {
                Directory.CreateDirectory(newFilePath);
            }
            //if (!File.Exists(newFilePath))
            //{
            //    File.Create(newFilePath);
            //}
            //var content = ReadFileContents(originFilePath);
            //WriteDataToFile(newFilePath, content);
            File.Copy(originFilePath, newFilePath, true);
        }

        public string GetSaveDialogFilePath()
        {
            return _saveFileDialog.ShowDialog() != DialogResult.OK ? null : _saveFileDialog.FileName;
        }

        public string GetOpenDialogFilePath(string limitToDirectory = null)
        {
            if (!Directory.Exists(limitToDirectory))
            {
                CreateDirectory(limitToDirectory);
            }

            _openFileDialogLimitDirectory.InitialDirectory =
                !string.IsNullOrEmpty(limitToDirectory) ?
                    limitToDirectory :
                    string.Empty;

            return _openFileDialogLimitDirectory.ShowDialog() != DialogResult.OK ? null : _openFileDialogLimitDirectory.FileName;
        }

        public string GetDirectoryPath()
        {
            return _folderBrowserDialog.ShowDialog() != DialogResult.OK ? null : _folderBrowserDialog.SelectedPath;
        }
    }
}
