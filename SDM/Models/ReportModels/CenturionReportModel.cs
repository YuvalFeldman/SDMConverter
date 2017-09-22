﻿using System;
using System.Collections.Generic;

namespace SDM.Models.ReportModels
{
    public class CenturionReportModel
    {
        public List<CenturionModelRow> CenturionReport { get; set; } = new List<CenturionModelRow>();
    }
    public class CenturionModelRow
    {
        public string ClientId { get; set; }

        public string InvoiceNumber { get; set; }

        public DateTime PaymentDate { get; set; }

        public int AmountPaid { get; set; }
    }
}
