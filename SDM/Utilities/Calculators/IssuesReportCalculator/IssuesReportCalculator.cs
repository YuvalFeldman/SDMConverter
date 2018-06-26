using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Forms;
using SDM.DAL.logsDal;
using SDM.Models.ReportModels;

namespace SDM.Utilities.Calculators.IssuesReportCalculator
{
    public class IssuesReportCalculator : IIssuesReportCalculator
    {
        private readonly ISdmlogsDal _logDal;

        public IssuesReportCalculator(ISdmlogsDal logDal)
        {
            _logDal = logDal;
        }

        public List<string> GetReportIssues(List<string> clientLogNames, List<string> additionalIssues, string latencyTable = null)
        {
            var latencyConversionTable = _logDal.GetReportLatencyLog(latencyTable);
            var clientLogs = clientLogNames.Select(logName => _logDal.GetClientLog(logName, latencyConversionTable)).Select(x => x.Item1).ToList();

            var duplicateClientReportInvoiceRow = GetDuplicateClientReportInvoiceRows(clientLogs);
            var differentClientNumberRowsWithSameCompanyNumber = GetClientRowsWithDifferentClientNumberForTheSameCompanyNumber(clientLogs);

            var issuesList = GetIssuesList(duplicateClientReportInvoiceRow,
                differentClientNumberRowsWithSameCompanyNumber, additionalIssues);

            return issuesList;
        }

        private Dictionary<int, List<ClientModelRow>> GetDuplicateClientReportInvoiceRows(List<ClientLog> clientLogs)
        {
            var clientColumnsByInvoiceNumber = new Dictionary<int, List<ClientModelRow>>();
            foreach (var clientLog in clientLogs)
            {
                foreach (var clientModelRow in clientLog.ClientReport)
                {
                    if (clientColumnsByInvoiceNumber.ContainsKey(clientModelRow.InvoiceNumber))
                    {
                        clientColumnsByInvoiceNumber[clientModelRow.InvoiceNumber].Add(clientModelRow);
                    }
                    else
                    {
                        clientColumnsByInvoiceNumber.Add(clientModelRow.InvoiceNumber, new List<ClientModelRow> { clientModelRow });
                    }
                }
            }

            var duplicateInvoiceRows = clientColumnsByInvoiceNumber.Where(x => x.Value.Count > 1).ToDictionary(k => k.Key, v => v.Value);

            return duplicateInvoiceRows;
        }

        private Dictionary<int, List<ClientModelRow>> GetClientRowsWithDifferentClientNumberForTheSameCompanyNumber(List<ClientLog> clientLogs)
        {
            var clientRowsByCompanyNumber = new Dictionary<int, List<ClientModelRow>>();
            foreach (var clientLog in clientLogs)
            {
                foreach (var clientModelRow in clientLog.ClientReport)
                {
                    if (clientRowsByCompanyNumber.ContainsKey(clientModelRow.CompanyNumber))
                    {
                        clientRowsByCompanyNumber[clientModelRow.CompanyNumber].Add(clientModelRow);
                    }
                    else
                    {
                        clientRowsByCompanyNumber.Add(clientModelRow.CompanyNumber, new List<ClientModelRow> { clientModelRow });
                    }
                }
            }

            var clientRowsWithDifferences = new Dictionary<int, List<ClientModelRow>>();
            foreach (var clientRowKvp in clientRowsByCompanyNumber)
            {
                foreach (var clientModelRow in clientRowKvp.Value)
                {
                    if (!clientRowsWithDifferences.ContainsKey(clientRowKvp.Key) && clientRowKvp.Value.Any(x => x.ClientNumber != clientModelRow.ClientNumber))
                    {
                        clientRowsWithDifferences.Add(clientRowKvp.Key, clientRowKvp.Value);
                    }
                }
            }

            return clientRowsWithDifferences;
        }

        private List<string> GetIssuesList(Dictionary<int, List<ClientModelRow>> duplicateClientReportInvoiceRow,
            Dictionary<int, List<ClientModelRow>> differentClientNumberRowsWithSameCompanyNumber, List<string> additionalIssues)
        {
            var issuesList = new List<string>();

            if (duplicateClientReportInvoiceRow.Any())
            {
                issuesList.Add("Client Report duplicate invoice numbers:");
                issuesList.AddRange(duplicateClientReportInvoiceRow.Select(x => $"Invoice number: {x.Key}, repetitions: {x.Value.Count}"));
            }

            if (differentClientNumberRowsWithSameCompanyNumber.Any())
            {
                issuesList.Add(string.Empty);
                issuesList.Add("Client Report different client numbers with same company numbers:");
                issuesList.AddRange(differentClientNumberRowsWithSameCompanyNumber.Select(x => $"Company number: {x.Key}, clientNumbers: {string.Join(",", x.Value.Select(y => y.ClientNumber))}"));
            }

            var filteredAdditionalIssues = additionalIssues.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (filteredAdditionalIssues.Any())
            {
                issuesList.Add(string.Empty);
                issuesList.Add("Additional issues encountered:");
                issuesList.AddRange(filteredAdditionalIssues);
            }

            return issuesList;
        }
    }
}
