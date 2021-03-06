﻿using System;
using System.Collections.Generic;

namespace SDM.Models.ReportModels
{
    public class ClientLog
    {
        public List<ClientModelRow> ClientReport { get; set; } = new List<ClientModelRow>();
    }
    public class ClientModelRow
    {
        public string ClientId { get; set; }

        public int InvoiceNumber { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int PaymentTerms { get; set; }

        public float AmountDue { get; set; }

        public int CompanyNumber { get; set; }
        public int ClientNumber { get; set; }
    }
}
