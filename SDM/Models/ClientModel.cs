using System;

namespace SDM.Models
{
    public class ClientModel
    {
        public string InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PaymentTerms { get; set; }

        public int AmountDue { get; set; }
    }
}
