using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SDM.DAL.FileWizard
{
    public class FileWizard : IFileWizard
    {
        private readonly OpenFileDialog _openFileDialogLimitDirectory;
        private readonly SaveFileDialog _saveFileDialog;
        private readonly FolderBrowserDialog _folderBrowserDialog;

        public FileWizard(SaveFileDialog saveFileDialog, FolderBrowserDialog folderBrowserDialog, OpenFileDialog openFileDialogLimitDirectory, OpenFileDialog openFileDialogCenturionFile)
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
            File.WriteAllLines(path, data);
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
            if (!string.IsNullOrEmpty(path) && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool DeleteFile(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public void DeleteDirectory(string path)
        {
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        public List<string> GetFileNamesInDirectory(string path)
        {
            return !string.IsNullOrEmpty(path) && !File.Exists(path) ? new List<string>() : Directory.GetFiles(path, "*.csv").Select(Path.GetFileName).ToList();
        }

        public void CopyFileToReportLogFolder(string filePath, string newFilePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(newFilePath))
            {
                return;
            }
            File.Copy(filePath, newFilePath);
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
