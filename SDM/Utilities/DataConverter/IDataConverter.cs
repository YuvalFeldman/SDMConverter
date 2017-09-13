using System.Collections.Generic;
using SDM.Models;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        List<string> ConvertFullDatabaseToCsv(List<FullDatabaseRow> data);

        List<string> ConvertSummedDatabaseToCsv(List<SummedDatabasePartner> data);

        List<ClientModel> ConvertCsvToClientDataModel(List<string> data);

        List<string> ConvertClientDataModelToCsv(List<ClientModel> data);

        List<CenturionModel> ConvertCsvToCenturionModel(List<string> data);

        List<string> ConvertCenturionModelToCsv(List<CenturionModel> data);

    }
}
