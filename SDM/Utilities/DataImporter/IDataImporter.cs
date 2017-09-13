using System.Collections.Generic;
using SDM.Models;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabase(List<ClientModel> clientData);

        void UpdateDatabase(List<CenturionModel> centurionData);
    }
}
