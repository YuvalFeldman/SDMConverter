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
            if (!File.Exists(path))
            {
                throw new Exception($"File does not exist, file: {path}");
            }
            try
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
            if (_saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return _saveFileDialog.FileName;
        }

        public string GetOpenDialogFilePath(string limitToDirectory = null)
        {
            _openFileDialogLimitDirectory.InitialDirectory = 
                string.IsNullOrEmpty(limitToDirectory) ? 
                limitToDirectory : 
                string.Empty;

            if (_openFileDialogLimitDirectory.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return _openFileDialogLimitDirectory.FileName;
        }

        public string GetDirectoryPath()
        {
            if (_folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            return _folderBrowserDialog.SelectedPath;
        }
    }
}
