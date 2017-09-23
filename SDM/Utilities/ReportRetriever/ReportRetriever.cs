using System.Collections.Generic;
using System.Linq;
using SDM.Models.ReportModels;
using SDM.Utilities.DataImporter;

namespace SDM.Utilities.ReportRetriever
{
    public class ReportRetriever : IReportRetriever
    {
        private readonly IDataImporter _dataImporter;

        public ReportRetriever(IDataImporter dataImporter)
        {
            _dataImporter = dataImporter;
        }

        public SummedDatabaseModel  GetSummedDebtReport(FullDatabaseModel fullDatabaseModel)
        {
            var dbSplitByPartner = new Dictionary<string, List<FullDatabaseRow>>();
            foreach (var fullDatabaseRow in fullDb)
            {
                if (dbSplitByPartner.ContainsKey(fullDatabaseRow.Value.ClientId))
                {
                    dbSplitByPartner.Add(fullDatabaseRow.Value.ClientId, new List<FullDatabaseRow>{fullDatabaseRow.Value});
                }
                else
                {
                    dbSplitByPartner[fullDatabaseRow.Value.ClientId].Add(fullDatabaseRow.Value);
                }
            }

            var summedPartnerDb = dbSplitByPartner.ToDictionary(key => key, partnerDb => ConvertClientFullDbToSummedFullDb(partnerDb.Value));

            return summedPartnerDb.Values.ToList();
        }

        public FullDatabaseModel GetFullDebtReport(List<ClientReportModel> clientReportModels, List<CenturionReportModel> centurionReportModels)
        {
            var fullDatabase = new FullDatabaseModel();
            _dataImporter.UpdateDatabase(fullDatabase, clientReportModels);
            _dataImporter.UpdateDatabase(fullDatabase, centurionReportModels);

            return fullDatabase;
        }
    }
}
