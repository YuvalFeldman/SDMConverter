using System.Collections.Generic;

namespace SDM.Models
{
    public class SummedDatabasePartner
    {
        public string ClientName { get; set; }
        public Dictionary<MonthEnum, SummedDatabaseRow> SummedDbPerMonth { get; set; }
    }

    public class SummedDatabaseRow
    {
        public MonthEnum Month { get; set; }
        public string InvoiceNumber { get; set; }
        public string PaymentDue { get; set; }
        public string PaymentPaid { get; set; }
        public int PaidBelow30 { get; set; }
        public int PaidOver30Below60 { get; set; }
        public int PaidOver60Below90 { get; set; }
        public int PaidOver90 { get; set; }
    }
}
