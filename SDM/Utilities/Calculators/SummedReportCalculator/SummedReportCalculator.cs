using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Models.ReportModels;
using SDM.Utilities.Calculators.FullReportCalculator;
using SDM.Utilities.DataConverter;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public class SummedReportCalculator : ISummedReportCalculator
    {
        private readonly IDataConverter _dataConverter;
        private readonly IFullReportCalculator _fullReportCalculator;

        public SummedReportCalculator(IDataConverter dataConverter, IFullReportCalculator fullReportCalculator)
        {
            _dataConverter = dataConverter;
            _fullReportCalculator = fullReportCalculator;
        }

        public SummedDatabaseModel GetSummedReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            var fullReportModel = _fullReportCalculator.GetFullReportModel(centurionLogNames, clientLogNames, latencyTable);

            var summedReportModel = ConvertFullReportToSummedReport(fullReportModel);

            return summedReportModel;
        }

        public Dictionary<string, List<string>> GetSummedReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            var summedReportModel = GetSummedReportModel(centurionLogNames, clientLogNames, latencyTable);
            var csvReport = _dataConverter.ConvertToCsv(summedReportModel);
            return csvReport;
        }

        private SummedDatabaseModel ConvertFullReportToSummedReport(FullDatabaseModel fullReportModel)
        {
            var dataBaseRowsByClientId = new Dictionary<string, List<FullDatabaseRow>>();
            foreach (var fullDatabaseRow in fullReportModel.FullDatabase)
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
                        clientDbRowsSplitByPaymentDueDate.Add(clientDbRow.PaymentDueDate, new List<FullDatabaseRow> { clientDbRow });
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
                        row.PaidBelow30 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays < 30).Sum(payment => payment.PaymentPaid);
                        row.PaidOver30Below60 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 30 && (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays < 60).Sum(payment => payment.PaymentPaid);
                        row.PaidOver60Below90 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 60 && (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays < 90).Sum(payment => payment.PaymentPaid);
                        row.PaidOver90 = databaseRow.Payments.Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - databaseRow.PaymentDueDate).TotalDays > 90).Sum(payment => payment.PaymentPaid);
                        return row;
                    }).ToList());

                summedDatabase.SummedDatabase.Add(clientDbRows.Key, clientSummedDatabase);
            }

            return summedDatabase;
        }
    }
}
