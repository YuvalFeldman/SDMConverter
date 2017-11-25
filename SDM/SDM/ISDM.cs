namespace SDM.SDM
{
    public interface ISDM
    {
        void ImportClientReport(string clientId);

        void ImportcenturionReport();

        void ExportFullDebtReport();

        void ExportSummedDebtReport();

        void DeleteClientReport();

        void DeleteCenturionReport();

        void SetLatencyConversionTable();

        void GetReportInvoiceIdIssues();
    }
}
