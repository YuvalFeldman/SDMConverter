using System.Collections.Generic;

namespace SDM.Models.LatencyConversionModel
{
    public class LatencyConversionModel
    {
        public Dictionary<string, Dictionary<int, int>> LatencyConversionTable { get; set; } = new Dictionary<string, Dictionary<int, int>>();
    }
}
