using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Database;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.ReportRetriever
{
    public class ReportRetriever : IReportRetriever
    {
        private readonly IDatabase _database;

        public ReportRetriever(IDatabase database)
        {
            _database = database;
        }

        public List<FullDatabaseRow> GetFullDebtReport()
        {
            return _database.Get().Values.ToList();
        }

        public List<SummedDatabasePartner> GetSummedDebtReport()
        {
            var fullDb = _database.Get();

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

        public List<SummedDatabasePartner> ConvertClientFullDbToSummedFullDb(List<FullDatabaseRow> clientFullDb)
        {
            
        }
    }
}
