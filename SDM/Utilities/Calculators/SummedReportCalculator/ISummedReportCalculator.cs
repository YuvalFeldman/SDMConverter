using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public interface ISummedReportCalculator
    {
        SummedDatabaseModel GetSummedReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
        Dictionary<string, List<string>> GetSummedReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
    }
}
