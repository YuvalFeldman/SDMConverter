using System.Collections.Generic;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        List<string> ConvertToCsv(FullDatabaseModel data);

        Dictionary<string, List<string>> ConvertToCsv(SummedDatabaseModel data);

        ClientReportModel  ConvertCsvToClientDataModel(List<string> data, LatencyConversionModel latencyConversionModel);

        CenturionReportModel ConvertCsvToCenturionModel(List<string> data);

        LatencyConversionModel ConvertCsvToLatencyConversionModel(List<string> data);
    }
}
