using System;
using System.Collections.Generic;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        Tuple<List<string>, List<string>> ConvertToCsv(FullDatabaseModel data);

        Dictionary<string, List<string>> ConvertToCsv(SummedDatabaseModel data);

        Tuple<ClientLog, List<string>> ConvertCsvToClientDataModel(List<string> data, LatencyConversionModel latencyConversionModel);

        Tuple<CenturionLog, List<string>> ConvertCsvToCenturionModel(List<string> data);

        LatencyConversionModel ConvertCsvToLatencyConversionModel(List<string> data);
    }
}
