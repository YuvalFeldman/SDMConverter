using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDM.Models.LatencyConversionModel
{
    public class LatencyConversionModel
    {
        public Dictionary<string, Dictionary<int, int>> LatencyConversionTable { get; set; } = new Dictionary<string, Dictionary<int, int>>();
    }
}
