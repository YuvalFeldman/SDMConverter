using System;
using System.Collections.Generic;
using System.Linq;
using SDM.Models;

namespace SDM.Utilities.DataConverter
{
    public class DataConverter : IDataConverter
    {
        public List<FullDatabase> ConvertCsvToFullDatabase(List<string> data)
        {
            var data2DArray = data.Select(line => line.Split(',').ToList()).ToList();
            data2DArray.RemoveAt(0);

            var fullDb = new List<FullDatabase>();
            foreach (var dataRow in data2DArray)
            {
                var fullDbRow = new FullDatabase
                {
                    ClientId = dataRow[0],
                    InvoiceNumber = dataRow[1],
                    PaymentDue = dataRow[2],
                    PaymentDueDate = dataRow[3]
                };
                if (dataRow.Count > 4)
                {
                    var paymentDateLatencyPaidList = new List<PaymentDateLatencyPaid>();
                    for (var i = 4; i < dataRow.Count; i += 3)
                    {
                        var paymentDateLatencyPaid = new PaymentDateLatencyPaid
                        {
                            PaymentDate = DateTime.Parse(dataRow[i]),
                            PaymentPaid = int.Parse(dataRow[i + 1]),
                            Invoice = int.Parse(data[i + 2])
                        };

                        paymentDateLatencyPaidList.Add(paymentDateLatencyPaid);
                    }

                    fullDbRow.Payments = paymentDateLatencyPaidList;
                }

                fullDb.Add(fullDbRow);
            }

            return fullDb;
        }

        public List<string> ConvertFullDatabaseToCsv(List<FullDatabase> data)
        {
            var csvData = new List<string>();
            throw new System.NotImplementedException();
        }

        public List<string> ConvertSummedDatabaseToCsv(List<SummedDbPartner> data)
        {
            throw new System.NotImplementedException();
        }

        public List<FullDatabase> ConvertCsvToClientDataModel(List<string> data)
        {
            throw new System.NotImplementedException();
        }

        public List<string> ConvertClientDataModelToCsv(List<FullDatabase> data)
        {
            throw new System.NotImplementedException();
        }

        public List<FullDatabase> ConvertCsvToCenturionModel(List<string> data)
        {
            throw new System.NotImplementedException();
        }

        public List<string> ConvertCenturionModelToCsv(List<FullDatabase> data)
        {
            throw new System.NotImplementedException();
        }
    }
}
