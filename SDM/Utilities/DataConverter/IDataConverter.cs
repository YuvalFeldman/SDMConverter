using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        List<string> ConvertFullDatabaseToCsv(FullDatabseModel data);

        Dictionary<string, List<string>> ConvertSummedDatabaseToCsv(SummedDatabaseModel data);

        ClientReportModel  ConvertCsvToClientDataModel(List<string> data);

        CenturionReportModel ConvertCsvToCenturionModel(List<string> data);
    }
}
