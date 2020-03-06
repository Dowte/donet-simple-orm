using System.Collections.Generic;

namespace DotNetSimpleOrm
{
    public class Entity

    {
        private readonly Dictionary<string, Dictionary<string, object>> _tables;

        public Entity()
        {
            _tables = new Dictionary<string, Dictionary<string, object>>();
        }

        public void Add(string tableName, string columnName, object value)
        {
            GetTable(tableName)[columnName] = value;
        }

        public Dictionary<string, Dictionary<string, object>>.KeyCollection TableNames()
        {
            return _tables.Keys;
        }

        public object GetValue(string tableName, string columnName)
        {
            var table = GetTable(tableName);


            return table.ContainsKey(columnName) ? table[columnName] : null;
        }

        private Dictionary<string, object> GetTable(string tableName)
        {
            if (!_tables.ContainsKey(tableName))
            {
                _tables[tableName] = new Dictionary<string, object>();
            }

            return _tables[tableName];
        }
    }
}