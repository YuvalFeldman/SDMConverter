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
                MessageBox.Show($@"Error reading file data, file: {path}. InnerMessage: {ex.Message}");
                throw;
            }
        }

        public void WriteDataToFile(string path, List<string> data)
        {
            try
            {
                CreateFile(path);
                File.WriteAllLines(path, data, Encoding.GetEncoding("windows-1255"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Failed writing data to file, file: {path}. InnerMessage: {ex.Message}");
            }
        }

        public void CreateFile(string path)
        {
            try
            {
                CreateDirectory(Path.GetDirectoryName(path));

                if (!string.IsNullOrEmpty(path) && !File.Exists(path))
                {
                    var file = File.Create(path);
                    file.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed Creating File, file: {path}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public void CreateDirectory(string path)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show($@"Failed creating directory, directory: {path}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public void DeleteFile(string path)
        {
            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed Deleting file, file: {path}. InnerMessage: {e.Message}");
            }
        }

        public List<string> GetAllFileNamesInDirectory(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return new List<string>();
                }

                var filesInDirectory = Directory.GetFiles(path);
                return filesInDirectory.ToList();
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed geting file names from directory, directory: {path}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public void AppendClientId(string path, string id)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show($@"Failed adding client id to report: {path}, id: {id}. InnerMessage: {e.Message}");
                throw;
            }
        }

        public void CopyFile(string originFilePath, string newFilePath)
        {
            if (string.IsNullOrEmpty(originFilePath) || string.IsNullOrEmpty(newFilePath) || !File.Exists(originFilePath))
            {
                return;
            }
            if (!Directory.Exists(Path.GetDirectoryName(newFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newFilePath));
            }
            File.Copy(originFilePath, newFilePath, true);
        }

        public string GetSaveDialogFilePath()
        {
            return _saveFileDialog.ShowDialog() != DialogResult.OK ? null : _saveFileDialog.FileName;
        }

        public string GetOpenDialogFilePath()
        {
            return _openFileDialogLimitDirectory.ShowDialog() != DialogResult.OK ? null : _openFileDialogLimitDirectory.FileName;
        }

        public string GetDirectoryPath()
        {
            return _folderBrowserDialog.ShowDialog() != DialogResult.OK ? null : _folderBrowserDialog.SelectedPath;
        }
    }
}
