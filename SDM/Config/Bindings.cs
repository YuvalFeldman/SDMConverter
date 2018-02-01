using Ninject.Modules;
using SDM.DAL.FileSystemController;
using SDM.DAL.logsDal;
using SDM.DAL.ReportsDal;
using SDM.Forms;
using SDM.Forms.ContentForms.ExportMenuForms;
using SDM.Forms.ContentForms.ImportForms;
using SDM.Utilities.Calculators.FullReportCalculator;
using SDM.Utilities.Calculators.IssuesReportCalculator;
using SDM.Utilities.Calculators.SummedReportCalculator;
using SDM.Utilities.DataConverter;

namespace SDM.Config
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IReportsDal>().To<ReportsDal>().InSingletonScope();
            Bind<ISdmlogsDal>().To<SdmlogsDal>().InSingletonScope();
            Bind<IDataConverter>().To<DataConverter>().InSingletonScope();
            Bind<IIssuesReportCalculator>().To<IssuesReportCalculator>().InSingletonScope();
            Bind<IFileSystemController>().To<FileSystemController>().InSingletonScope();
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
