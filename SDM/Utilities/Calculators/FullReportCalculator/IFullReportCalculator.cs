using System;
using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.FullReportCalculator
{
    public interface IFullReportCalculator
    {
        Tuple<FullDatabaseModel, List<string>> GetFullReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);
        Tuple<List<string>, List<string>> GetFullReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null);

    }
}
