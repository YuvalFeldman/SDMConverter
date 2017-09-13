using System.Collections.Generic;
using SDM.Models;

namespace SDM.Utilities.ReportRetriever
{
    public interface IReportRetriever
    {
        List<FullDatabaseRow> GetFullDebtReport();
        List<SummedDatabasePartner> GetSummedDebtReport();
    }
}
