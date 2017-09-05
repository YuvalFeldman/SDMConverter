using System.Collections.Generic;
using SDM.Models;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabaseWithClientData(List<ClientModel> clientData);

        void UpdateDatabaseWithCenturionFile(List<CenturionModel> centurionData);
    }
}
