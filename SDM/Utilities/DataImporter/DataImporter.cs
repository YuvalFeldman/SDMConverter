using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Database;
using SDM.Models;

namespace SDM.Utilities.DataImporter
{
    public class DataImporter : IDataImporter
    {
        private readonly IDatabase _database;

        public DataImporter(IDatabase database)
        {
            _database = database;
        }

        public void UpdateDatabase(List<ClientModel> clientData)
        {
            var database = _database.Get();
            var newInvoiceFilteredClientData = clientData.Where(dataRow => !database.ContainsKey(dataRow.InvoiceNumber));

            foreach (var clientModel in newInvoiceFilteredClientData)
            {
                var fullDbRow = new FullDatabaseRow
                {
                    InvoiceNumber = clientModel.InvoiceNumber,
                    PaymentDue = clientModel.AmountDue,
                    PaymentDueDate = new DateTime(
                            clientModel.InvoiceDate.Year,
                            clientModel.InvoiceDate.Month,
                            DateTime.DaysInMonth(clientModel.InvoiceDate.Year, clientModel.InvoiceDate.Month)).
                        AddDays(clientModel.PaymentTerms)
                };

                database.Add(clientModel.InvoiceNumber, fullDbRow);
            }
        }

        public void UpdateDatabase(List<CenturionModel> centurionData)
        {
            var database = _database.Get();
            var newInvoiceFilteredCenturionData = centurionData.Where(dataRow => !database.ContainsKey(dataRow.InvoiceNumber));
            //todo: export the new invoice numbers to an issue file

            var existingInvoiceFilteredCenturionData = centurionData.Where(dataRow => database.ContainsKey(dataRow.InvoiceNumber));

            foreach (var centurionModel in existingInvoiceFilteredCenturionData)
            {
                var fullDbRow = database[centurionModel.InvoiceNumber];
                fullDbRow.ClientId = centurionModel.ClientId;
                var payment = new PaymentDateLatencyPaid
                {
                    PaymentDate = centurionModel.PaymentDate,
                    PaymentPaid = centurionModel.AmountPaid
                };

                var totalDaysBetweenDueAndPaid = (centurionModel.PaymentDate - fullDbRow.PaymentDueDate).TotalDays;
                payment.Latency = totalDaysBetweenDueAndPaid > 0 ? (int)totalDaysBetweenDueAndPaid : 0;
            }
        }
    }
}
