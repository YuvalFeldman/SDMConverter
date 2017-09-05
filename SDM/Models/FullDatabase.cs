using System;
using System.Collections.Generic;

namespace SDM.Models
{
    public class FullDatabase
    {
        public string ClientId { get; set; }
        public string InvoiceNumber { get; set; }
        public int PaymentDue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public List<PaymentDateLatencyPaid> Payments { get; set; }
    }

    public class PaymentDateLatencyPaid
    {
        public DateTime PaymentDate { get; set; }
        public int PaymentPaid { get; set; }
        public int Latency { get; set; }
    }
}
