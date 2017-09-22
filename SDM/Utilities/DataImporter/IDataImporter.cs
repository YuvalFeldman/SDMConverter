using System.Collections.Generic;
using SDM.Models;
using SDM.Models.ReportModels;

namespace SDM.Utilities.DataImporter
{
    public interface IDataImporter
    {
        void UpdateDatabase(ClientReportModel data);

        void UpdateDatabase(CenturionReportModel data);
    }
}
