using System.Collections.Generic;
using SDM.Models;

namespace SDM.Database
{
    public class Database : IDatabase
    {
        private Dictionary<string, FullDatabase> _fullDatabase;

        public Dictionary<string, FullDatabase> Get()
        {
            return _fullDatabase;
        }
    }
}
