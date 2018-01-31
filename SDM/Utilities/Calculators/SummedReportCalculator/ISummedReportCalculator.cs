using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public interface ISummedReportCalculator
    {
        SummedDatabaseModel GetSummedReportModel();
        List<string> GetSummedReport();
    }
}
