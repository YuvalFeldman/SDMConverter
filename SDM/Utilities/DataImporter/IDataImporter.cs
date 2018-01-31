using System.Collections.Generic;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabase(FullDatabaseModel fullDatabase, List<ClientLog> data);

        void UpdateDatabase(FullDatabaseModel fullDatabase, List<CenturionLog> data);

        void OutputInvoiceNumberIssues(FullDatabaseModel fullDatabase, List<CenturionLog> data);
    }
}
