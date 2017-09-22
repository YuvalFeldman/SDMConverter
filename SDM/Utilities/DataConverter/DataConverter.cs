using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Models.Enums;
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
        private const string InvoiceDate = "Invoice date";
        private const string PaymentTerms = "Payment terms";
        private const string AmountDue = "Amount due";
        private const string AmountPaid = "Amount paid";

        //public List<ClientModelRow> ConvertCsvToClientDataModel(List<string> data)
        //{
        //    var header = data[0].Split(',').ToList();
        //    var invoiceNumberPlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceNumber, StringComparison.OrdinalIgnoreCase));
        //    var invoiceDatePlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceDate, StringComparison.OrdinalIgnoreCase));
        //    var paymentTermsPlacement = header.FindIndex(headerItem => string.Equals(headerItem, PaymentTerms, StringComparison.OrdinalIgnoreCase));
        //    var amountDuePlacement = header.FindIndex(headerItem => string.Equals(headerItem, AmountDue, StringComparison.OrdinalIgnoreCase));

        //    data.RemoveAt(0);
        //    var clientModelList = data
        //        .Select(line => line.Split(','))
        //        .Select(lineParams => new ClientModelRow
        //        {
        //            InvoiceNumber = lineParams[invoiceNumberPlacement],
        //            InvoiceDate = DateTime.Parse(lineParams[invoiceDatePlacement]),
        //            AmountDue = int.Parse(lineParams[amountDuePlacement]),
        //            PaymentTerms = int.Parse(lineParams[paymentTermsPlacement])
        //        })
        //        .ToList();

        //    return clientModelList;
        //}

        //public List<CenturionModelRow> ConvertCsvToCenturionModel(List<string> data)
        //{
        //    var header = data[0].Split(',').ToList();
        //    var clientIdPlacement = header.FindIndex(headerItem => string.Equals(headerItem, ClientId, StringComparison.OrdinalIgnoreCase));
        //    var invoiceNumberPlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceNumber, StringComparison.OrdinalIgnoreCase));
        //    var paymentDatePlacement = header.FindIndex(headerItem => string.Equals(headerItem, PaymentDate, StringComparison.OrdinalIgnoreCase));
        //    var amountPaidPlacement = header.FindIndex(headerItem => string.Equals(headerItem, AmountPaid, StringComparison.OrdinalIgnoreCase));

        //    data.RemoveAt(0);
        //    var clientModelList = data
        //        .Select(line => line.Split(','))
        //        .Select(lineParams => new CenturionModelRow { InvoiceNumber = lineParams[invoiceNumberPlacement], PaymentDate = DateTime.Parse(lineParams[paymentDatePlacement]), ClientId = lineParams[clientIdPlacement], AmountPaid = int.Parse(lineParams[amountPaidPlacement])})
        //        .ToList();

        //    return clientModelList;
        //}

        public List<string> ConvertFullDatabaseToCsv(FullDatabseModel data)
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

        public Dictionary<string, List<string>> ConvertSummedDatabaseToCsv(SummedDatabaseModel data)
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
                    csvData.Add($"{currentDateData.InvoiceNumber},{currentDateData.PaymentDue},{currentDateData.PaymentPaid},{currentDateData.PaidBelow30},{currentDateData.PaidOver30Below60},{currentDateData.PaidOver60Below90},{currentDateData.PaidOver90}");
                }

                csvsByPartner.Add(summedDatabasePartner.Key, csvData);
            }

            return csvsByPartner;
        }

        public ClientReportModel ConvertCsvToClientDataModel(List<string> data)
        {
            var clientReport = new ClientReportModel();
            data.RemoveAt(0);
            foreach (var clientReportRow in data)
            {
                //TODO: get data from client report
            }

            return clientReport;
        }

        public CenturionReportModel ConvertCsvToCenturionModel(List<string> data)
        {
            var centurionReport = new CenturionReportModel();
            data.RemoveAt(0);
            foreach (var centurionReportRow in data)
            {
                //TODO: get data from centurion report
            }

            return centurionReport;
        }
    }
}
