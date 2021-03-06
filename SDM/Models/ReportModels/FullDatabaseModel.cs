﻿using System;
using System.Collections.Generic;

namespace SDM.Models.ReportModels
{
    public class FullDatabaseModel
    {
        public List<FullDatabaseRow> FullDatabase { get; set; } = new List<FullDatabaseRow>();
    }
    public class FullDatabaseRow
    {
        public string ClientId { get; set; }
        public int InvoiceNumber { get; set; }
        public float PaymentDue { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public List<PaymentDateLatencyPaid> Payments { get; set; } = new List<PaymentDateLatencyPaid>();
    }

    public class PaymentDateLatencyPaid
    {
        public DateTime PaymentDate { get; set; }
        public float PaymentPaid { get; set; }
        public int Latency { get; set; }
    }
}
