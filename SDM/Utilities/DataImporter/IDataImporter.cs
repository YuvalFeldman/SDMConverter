using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabase(List<ClientModelRow> clientData);

        void UpdateDatabase(List<CenturionModelRow> centurionData);
    }
}
