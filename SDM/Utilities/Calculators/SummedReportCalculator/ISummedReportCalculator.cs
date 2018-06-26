using System;
using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public interface ISummedReportCalculator
    {
        Tuple<SummedDatabaseModel, List<string>> GetSummedReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
        Tuple<Dictionary<string, List<string>>, List<string>> GetSummedReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
    }
}
