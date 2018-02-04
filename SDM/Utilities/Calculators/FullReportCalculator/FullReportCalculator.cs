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

        public FullDatabaseModel GetFullReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            try
            {
                var fullDatabaseModel = new FullDatabaseModel();

                var centurionLogs = centurionLogNames.Select(_logDal.GetCenturionLog).ToList();
                var latencyConversionTable = _logDal.GetReportLatencyLog(latencyTable);
                var clientLogs = clientLogNames.Select(logName => _logDal.GetClientLog(logName, latencyConversionTable)).ToList();

                try
                {
                    AddClientLogs(fullDatabaseModel, clientLogs);
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Failed adding client logs to full report model. InnerMessage: {e.Message}");
                    throw;
                }
                try
                {
                    AddCenturionLogs(fullDatabaseModel, centurionLogs);
                }
                catch (Exception e)
                {
                    MessageBox.Show($@"Failed adding centurions logs to full report model. InnerMessage: {e.Message}");
                    throw;
                }

                return fullDatabaseModel;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed calculating full report model. InnerMessage: {e.Message}");
                throw;
            }
        }

        public List<string> GetFullReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            var fullReportModel = GetFullReportModel(centurionLogNames, clientLogNames, latencyTable);
            var csvReport = _dataConverter.ConvertToCsv(fullReportModel);
            return csvReport;
        }

        private void AddClientLogs(FullDatabaseModel fullDatabase, List<ClientLog> clientLogs)
        {
            if (!clientLogs.Any())
            {
                return;
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
        }

        private void AddCenturionLogs(FullDatabaseModel fullDatabase, List<CenturionLog> centurionLogs)
        {
            if (!centurionLogs.Any())
            {
                return;
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
        }
    }
}
