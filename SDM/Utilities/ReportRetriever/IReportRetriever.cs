using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.ReportRetriever
{
    public interface IReportRetriever
    {
        List<FullDatabaseRow> GetFullDebtReport();
        List<SummedDatabasePartner> GetSummedDebtReport();
    }
}
