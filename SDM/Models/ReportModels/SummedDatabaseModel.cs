using System.Collections.Generic;
using SDM.Models.Enums;

namespace SDM.Models.ReportModels
{
    public class SummedDatabaseModel
    {
        public Dictionary<string, SummedDatabasePartner> SummedDatabase { get; set; } = new Dictionary<string, SummedDatabasePartner>();
    }

    public class SummedDatabasePartner
    {
        public Dictionary<int, YearlySummedDbData> YearlySummedDbData { get; set; } = new Dictionary<int, YearlySummedDbData>();
    }

    public class YearlySummedDbData
    {
        public Dictionary<MonthEnum, List<MonthlySummedDbData>> MonthlySummedDbData { get; set; } = new Dictionary<MonthEnum, List<MonthlySummedDbData>>();
    }

    public class MonthlySummedDbData
    {
        public int InvoiceNumber { get; set; }
        public float PaymentDue { get; set; }
        public float PaymentPaid { get; set; }
        public float PaidBelow30 { get; set; }
        public float PaidOver30Below60 { get; set; }
        public float PaidOver60Below90 { get; set; }
        public float PaidOver90 { get; set; }
    }
}
