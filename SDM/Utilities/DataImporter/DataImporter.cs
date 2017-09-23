using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public class DataImporter : IDataImporter
    {
        public void UpdateDatabase(FullDatabaseModel fullDatabase, List<ClientReportModel> data)
        {
            var uniqueClientReportRows =
                data
                .Select(report => report.ClientReport)
                .Aggregate((a,b) => a.Union(b).ToList());

            foreach (var clientReportRow in uniqueClientReportRows)
            {
                if (fullDatabase.FullDatabase.Any(fullDatabaseRow => fullDatabaseRow.InvoiceNumber.Equals(clientReportRow.InvoiceNumber)))
                {
                    continue;
                }

                var newDatabaseRow = new FullDatabaseRow
                {
                    InvoiceNumber = clientReportRow.InvoiceNumber,
                    PaymentDue = clientReportRow.AmountDue,
                    PaymentDueDate = new DateTime(
                        clientReportRow.InvoiceDate.Year,
                        clientReportRow.InvoiceDate.Month,
                        DateTime.DaysInMonth(clientReportRow.InvoiceDate.Year, clientReportRow.InvoiceDate.Month)).
                        AddDays(clientReportRow.PaymentTerms)
                };

                fullDatabase.FullDatabase.Add(newDatabaseRow);
            }
        }

        public void UpdateDatabase(FullDatabaseModel fullDatabase, List<CenturionReportModel> data)
        {
            var uniqueCenturionReportRows =
                data
                .Select(report => report.CenturionReport)
                .Aggregate((a, b) => a.Union(b).ToList());

            //TODO: export errors list with rows that have new invoice numbers
            foreach (var uniqueCenturionReportRow in uniqueCenturionReportRows)
            {
                var fullDbRow = fullDatabase.FullDatabase.First(row => row.InvoiceNumber.Equals(uniqueCenturionReportRow.InvoiceNumber));
                fullDbRow.ClientId = uniqueCenturionReportRow.ClientId;
                var payment = new PaymentDateLatencyPaid
                {
                    PaymentDate = uniqueCenturionReportRow.PaymentDate,
                    PaymentPaid = uniqueCenturionReportRow.AmountPaid
                };

                var totalDaysBetweenDueAndPaid = (uniqueCenturionReportRow.PaymentDate - fullDbRow.PaymentDueDate).TotalDays;
                payment.Latency = totalDaysBetweenDueAndPaid > 0 ? (int)totalDaysBetweenDueAndPaid : 0;
            }
        }
    }
}
