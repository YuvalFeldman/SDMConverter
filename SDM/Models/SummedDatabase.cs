using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDM.Models
{
    public class SummedDbPartner
    {
        public string ClientName { get; set; }
        public Dictionary<MonthEnum, SummedDatabase> SummedDbPerMonth { get; set; }
    }

    public class SummedDatabase
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
