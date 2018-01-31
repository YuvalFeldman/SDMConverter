using Ninject.Modules;
using SDM.DAL.FileSystemController;
using SDM.DAL.logsDal;
using SDM.DAL.ReportsDal;
using SDM.Forms;
using SDM.Forms.ContentForms.ExportMenuForms;
using SDM.Forms.ContentForms.ImportForms;
using SDM.SDM;
using SDM.Utilities.Calculators.FullReportCalculator;
using SDM.Utilities.Calculators.SummedReportCalculator;
using SDM.Utilities.DataConverter;
using SDM.Utilities.DataImporter;
using SDM.Utilities.ReportRetriever;

namespace SDM.Config
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IReportsDal>().To<ReportsDal>().InSingletonScope();
            Bind<ISdmlogsDal>().To<SdmlogsDal>().InSingletonScope();
            Bind<IDataConverter>().To<DataConverter>().InSingletonScope();
            Bind<IDataImporter>().To<DataImporter>().InSingletonScope();
            Bind<IReportRetriever>().To<ReportRetriever>().InSingletonScope();
            Bind<IFileSystemController>().To<FileSystemController>().InSingletonScope();
            Bind<ISDM>().To<SDM.SDM>().InSingletonScope();
            Bind<ISummedReportCalculator>().To<SummedReportCalculator>().InSingletonScope();
            Bind<IFullReportCalculator>().To<FullReportCalculator>().InSingletonScope();

            Bind<SdmForm>().ToSelf();
            Bind<FullExportMenu>().ToSelf();
            Bind<SummedExportMenu>().ToSelf();
            Bind<ImportCenturionForm>().ToSelf();
            Bind<ImportClientForm>().ToSelf();
            Bind<ImportLatencyForm>().ToSelf();
        }
    }
}
