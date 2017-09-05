using System.Collections.Generic;
using SDM.Models;

namespace SDM.Database
{
    public interface IDatabase
    {
        Dictionary<string, FullDatabase> Get();
    }
}
