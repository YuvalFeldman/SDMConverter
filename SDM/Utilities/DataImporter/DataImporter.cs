using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.FileSystemController;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public class DataImporter : IDataImporter
    {
        private readonly IFileSystemController _fileSystemController;

        public DataImporter(IFileSystemController fileSystemController)
        {
            _fileSystemController = fileSystemController;
        }

        public void UpdateDatabase(FullDatabaseModel fullDatabase, List<ClientReportModel> data)
        {
            if (!data.Any())
            {
                return;
            }
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
                        AddDays(clientReportRow.PaymentTerms),
                    ClientId = clientReportRow.ClientId
                };

                fullDatabase.FullDatabase.Add(newDatabaseRow);
            }
        }

        public void UpdateDatabase(FullDatabaseModel fullDatabase, List<CenturionReportModel> data)
        {
            if (!data.Any())
            {
                return;
            }
            var uniqueCenturionReportRows =
                data
                .Select(report => report.CenturionReport)
                .Aggregate((a, b) => a.Union(b).ToList());

            foreach (var uniqueCenturionReportRow in uniqueCenturionReportRows)
            {
                var fullDbRow = fullDatabase
                    .FullDatabase
                    .FirstOrDefault(row => row.InvoiceNumber.Equals(uniqueCenturionReportRow.InvoiceNumber));

                if (fullDbRow == null)
                {
                    continue;
                }

                fullDbRow.ClientId = uniqueCenturionReportRow.ClientId;
                var payment = new PaymentDateLatencyPaid
                {
                    PaymentDate = uniqueCenturionReportRow.PaymentDate,
                    PaymentPaid = uniqueCenturionReportRow.AmountPaid
                };

                var totalDaysBetweenDueAndPaid = (uniqueCenturionReportRow.PaymentDate - fullDbRow.PaymentDueDate).TotalDays;
                payment.Latency = totalDaysBetweenDueAndPaid > 0 ? (int)totalDaysBetweenDueAndPaid : 0;


                if (fullDbRow.Payments == null)
                {
                    fullDbRow.Payments = new List<PaymentDateLatencyPaid>();
                }
                fullDbRow.Payments.Add(payment);
            }
        }

        public void OutputInvoiceNumberIssues(FullDatabaseModel fullDatabase, List<CenturionReportModel> data)
        {
            if (!data.Any())
            {
                return;
            }
            var uniqueCenturionReportRows =
                data
                .Select(report => report.CenturionReport)
                .Aggregate((a, b) => a.Union(b).ToList());

            var centurionDataWithNewInvoiceNumbers = new List<string>();

            foreach (var uniqueCenturionReportRow in uniqueCenturionReportRows)
            {
                var fullDbRow = fullDatabase
                    .FullDatabase
                    .FirstOrDefault(row => row.InvoiceNumber.Equals(uniqueCenturionReportRow.InvoiceNumber));

                if (fullDbRow == null)
                {
                    if (!centurionDataWithNewInvoiceNumbers.Any())
                    {
                        centurionDataWithNewInvoiceNumbers.Add("ClientId,InvoiceNumber,PaymentDate,AmountPaid");
                    }
                    centurionDataWithNewInvoiceNumbers.Add($"{uniqueCenturionReportRow.ClientId},{uniqueCenturionReportRow.InvoiceNumber},{uniqueCenturionReportRow.PaymentDate},{uniqueCenturionReportRow.AmountPaid}");
                }
            }

            if (centurionDataWithNewInvoiceNumbers.Any())
            {
                MessageBox.Show("Found centurion reports with invoice numbers that did not match any client report invoice numbers, exporting to error file", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                _fileSystemController.WriteToFile(centurionDataWithNewInvoiceNumbers);
            }
            else
            {
                MessageBox.Show("No issues found with invoice numbers", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
