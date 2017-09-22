using System.Collections.Generic;
using System.Linq;
using SDM.Models.ReportModels;

namespace SDM.Utilities.SummedReportRetriever
{
    public class SummedReportRetriever : ISummedReportRetriever
    {
        public SummedDatabaseModel GetSummedDebtReport()
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
    }
}
