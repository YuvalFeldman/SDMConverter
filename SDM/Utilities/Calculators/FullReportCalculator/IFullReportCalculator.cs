using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.FullReportCalculator
{
    public interface IFullReportCalculator
    {
        FullDatabaseModel GetFullReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
        List<string> GetFullReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);

    }
}
