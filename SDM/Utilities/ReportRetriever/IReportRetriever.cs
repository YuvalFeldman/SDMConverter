using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.ReportRetriever
{
    public interface IReportRetriever
    {
        FullDatabaseModel GetFullDebtReport(List<ClientReportModel> clientReportModels, List<CenturionReportModel> centurionReportModels);

        SummedDatabaseModel GetSummedDebtReport(FullDatabaseModel fullDatabaseModel);
    }
}
