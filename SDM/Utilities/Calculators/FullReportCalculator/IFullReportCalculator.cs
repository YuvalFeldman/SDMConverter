using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.FullReportCalculator
{
    public interface IFullReportCalculator
    {
        FullDatabaseModel GetFullReportModel();
        List<string> GetFullReport();
    }
}
