using System;
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

        public SummedDatabaseModel GetSummedDebtReport(FullDatabaseModel fullDatabaseModel)
        {
            var dataBaseRowsByClientId = new Dictionary<string, List<FullDatabaseRow>>();
            foreach (var fullDatabaseRow in fullDatabaseModel.FullDatabase)
            {
                if (dataBaseRowsByClientId.ContainsKey(fullDatabaseRow.ClientId))
                {
                    dataBaseRowsByClientId[fullDatabaseRow.ClientId].Add(fullDatabaseRow);
                }
                else
                {
                    dataBaseRowsByClientId.Add(fullDatabaseRow.ClientId, new List<FullDatabaseRow> { fullDatabaseRow });
                }
            }

            var summedDatabase = new SummedDatabaseModel();
            foreach (var clientDbRows in dataBaseRowsByClientId)
            {
                var clientDbRowsSplitByPaymentDueDate = new Dictionary<DateTime, List<FullDatabaseRow>>();
                foreach (var clientDbRow in clientDbRows.Value)
                {
                    if (clientDbRowsSplitByPaymentDueDate.ContainsKey(clientDbRow.PaymentDueDate))
                    {
                        clientDbRowsSplitByPaymentDueDate[clientDbRow.PaymentDueDate].Add(clientDbRow);
                    }
                    else
                    {
                        clientDbRowsSplitByPaymentDueDate.Add(clientDbRow.PaymentDueDate, new List<FullDatabaseRow>{clientDbRow});
                    }
                }

                var clientSummedDatabase = new SummedDatabasePartner
                {
                    ClientName = clientDbRows.Key,
                    SummedDbPerDate = clientDbRowsSplitByPaymentDueDate
                        .ToDictionary(key => key.Key, value => value.Value.Select(databaseRow => new SummedDatabaseRow
                        {
                            Month = value.Key,
                            InvoiceNumber = databaseRow.InvoiceNumber,
                            PaymentDue = databaseRow.PaymentDue,
                            PaymentPaid = databaseRow.Payments.Sum(payment => payment.PaymentPaid),
                            PaidBelow30 = databaseRow.Payments.Where(payment => (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays < 30).Sum(payment => payment.PaymentPaid),
                            PaidOver30Below60 = databaseRow.Payments.Where(payment => (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays > 30 && (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays < 60).Sum(payment => payment.PaymentPaid),
                            PaidOver60Below90 = databaseRow.Payments.Where(payment => (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays > 60 && (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays < 90).Sum(payment => payment.PaymentPaid),
                            PaidOver90 = databaseRow.Payments.Where(payment => (databaseRow.PaymentDueDate - payment.PaymentDate.AddDays(payment.Latency)).TotalDays > 90).Sum(payment => payment.PaymentPaid)

                        }).ToList())
                };

                summedDatabase.SummedDatabase.Add(clientDbRows.Key, clientSummedDatabase);
            }

            return summedDatabase;
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
