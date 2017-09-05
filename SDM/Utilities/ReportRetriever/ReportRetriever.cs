using System;
using System.Collections.Generic;
using SDM.Database;
using SDM.Models;

namespace SDM.Utilities.ReportRetriever
{
    public class ReportRetriever : IReportRetriever
    {
        private readonly IDatabase _database;

        public ReportRetriever(IDatabase database)
        {
            _database = database;
        }

        public List<FullDatabase> GetFullDebtReport()
        {
            return _database.Get();
        }

        public List<SummedDatabase> GetSummedDebtReport()
        {
            throw new NotImplementedException();
        }
    }
}
