using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataConverter
{
    public interface IDataConverter
    {
        List<string> ConvertFullDatabaseToCsv(List<FullDatabaseRow> data);

        List<string> ConvertSummedDatabaseToCsv(List<SummedDatabasePartner> data);

        List<ClientModelRow> ConvertCsvToClientDataModel(List<string> data);

        List<string> ConvertClientDataModelToCsv(List<ClientModelRow> data);

        List<CenturionModelRow> ConvertCsvToCenturionModel(List<string> data);

        List<string> ConvertCenturionModelToCsv(List<CenturionModelRow> data);

    }
}
