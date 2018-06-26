using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.IssuesReportCalculator
{
    public interface IIssuesReportCalculator
    {
        List<string> GetReportIssues(List<string> clientLogNames, List<string> additionalIssues, string latencyTable = null);
    }
}
