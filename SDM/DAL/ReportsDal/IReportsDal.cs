using System.Collections.Generic;

namespace SDM.DAL.ReportsDal
{
    public interface IReportsDal
    {
        void ExportReport(List<string> report);
        void ExportReports(Dictionary<string, List<string>> reports);
        void ExportIssuesReport(List<string> issues);
    }
}
