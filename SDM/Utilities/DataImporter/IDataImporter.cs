using System.Collections.Generic;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        List<List<string>> GetDbUpdatedWithClientData(List<List<string>> currentDb, List<List<string>> clientData);

        List<List<string>> GetDbUpdatedWithcenturionDebtCollection(List<List<string>> currentDb, List<List<string>> centurionDebtCollection);
    }
}
