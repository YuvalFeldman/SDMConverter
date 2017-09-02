using Ninject.Modules;
using SDM.DAL.FileSystemController;
using SDM.DAL.FileWizard;
using SDM.Utilities.DataConverter;
using SDM.Utilities.DataImporter;

namespace SDM.Config
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataConverter>().To<DataConverter>().InSingletonScope();
            Bind<IFileSystemController>().To<FileSystemController>().InSingletonScope();
            Bind<IFileWizard>().To<FileWizard>().InSingletonScope();
            Bind<IDataImporter>().To<DataImporter>().InSingletonScope();
        }
    }
}
