﻿using System.Collections.Generic;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.DAL.logsDal
{
    public interface ISdmlogsDal
    {
        List<string> GetLogNames(ReportTypes reportType);
        string ImportReport(ReportTypes reportType);
        void DeleteReport(ReportTypes reportType, string reportName);
        CenturionLog GetCenturionLog(string reportName);
        ClientLog GetClientLog(string reportName, LatencyConversionModel conversionModel);
        LatencyConversionModel GetReportLatencyLog(string reportName);
        void AddClientIdToClientReport(string reportName, string id);
    }
}
