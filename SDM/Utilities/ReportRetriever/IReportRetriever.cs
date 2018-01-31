using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.ReportRetriever
{
    public interface IReportRetriever
    {
        FullDatabaseModel GetFullDebtReport(List<ClientLog> clientReportModels, List<CenturionLog> centurionReportModels);

        SummedDatabaseModel GetSummedDebtReport(FullDatabaseModel fullDatabaseModel);

        void GetInvoiceNumberIssues(List<ClientLog> clientReportModels, List<CenturionLog> centurionReportModels);
    }
}
