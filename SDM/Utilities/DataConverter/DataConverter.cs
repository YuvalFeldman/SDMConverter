using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataConverter
{
    public class DataConverter : IDataConverter
    {
        private const string ClientId = "Client id";
        private const string InvoiceNumber = "Invoice number";
        private const string PaymentDue = "Payment due";
        private const string PaymentDueDate = "Payment due date";
        private const string PaymentDate = "Payment date";
        private const string PaymentPaid = "Payment paid";
        private const string Latency = "Latency";
        private const string LateBelow30 = "Late<30";
        private const string LateBelow60 = "30<Late<60";
        private const string LateBelow90 = "60<Late<90";
        private const string LateAbove90 = "90<Late";

        public List<string> ConvertToCsv(FullDatabaseModel data)
        {
            try
            {
                if (data == null || !data.FullDatabase.Any())
                {
                    return new List<string>();
                }
                var csvData = new List<string>();

                var header = $"{ClientId},{InvoiceNumber},{PaymentDue},{PaymentDueDate}";
                var payments = Math.Ceiling((double)data.FullDatabase.Max(row => row.Payments.Count) / 3);
                for (var i = 1; i <= payments; i++)
                {
                    header = $"{header},{PaymentDate}{i},{PaymentPaid}{i},{Latency}{i}";
                }
                csvData.Add(header);
                csvData.AddRange(
                    from databaseRow
                    in data.FullDatabase
                    let databaseRowString = $"{databaseRow.ClientId},{databaseRow.InvoiceNumber},{databaseRow.PaymentDue},{databaseRow.PaymentDueDate.Day}.{databaseRow.PaymentDueDate.Month}.{databaseRow.PaymentDueDate.Year}"
                    select databaseRow.Payments
                        .Aggregate(databaseRowString, (current, paymentDateLatencyPaid) => $"{current},{paymentDateLatencyPaid.PaymentDate.Day}.{paymentDateLatencyPaid.PaymentDate.Month}.{paymentDateLatencyPaid.PaymentDate.Year},{paymentDateLatencyPaid.PaymentPaid},{paymentDateLatencyPaid.Latency}"));

                return csvData;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed converting full report to csv. InnerMessage: {e.Message}");
                throw;
            }
        }

        public Dictionary<string, List<string>> ConvertToCsv(SummedDatabaseModel summedDatabaseModel)
        {
            try
            {
                if (summedDatabaseModel == null || !summedDatabaseModel.SummedDatabase.Any())
                {
                    return new Dictionary<string, List<string>>();
                }

                return summedDatabaseModel
                    .SummedDatabase
                    .ToDictionary(k => k.Key, v => GetSummedDbPartnerCsv(v.Key, v.Value));
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed converting summed report to csv. InnerMessage: {e.Message}");
                throw;
            }
        }

        private List<string> GetSummedDbPartnerCsv(string client, SummedDatabasePartner summedDatabasePartner)
        {
            var summedPartnerReportCsv = new List<string>();
            summedPartnerReportCsv.Add($"{InvoiceNumber},{PaymentDue},{PaymentPaid},{LateBelow30},{LateBelow60},{LateBelow90},{LateAbove90}");
            summedPartnerReportCsv.Add(client);

            foreach (var yearlySummedData in summedDatabasePartner.YearlySummedDbData.OrderByDescending(k => k.Key))
            {
                foreach (var monthlySummedData in yearlySummedData.Value.MonthlySummedDbData.OrderByDescending(k => k.Key))
                {
                    summedPartnerReportCsv.Add($"{monthlySummedData.Key}-{yearlySummedData.Key}");
                    summedPartnerReportCsv.AddRange(monthlySummedData.Value.Select(monthlyRow => $"{monthlyRow.InvoiceNumber},{monthlyRow.PaymentDue},{monthlyRow.PaymentPaid},{monthlyRow.PaidBelow30},{monthlyRow.PaidOver30Below60},{monthlyRow.PaidOver60Below90},{monthlyRow.PaidOver90}"));
                    summedPartnerReportCsv.Add($"total due: {monthlySummedData.Value.Sum(payments => payments.PaymentDue)}, total paid: {monthlySummedData.Value.Sum(payments => payments.PaymentPaid)}");
                    summedPartnerReportCsv.Add(string.Empty);
                }
                summedPartnerReportCsv.Add(string.Empty);
            }

            return summedPartnerReportCsv;
        }

        public ClientLog ConvertCsvToClientDataModel(List<string> data, LatencyConversionModel latencyConversionModel)
        {
            try
            {
                if (data == null || !data.Any())
                {
                    return new ClientLog();
                }
                if (latencyConversionModel == null)
                {
                    latencyConversionModel = new LatencyConversionModel();

                }
                var clientReport = new ClientLog();
                data.RemoveAt(0);
                clientReport.ClientReport = data
                    .Select(line => LineSplitter(line).ToArray())
                    .Select(lineParams =>
                    {
                        try
                        {
                            var row = new ClientModelRow();
                            row.InvoiceNumber = latencyConversionModel.LatencyConversionTable.ContainsKey(lineParams[20]) &&
                                                latencyConversionModel.LatencyConversionTable[lineParams[20]].ContainsKey(int.Parse(lineParams[15])) ?
                                latencyConversionModel.LatencyConversionTable[lineParams[20]][int.Parse(lineParams[15])] :
                                int.Parse(lineParams[15]);
                            row.InvoiceDate = TryParsingDateTime(lineParams[19], true);
                            row.AmountDue = float.Parse(lineParams[16]);
                            row.PaymentTerms = int.Parse(lineParams[14]);
                            row.ClientId = lineParams[20];
                            return row;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show($"Error parsing content from client file in row: {string.Join(",", lineParams)}. Excpetion: {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return null;
                        }
                    })
                    .Where(x => x != null)
                    .ToList();

                return clientReport;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed parsing client report. InnerMessage: {e.Message}");
                throw;
            }
        }

        private DateTime TryParsingDateTime(string date, bool dateTimeEu)
        {
            var reformatedDate = date;

            try
            {
                reformatedDate = reformatedDate.Replace('.', '/');
                reformatedDate = reformatedDate.Replace('-', '/');
                var dateSplit = reformatedDate.Split('/');

                var day = dateSplit[0].Length == 1 ? "d" : "dd";
                var month = dateSplit[1].Length == 1 ? "M" : "MM";
                var year = dateSplit[2].Length == 2 ? "yy" : "yyyy";

                var daymonthYear = dateTimeEu ? $"{day}/{month}/{year}" : $"{month}/{day}/{year}";

                return DateTime.ParseExact(reformatedDate, daymonthYear, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                MessageBox.Show($@"Could not parse date : {date} (parsed: {reformatedDate}), format is eu: {dateTimeEu}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        public CenturionLog ConvertCsvToCenturionModel(List<string> data)
        {
            if (data == null || !data.Any())
            {
                return new CenturionLog();
            }
            var centurionReport = new CenturionLog();
            data.RemoveAt(0);
            centurionReport.CenturionReport = data
                .Select(line => LineSplitter(line).ToArray())
                .Select(lineParams =>
                {
                    try
                    {
                        var row = new CenturionModelRow();
                        row.InvoiceNumber = int.Parse(lineParams[4]);
                        row.PaymentDate = TryParsingDateTime(lineParams[5], true);
                        row.ClientId = lineParams[0];
                        row.AmountPaid = float.Parse(lineParams[7]);
                        return row;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Error parsing content from centurion file in row: {string.Join(",", lineParams)}. Excpetion: {e.Message}", "Reports manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return null;
                    }
                })
                .Where(x => x != null)
                .ToList();

            return centurionReport;
        }

        public LatencyConversionModel ConvertCsvToLatencyConversionModel(List<string> data)
        {
            try
            {
                if (data == null || !data.Any())
                {
                    return new LatencyConversionModel();
                }
                var latencyConversionModel = new LatencyConversionModel();
                var conversionHeader = LineSplitter(data[0]).ToList();
                data.RemoveAt(0);

                foreach (var latencyRow in data)
                {
                    var splitRow = LineSplitter(latencyRow).ToList();
                    var clientId = splitRow[0];
                    var conversionRow = new Dictionary<int, int>();
                    for (var i = 1; i < conversionHeader.Count; i++)
                    {
                        conversionRow.Add(int.Parse(conversionHeader[i]), int.Parse(splitRow[i]));
                    }

                    latencyConversionModel.LatencyConversionTable.Add(clientId, conversionRow);
                }

                return latencyConversionModel;
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed parsing latency conversion table. InnerMessage: {e.Message}");
                throw;
            }
        }

        IEnumerable<string> LineSplitter(string line)
        {
            var fieldStart = 0;
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i] == ',')
                {
                    yield return line.Substring(fieldStart, i - fieldStart);
                    fieldStart = i + 1;
                }
                if (i == line.Length - 1)
                {
                    yield return line.Substring(fieldStart, i - fieldStart + 1);
                }
                if (line[i] != '"') continue;

                for (i++; line[i] != '"'; i++) { }
            }
        }
    }
}
