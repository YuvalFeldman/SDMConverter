using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Models;

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

        public List<string> ConvertFullDatabaseToCsv(List<FullDatabase> data)
        {
            var csvData = new List<string>();
            var header = $"{ClientId},{InvoiceNumber},{PaymentDue},{PaymentDueDate}";
            var payments = Math.Ceiling((double)(data.Max(row => row.Payments.Count) / 3));
            for (var i = 1; i <= payments; i++)
            {
                header = $"{header},{PaymentDate}{i},{PaymentPaid}{i},{Latency}{i}";
            }
            csvData.Add(header);
            csvData.AddRange(from databaseRow in data let databaseRowString = $"{databaseRow.ClientId},{databaseRow.InvoiceNumber},{databaseRow.PaymentDue},{databaseRow.PaymentDueDate}" select databaseRow.Payments.Aggregate(databaseRowString, (current, paymentDateLatencyPaid) => $"{current},{paymentDateLatencyPaid.PaymentDate},{paymentDateLatencyPaid.PaymentPaid},{paymentDateLatencyPaid.Latency}"));

            return csvData;
        }

        public List<string> ConvertSummedDatabaseToCsv(List<SummedDbPartner> data)
        {
            //todo: all partners in same file? seperate files? the list is different clients
            var csvData = new List<string> { $"{InvoiceNumber},{PaymentDue},{PaymentPaid},{LateBelow30},{LateBelow60},{LateBelow90},{LateAbove90}" };
            foreach (var databaseRow in data)
            {
                if (!databaseRow.SummedDbPerMonth.Any())
                {
                    continue;
                }
                var currentMonth = databaseRow.SummedDbPerMonth.Min(summedDb => summedDb.Value.Month);
                var databaseRowString = $"{databaseRow.ClientName} ,{currentMonth}";
                csvData.Add(databaseRowString);
                var currentPartnerMonthData = databaseRow.SummedDbPerMonth[currentMonth];
                databaseRowString = $"{currentPartnerMonthData.InvoiceNumber},{currentPartnerMonthData.PaymentDue},{currentPartnerMonthData.PaymentPaid},{currentPartnerMonthData.PaidBelow30},{currentPartnerMonthData.PaidOver30Below60},{currentPartnerMonthData.PaidOver60Below90},{currentPartnerMonthData.PaidOver90}";
                csvData.Add(databaseRowString);
            }

            return csvData;
        }

        public List<string> ConvertClientDataModelToCsv(List<ClientModel> data)
        {
            var csv = new List<string> { $"{InvoiceNumber},{InvoiceDate},{PaymentTerms},{AmountDue}" };
            csv.AddRange(data.Select(clientModel => $"{clientModel.InvoiceNumber},{clientModel.InvoiceDate},{clientModel.PaymentTerms},{clientModel.AmountDue}"));
            return csv;
        }

        public List<string> ConvertCenturionModelToCsv(List<CenturionModel> data)
        {
            var csv = new List<string> { $"{ClientId},{InvoiceNumber},{PaymentDate},{AmountPaid}" };
            csv.AddRange(data.Select(centurionModel => $"{centurionModel.ClientId},{centurionModel.InvoiceNumber},{centurionModel.PaymentDate},{centurionModel.AmountPaid}"));
            return csv;
        }

        public List<ClientModel> ConvertCsvToClientDataModel(List<string> data)
        {
            var header = data[0].Split(',').ToList();
            var invoiceNumberPlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceNumber, StringComparison.OrdinalIgnoreCase));
            var invoiceDatePlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceDate, StringComparison.OrdinalIgnoreCase));
            var paymentTermsPlacement = header.FindIndex(headerItem => string.Equals(headerItem, PaymentTerms, StringComparison.OrdinalIgnoreCase));
            var amountDuePlacement = header.FindIndex(headerItem => string.Equals(headerItem, AmountDue, StringComparison.OrdinalIgnoreCase));

            data.RemoveAt(0);
            var clientModelList = data
                .Select(line => line.Split(','))
                .Select(lineParams => new ClientModel
                {
                    InvoiceNumber = lineParams[invoiceNumberPlacement],
                    InvoiceDate = DateTime.Parse(lineParams[invoiceDatePlacement]),
                    AmountDue = int.Parse(lineParams[amountDuePlacement]),
                    PaymentTerms = int.Parse(lineParams[paymentTermsPlacement])
                })
                .ToList();

            return clientModelList;
        }

        public List<CenturionModel> ConvertCsvToCenturionModel(List<string> data)
        {
            var header = data[0].Split(',').ToList();
            var clientIdPlacement = header.FindIndex(headerItem => string.Equals(headerItem, ClientId, StringComparison.OrdinalIgnoreCase));
            var invoiceNumberPlacement = header.FindIndex(headerItem => string.Equals(headerItem, InvoiceNumber, StringComparison.OrdinalIgnoreCase));
            var paymentDatePlacement = header.FindIndex(headerItem => string.Equals(headerItem, PaymentDate, StringComparison.OrdinalIgnoreCase));
            var amountPaidPlacement = header.FindIndex(headerItem => string.Equals(headerItem, AmountPaid, StringComparison.OrdinalIgnoreCase));

            data.RemoveAt(0);
            var clientModelList = data
                .Select(line => line.Split(','))
                .Select(lineParams => new CenturionModel { InvoiceNumber = lineParams[invoiceNumberPlacement], PaymentDate = DateTime.Parse(lineParams[paymentDatePlacement]), ClientId = lineParams[clientIdPlacement], AmountPaid = int.Parse(lineParams[amountPaidPlacement])})
                .ToList();

            return clientModelList;
        }
    }
}
