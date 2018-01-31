using System;
using System.Windows.Forms;
using Ninject;
using SDM.Config;
using SDM.Forms;

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

            var kernel = new StandardKernel(new Bindings());
            var form = kernel.Get<SdmForm>();
            Application.Run(form);
        }
    }
}
