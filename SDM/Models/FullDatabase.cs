using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDM.Models
{
    public class FullDatabase
    {
        public string ClientId { get; set; }
        public string InvoiceNumber { get; set; }
        public string PaymentDue { get; set; }
        public string PaymentDueDate { get; set; }
        public List<PaymentDateLatencyPaid> Payments { get; set; }
    }

    public class PaymentDateLatencyPaid
    {
        public DateTime PaymentDate { get; set; }
        public int PaymentPaid { get; set; }
        public int Invoice { get; set; }
    }
}
