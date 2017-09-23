using System;
using System.Collections.Generic;

namespace SDM.Models.ReportModels
{
    public class ClientReportModel
    {
        public List<ClientModelRow> ClientReport { get; set; } = new List<ClientModelRow>();
    }
    public class ClientModelRow
    {
        public string ClientId { get; set; }

        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PaymentTerms { get; set; }

        public int AmountDue { get; set; }
    }
}
