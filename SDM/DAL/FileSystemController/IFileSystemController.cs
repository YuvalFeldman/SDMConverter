﻿using System.Collections.Generic;
using SDM.Models.Enums;
using SDM.Models.LatencyConversionModel;
using SDM.Models.ReportModels;

namespace SDM.DAL.FileSystemController
{
    public interface IFileSystemController
    {
        List<ClientReportModel> ReadClientLogs(LatencyConversionModel latencyConversionModel);

        List<CenturionReportModel> ReadCenturionLogs();

        void LogData(ReportTypes reportType, string clientId = null);

        void WriteToFile(List<string> data);

        void WriteToFile(FullDatabaseModel data);

        void WriteToFile(SummedDatabaseModel data);

        void DeleteReport(ReportTypes reportType);

        LatencyConversionModel ReadLatencyConversionTable();
    }
}
