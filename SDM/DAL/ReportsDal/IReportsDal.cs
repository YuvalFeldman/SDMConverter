﻿using System.Collections.Generic;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.DAL.ReportsDal
{
    public interface IReportsDal
    {
        List<ClientLog> ReadClientLogs(LatencyConversionModel latencyConversionModel);

        List<CenturionLog> ReadCenturionLogs();

        void LogData(ReportTypes reportType, string clientId = null);

        void WriteToFile(List<string> data);

        void WriteToFile(FullDatabaseModel data);

        void WriteToFile(SummedDatabaseModel data);

        void DeleteReport(ReportTypes reportType);

        void ImportLatencyConversionTable();

        LatencyConversionModel ReadLatencyConversionTable();
    }
}