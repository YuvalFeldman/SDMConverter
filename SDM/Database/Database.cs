using System.Collections.Generic;
using SDM.Models;

namespace SDM.Database
{
    public class Database : IDatabase
    {
        private Dictionary<string, FullDatabaseRow> _fullDatabase;

        public Dictionary<string, FullDatabaseRow> Get()
        {
            return _fullDatabase;
        }
    }
}
