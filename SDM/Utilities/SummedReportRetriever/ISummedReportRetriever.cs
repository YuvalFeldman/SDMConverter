using SDM.Models.ReportModels;

namespace SDM.Utilities.SummedReportRetriever
{
    public interface ISummedReportRetriever
    {
        SummedDatabaseModel GetSummedDebtReport();
    }
}
