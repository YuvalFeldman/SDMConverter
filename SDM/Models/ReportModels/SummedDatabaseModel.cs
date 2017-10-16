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
        public Dictionary<DateTime, List<SummedDatabaseRow>> SummedDbPerDate { get; set; }
    }
    public class SummedDatabaseRow
    {
        public DateTime Month { get; set; }
        public int InvoiceNumber { get; set; }
        public float PaymentDue { get; set; }
        public float PaymentPaid { get; set; }
        public float PaidBelow30 { get; set; }
        public float PaidOver30Below60 { get; set; }
        public float PaidOver60Below90 { get; set; }
        public float PaidOver90 { get; set; }
    }
}
