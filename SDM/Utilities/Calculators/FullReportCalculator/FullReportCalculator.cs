using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.DAL.logsDal;
using SDM.Models.ReportModels;
using SDM.Utilities.DataConverter;

namespace SDM.Utilities.Calculators.FullReportCalculator
{
    public class FullReportCalculator : IFullReportCalculator
    {
        private readonly IDataConverter _dataConverter;
        private readonly ISdmlogsDal _logDal;

        public FullReportCalculator(IDataConverter dataConverter, ISdmlogsDal logDal)
        {
            _dataConverter = dataConverter;
            _logDal = logDal;
        }

        public Tuple<FullDatabaseModel, List<string>> GetFullReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            try
            {
                var fullDatabaseModel = new FullDatabaseModel();
                var issues = new List<string>();

                var centurionLogsAndIssues = centurionLogNames.Select(_logDal.GetCenturionLog).ToList();
                var latencyConversionTable = _logDal.GetReportLatencyLog(latencyTable);
                var clientLogsAndIssues = clientLogNames.Select(logName => _logDal.GetClientLog(logName, latencyConversionTable)).ToList();

                centurionLogsAndIssues.ForEach(x => issues.AddRange(x.Item2));
                clientLogsAndIssues.ForEach(x => issues.AddRange(x.Item2));

                try
                {
                    issues.AddRange(AddClientLogsGetBackIssues(fullDatabaseModel, clientLogsAndIssues.Select(x => x.Item1).ToList()).Where(x => !string.IsNullOrEmpty(x)));
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Failed adding client logs to full report model. InnerMessage: {e.Message}, issues cataloged in issues file.");
                    throw;
                }
                try
                {
                    issues.AddRange(AddCenturionLogsGetBackIssues(fullDatabaseModel, centurionLogsAndIssues.Select(x => x.Item1).ToList()).Where(x => !string.IsNullOrEmpty(x)));
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Failed adding centurions logs to full report model. InnerMessage: {e.Message}, issues cataloged in issues file.");
                    throw;
                }

                return new Tuple<FullDatabaseModel, List<string>>(fullDatabaseModel, issues);
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed calculating full report model. InnerMessage: {e.Message}, issues cataloged in issues file.");
                throw;
            }
        }

        public Tuple<List<string>, List<string>> GetFullReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            var fullReportModelAndIssues = GetFullReportModel(centurionLogNames, clientLogNames, latencyTable);
            var fullReport = fullReportModelAndIssues.Item1;
            var issues = fullReportModelAndIssues.Item2;

            var csvReportAndIssues = _dataConverter.ConvertToCsv(fullReport);
            var csvReport = csvReportAndIssues.Item1;
            var issues2 = csvReportAndIssues.Item2;

            return new Tuple<List<string>, List<string>>(csvReport, issues.Concat(issues2).ToList());
        }

        private List<string> AddClientLogsGetBackIssues(FullDatabaseModel fullDatabase, List<ClientLog> clientLogs)
        {
            var issues = new List<string>();
            if (!clientLogs.Any())
            {
                return issues;
            }
            var uniqueClientReportRows =
                clientLogs
                    .Select(report => report.ClientReport)
                    .Aggregate((a, b) => a.Union(b).ToList());

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

            return issues;
        }

        private List<string> AddCenturionLogsGetBackIssues(FullDatabaseModel fullDatabase, List<CenturionLog> centurionLogs)
        {
            var issues = new List<string>();

            if (!centurionLogs.Any())
            {
                return issues;
            }
            var uniqueCenturionReportRows =
                centurionLogs
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

            return issues;
        }
    }
}
