using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.IssuesReportCalculator
{
    public interface IIssuesReportCalculator
    {
        void OutputInvoiceNumberIssues(FullDatabaseModel fullDatabase, List<CenturionLog> data);
    }
}
