using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public class SummedReportCalculator : ISummedReportCalculator
    {
        public SummedDatabaseModel GetSummedReportModel()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetSummedReport()
        {
            var summedReportModel = GetSummedReportModel();
        }
    }
}
