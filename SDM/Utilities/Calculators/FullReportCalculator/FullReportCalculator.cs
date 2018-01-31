using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.FullReportCalculator
{
    public class FullReportCalculator : IFullReportCalculator
    {
        public FullDatabaseModel GetFullReportModel()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetFullReport()
        {
            var fullReportModel = GetFullReportModel();
        }
    }
}
