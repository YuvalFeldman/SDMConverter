using System.Collections.Generic;
using SDM.Models;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        List<FullDatabase> ConvertCsvToFullDatabase(List<string> data);

        List<string> ConvertFullDatabaseToCsv(List<FullDatabase> data);

        List<FullDatabase> ConvertCsvToClientDataModel(List<string> data);

        List<string> ConvertClientDataModelToCsv(List<FullDatabase> data);

        List<FullDatabase> ConvertCsvToCenturionModel(List<string> data);

        List<string> ConvertCenturionModelToCsv(List<FullDatabase> data);

        List<string> ConvertSummedDatabaseToCsv(List<SummedDbPartner> data);
    }
}
