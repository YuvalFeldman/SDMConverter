using System;
using System.Windows.Forms;
using SDM.DAL.FileSystemController;
using SDM.DAL.FileWizard;
using SDM.SDM;
using SDM.Utilities.DataConverter;
using SDM.Utilities.DataImporter;
using SDM.Utilities.ReportRetriever;

namespace SDM
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var sdm =
                new SDM.SDM(
                    new FileSystemController(
                        new FileWizard(new SaveFileDialog(), new FolderBrowserDialog(), new OpenFileDialog(),
                            new OpenFileDialog()), new DataConverter()), new ReportRetriever(new DataImporter()));
            Application.Run(new Form1(sdm));
        }
    }
}
