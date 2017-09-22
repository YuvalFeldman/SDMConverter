using System;
using System.Collections.Generic;
using SDM.Models.Enums;

namespace SDM.Models.ReportModels
{
    public class SummedDatabaseModel
    {
        public Dictionary<string, SummedDatabasePartner> SummedDatabase { get; set; } = new Dictionary<string, SummedDatabasePartner>();
    }
    public class SummedDatabasePartner
    {
        public string ClientName { get; set; }
        public Dictionary<DateTime, SummedDatabaseRow> SummedDbPerDate { get; set; }
    }
    public class SummedDatabaseRow
    {
        public MonthEnum Month { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime PaymentDue { get; set; }
        public string PaymentPaid { get; set; }
        public int PaidBelow30 { get; set; }
        public int PaidOver30Below60 { get; set; }
        public int PaidOver60Below90 { get; set; }
        public int PaidOver90 { get; set; }
    }
}
