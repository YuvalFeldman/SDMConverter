using System.Collections.Generic;

namespace SDM.DAL.ReportsDal
{
    public interface IReportsDal
    {
        void ExportReport(List<string> report, List<string> issues);
        void ExportReports(Dictionary<string, List<string>> reports, List<string> issues);
    }
}
