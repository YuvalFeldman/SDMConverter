using System.Collections.Generic;

namespace SDM.Utilities.ReportRetriever
{
    public interface IReportRetriever
    {
        List<List<string>> GetSummedDebtReport(List<List<string>> database);
    }
}
