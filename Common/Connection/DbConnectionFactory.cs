using Common.Adapters;

namespace Common.Connection
{
    public class DbConnectionFactory
    {
        private readonly IDictionary<string, IDatabaseAdapter> _adapters;
        private DatabaseConfig _config;
        public DbConnectionFactory(DatabaseConfig config)
        {
            _config = config;
            _adapters = new Dictionary<string, IDatabaseAdapter>
                        {
                            { "mysql", new MySqlDatabaseAdapter(config) },
                            { "postgresql", new PostgreSQLDatabaseAdapter(config) },
                            { "mssql", new SqlServerDatabaseAdapter(config) },
                            { "mongodb", new MongoDBDatabaseAdapter(config) }
                        };
        }

        public IDatabaseAdapter GetAdapter(string dbType)
        {
            if (_adapters.ContainsKey(dbType.ToLower()))
            {
                return _adapters[dbType.ToLower()];
            }
            throw new InvalidOperationException("Unsupported database type.");
        }
    }
}
