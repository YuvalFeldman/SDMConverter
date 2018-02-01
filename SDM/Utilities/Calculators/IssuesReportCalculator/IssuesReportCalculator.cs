using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.IssuesReportCalculator
{
    public class IssuesReportCalculator : IIssuesReportCalculator
    {
        public void OutputInvoiceNumberIssues(FullDatabaseModel fullDatabase, List<CenturionLog> data)
        {
            //TODO: needs the full database filled with only client reports
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

                //_reportsDal.WriteToFile(centurionDataWithNewInvoiceNumbers);
            }
            else
            {
                MessageBox.Show("No issues found with invoice numbers", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
