using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabase(FullDatabaseModel fullDatabase, List<ClientReportModel> data);

        void UpdateDatabase(FullDatabaseModel fullDatabase, List<CenturionReportModel> data);

        void OutputInvoiceNumberIssues(FullDatabaseModel fullDatabase, List<CenturionReportModel> data);
    }
}
