using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SDM.Models.Enums;
using SDM.Models.ReportModels;
using SDM.Utilities.Calculators.FullReportCalculator;
using SDM.Utilities.DataConverter;

namespace SDM.Utilities.Calculators.SummedReportCalculator
{
    public class SummedReportCalculator : ISummedReportCalculator
    {
        private readonly IDataConverter _dataConverter;
        private readonly IFullReportCalculator _fullReportCalculator;

        public SummedReportCalculator(IDataConverter dataConverter, IFullReportCalculator fullReportCalculator)
        {
            _dataConverter = dataConverter;
            _fullReportCalculator = fullReportCalculator;
        }

        public Tuple<SummedDatabaseModel, List<string>> GetSummedReportModel(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            try
            {
                var fullReportModel = _fullReportCalculator.GetFullReportModel(centurionLogNames, clientLogNames, latencyTable);

                var summedReportModel = ConvertFullReportToSummedReport(fullReportModel.Item1);

                return new Tuple<SummedDatabaseModel, List<string>>(summedReportModel, new List<string>());
            }
            catch (Exception e)
            {
                MessageBox.Show($@"Failed calculating summed report model. InnerMessage: {e.Message}");
                throw;
            }
        }

        public Tuple<Dictionary<string, List<string>>, List<string>> GetSummedReport(List<string> centurionLogNames, List<string> clientLogNames, string latencyTable = null)
        {
            var summedReportModel = GetSummedReportModel(centurionLogNames, clientLogNames, latencyTable);
            var csvReport = _dataConverter.ConvertToCsv(summedReportModel.Item1);
            return new Tuple<Dictionary<string, List<string>>, List<string>>(csvReport, new List<string>());
        }

        private SummedDatabaseModel ConvertFullReportToSummedReport(FullDatabaseModel fullReportModel)
        {
            var dataBaseRowsByClientId = SplitFullDbRowsByClientId(fullReportModel);

            var summedDatabase = new SummedDatabaseModel();
            foreach (var clientInFullDb in dataBaseRowsByClientId)
            {
                summedDatabase.SummedDatabase.Add(clientInFullDb.Key, GetSummedDatabasePartner(clientInFullDb.Value));
            }

            return summedDatabase;
        }

        private Dictionary<string, List<FullDatabaseRow>> SplitFullDbRowsByClientId(FullDatabaseModel fullReportModel)
        {
            var dataBaseRowsByClientId = new Dictionary<string, List<FullDatabaseRow>>();
            foreach (var fullDatabaseRow in fullReportModel.FullDatabase)
            {
                if (string.IsNullOrEmpty(fullDatabaseRow.ClientId))
                {
                    continue;
                }
                var fullDbRowClientId = fullDatabaseRow.ClientId.Replace(" ", "");
                if (dataBaseRowsByClientId.ContainsKey(fullDbRowClientId))
                {
                    dataBaseRowsByClientId[fullDbRowClientId].Add(fullDatabaseRow);
                }
                else
                {
                    dataBaseRowsByClientId.Add(fullDbRowClientId, new List<FullDatabaseRow> { fullDatabaseRow });
                }
            }

            return dataBaseRowsByClientId;
        }
        private SummedDatabasePartner GetSummedDatabasePartner(List<FullDatabaseRow> summedPartnerRows)
        {
            var summedPartner = new SummedDatabasePartner();

            summedPartner.YearlySummedDbData = SplitRowsIntoYears(summedPartnerRows)
                .ToDictionary(k => k.Key, v => GetYearlySummedDb(v.Value));

            return summedPartner;
        }

        private Dictionary<int, List<FullDatabaseRow>> SplitRowsIntoYears(List<FullDatabaseRow> summedPartnerRows)
        {
            var summedRowsSplitByYear = new Dictionary<int, List<FullDatabaseRow>>();
            foreach (var summedPartnerRow in summedPartnerRows)
            {
                var rowYear = summedPartnerRow.PaymentDueDate.Year;
                if (summedRowsSplitByYear.ContainsKey(rowYear))
                {
                    summedRowsSplitByYear[rowYear].Add(summedPartnerRow);
                }
                else
                {
                    summedRowsSplitByYear.Add(rowYear, new List<FullDatabaseRow> { summedPartnerRow });
                }
            }

            return summedRowsSplitByYear;
        }
        private YearlySummedDbData GetYearlySummedDb(List<FullDatabaseRow> yearlyRows)
        {
            var yearlySummedDb = new YearlySummedDbData();
            yearlySummedDb.MonthlySummedDbData = SplitRowsIntoMonths(yearlyRows)
                .ToDictionary(k => k.Key, v => GetMonthlySummedReport(v.Value));

            return yearlySummedDb;
        }

        private Dictionary<MonthEnum, List<FullDatabaseRow>> SplitRowsIntoMonths(List<FullDatabaseRow> yearlyRows)
        {
            var summedRowsSplitByMonth = new Dictionary<MonthEnum, List<FullDatabaseRow>>();
            foreach (var row in yearlyRows)
            {
                var rowMonth = (MonthEnum)row.PaymentDueDate.Month;
                if (summedRowsSplitByMonth.ContainsKey(rowMonth))
                {
                    summedRowsSplitByMonth[rowMonth].Add(row);
                }
                else
                {
                    summedRowsSplitByMonth.Add(rowMonth, new List<FullDatabaseRow> { row });
                }
            }

            return summedRowsSplitByMonth;
        }

        private List<MonthlySummedDbData> GetMonthlySummedReport(List<FullDatabaseRow> monthlyRows)
        {
            var monthlyReport = new List<MonthlySummedDbData>();
            foreach (var monthlySummedDbData in monthlyRows)
            {
                var monthlyDbRow = new MonthlySummedDbData();

                monthlyDbRow.InvoiceNumber = monthlySummedDbData.InvoiceNumber;
                monthlyDbRow.PaymentDue = monthlySummedDbData.PaymentDue;
                monthlyDbRow.PaymentPaid = GetPaymentsPaid(monthlySummedDbData);
                monthlyDbRow.PaidBelow30 = GetPaidBelowThirty(monthlySummedDbData);
                monthlyDbRow.PaidOver30Below60 = GetPaidBetweenThirtyAndSixty(monthlySummedDbData);
                monthlyDbRow.PaidOver60Below90 = GetPaidBetweenSixtyAndNinety(monthlySummedDbData);
                monthlyDbRow.PaidOver90 = GetPaidAboveNinety(monthlySummedDbData);

                monthlyReport.Add(monthlyDbRow);
            }

            return monthlyReport;
        }

        private float GetPaymentsPaid(FullDatabaseRow row)
        {
            return row
                .Payments
                .Sum(payment => payment.PaymentPaid);
        }

        private float GetPaidBelowThirty(FullDatabaseRow row)
        {
            return row
                .Payments
                .Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays < 30)
                .Sum(payment => payment.PaymentPaid);
        }

        private float GetPaidBetweenThirtyAndSixty(FullDatabaseRow row)
        {
            return row
                .Payments
                .Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays > 30 && (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays < 60)
                .Sum(payment => payment.PaymentPaid);
        }

        private float GetPaidBetweenSixtyAndNinety(FullDatabaseRow row)
        {
            return row
                .Payments
                .Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays > 60 && (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays < 90)
                .Sum(payment => payment.PaymentPaid);
        }

        private float GetPaidAboveNinety(FullDatabaseRow row)
        {
            return row
                .Payments
                .Where(payment => (payment.PaymentDate.AddDays(payment.Latency) - row.PaymentDueDate).TotalDays > 90)
                .Sum(payment => payment.PaymentPaid);
        }
    }
}