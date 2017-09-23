using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string LateBelow30 = "Late<30,30<Late<60,60<Late<90,90<Late";
        private const string LateBelow60 = "Late<30,30<Late<60,60<Late<90,90<Late";
        private const string LateBelow90 = "Late<30,30<Late<60,60<Late<90,90<Late";
        private const string LateAbove90 = "Late<30,30<Late<60,60<Late<90,90<Late";

        public List<string> ConvertToCsv(FullDatabaseModel data)
        {
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
                let databaseRowString = $"{databaseRow.ClientId},{databaseRow.InvoiceNumber},{databaseRow.PaymentDue},{databaseRow.PaymentDueDate}"
                select databaseRow.Payments
                .Aggregate(databaseRowString, (current, paymentDateLatencyPaid) => $"{current},{paymentDateLatencyPaid.PaymentDate},{paymentDateLatencyPaid.PaymentPaid},{paymentDateLatencyPaid.Latency}"));

            return csvData;
        }

        public Dictionary<string, List<string>> ConvertToCsv(SummedDatabaseModel data)
        {
            var csvsByPartner = new Dictionary<string, List<string>>();
            foreach (var summedDatabasePartner in data.SummedDatabase)
            {
                var csvData = new List<string>{$"{InvoiceNumber},{PaymentDue},{PaymentPaid},{LateBelow30},{LateBelow60},{LateBelow90},{LateAbove90}"};

                var sortedDates = summedDatabasePartner
                    .Value
                    .SummedDbPerDate
                    .Keys
                    .ToList()
                    .OrderBy(element => element.Date);

                foreach (var partnerDate in sortedDates)
                {
                    var currentDateData = summedDatabasePartner.Value.SummedDbPerDate[partnerDate];

                    csvData.Add($"{partnerDate.Month}-{partnerDate.Year}");
                    csvData.AddRange(currentDateData.Select(summedDatabaseRow => $"{summedDatabaseRow.InvoiceNumber},{summedDatabaseRow.PaymentDue},{summedDatabaseRow.PaymentPaid},{summedDatabaseRow.PaidBelow30},{summedDatabaseRow.PaidOver30Below60},{summedDatabaseRow.PaidOver60Below90},{summedDatabaseRow.PaidOver90}"));
                    csvData.Add($"total due: {currentDateData.Sum(payments => payments.PaymentDue)}, total paid: {currentDateData.Sum(payments => payments.PaymentPaid)}");
                }

                csvsByPartner.Add(summedDatabasePartner.Key, csvData);
            }

            return csvsByPartner;
        }

        public ClientReportModel ConvertCsvToClientDataModel(List<string> data, LatencyConversionModel latencyConversionModel)
        {
            var clientReport = new ClientReportModel();
            data.RemoveAt(0);
            clientReport.ClientReport = data
                .Select(line => line.Split(','))
                .Select(lineParams => new ClientModelRow
                {
                    InvoiceNumber = latencyConversionModel.LatencyConversionTable.ContainsKey(lineParams[20]) && 
                                    latencyConversionModel.LatencyConversionTable[lineParams[20]].ContainsKey(int.Parse(lineParams[15])) ?
                                    latencyConversionModel.LatencyConversionTable[lineParams[20]][int.Parse(lineParams[15])] :
                                    int.Parse(lineParams[15]),
                    InvoiceDate = DateTime.Parse(lineParams[19]),
                    AmountDue = int.Parse(lineParams[16]),
                    PaymentTerms = int.Parse(lineParams[14]),
                    ClientId = lineParams[20]
                })
                .ToList();

            return clientReport;
        }

        public CenturionReportModel ConvertCsvToCenturionModel(List<string> data)
        {
            var centurionReport = new CenturionReportModel();
            data.RemoveAt(0);
            centurionReport.CenturionReport = data
                .Select(line => line.Split(','))
                .Select(lineParams => new CenturionModelRow
                {
                    InvoiceNumber = lineParams[3],
                    PaymentDate = DateTime.Parse(lineParams[5]),
                    ClientId = lineParams[0],
                    AmountPaid = int.Parse(lineParams[7])
                })
                .ToList();

            return centurionReport;
        }

        public LatencyConversionModel ConvertCsvToLatencyConversionModel(List<string> data)
        {
            var latencyConversionModel = new LatencyConversionModel();
            var conversionHeader = data[0].Split(',');
            data.RemoveAt(0);

            foreach (var latencyRow in data)
            {
                var splitRow = latencyRow.Split(',');
                var clientId = splitRow[0];
                var conversionRow = new Dictionary<int, int>();
                for (var i = 1; i < conversionHeader.Length; i++)
                {
                    conversionRow.Add(int.Parse(conversionHeader[i]), int.Parse(splitRow[i]));
                }

                latencyConversionModel.LatencyConversionTable.Add(clientId, conversionRow);
            }

            return latencyConversionModel;
        }
    }
}
