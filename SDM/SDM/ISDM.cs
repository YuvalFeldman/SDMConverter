namespace SDM.SDM
{
    public interface ISDM
    {
        void ImportClientReport();

        void ImportcenturionReport();

        void ExportFullDebtReport();

        void ExportSummedDebtReport();

        void DeleteClientReport();

        void DeleteCenturionReport();
    }
}
