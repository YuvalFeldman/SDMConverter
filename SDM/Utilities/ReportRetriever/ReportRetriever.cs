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
                if (fullDatabaseRow.ClientId == null)
                {
                    continue;                    
                }
                if (dataBaseRowsByClientId.ContainsKey(fullDatabaseRow.ClientId.Replace(" ", "")))
                {
                    dataBaseRowsByClientId[fullDatabaseRow.ClientId.Replace(" ", "")].Add(fullDatabaseRow);
                }
                else
                {
                    dataBaseRowsByClientId.Add(fullDatabaseRow.ClientId.Replace(" ", ""), new List<FullDatabaseRow> { fullDatabaseRow });
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

                var clientSummedDatabase = new SummedDatabasePartner();
                clientSummedDatabase.ClientName = clientDbRows.Key;
                clientSummedDatabase.SummedDbPerDate = clientDbRowsSplitByPaymentDueDate
                    .ToDictionary(key => key.Key, value => value.Value.Select(databaseRow =>
                    {
                        var row = new SummedDatabaseRow();
                        row.Month = value.Key;
                        row.InvoiceNumber = databaseRow.InvoiceNumber;
                        row.PaymentDue = databaseRow.PaymentDue;
                        row.PaymentPaid = databaseRow.Payments.Sum(payment => payment.PaymentPaid);
                        row.PaidBelow30 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency)- databaseRow.PaymentDueDate).TotalDays < 30).Sum(payment => payment.PaymentPaid);
                        row.PaidOver30Below60 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 30 && (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays < 60).Sum(payment => payment.PaymentPaid);
                        row.PaidOver60Below90 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 60 && (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays < 90).Sum(payment => payment.PaymentPaid);
                        row.PaidOver90 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 90).Sum(payment => payment.PaymentPaid);
                        return row;
                    }).ToList());

                summedDatabase.SummedDatabase.Add(clientDbRows.Key, clientSummedDatabase);
            }

            return summedDatabase;
        }

        public FullDatabaseModel GetFullDebtReport(List<ClientLog> clientReportModels, List<CenturionLog> centurionReportModels)
        {
            var fullDatabase = new FullDatabaseModel();
            _dataImporter.UpdateDatabase(fullDatabase, clientReportModels);
            _dataImporter.UpdateDatabase(fullDatabase, centurionReportModels);

            return fullDatabase;
        }

        public void GetInvoiceNumberIssues(List<ClientLog> clientReportModels, List<CenturionLog> centurionReportModels)
        {
            var fullDatabase = new FullDatabaseModel();
            _dataImporter.UpdateDatabase(fullDatabase, clientReportModels);
            _dataImporter.OutputInvoiceNumberIssues(fullDatabase, centurionReportModels);
        }
    }
}
