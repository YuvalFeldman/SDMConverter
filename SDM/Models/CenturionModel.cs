using System;

namespace SDM.Models
{
    public class CenturionModel
    {
        public string ClientId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime PaymentDate { get; set; }

        public int AmountPaid { get; set; }
    }
}
