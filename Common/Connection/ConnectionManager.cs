using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Connection
{
    public class ConnectionManager
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ConnectionManager(DatabaseConfig config)
        {
            _connectionFactory = new DbConnectionFactory(config);
        }

        public async Task<List<string>> CheckConnectionWithoutDb(DatabaseConfig config)
        {
            var adapter = _connectionFactory.GetAdapter(config.DbType);
            return await adapter.CheckConnectionAsync(config);
        }

        public async Task<bool> CheckConnectionWithDb(DatabaseConfig config)
        {
            var adapter = _connectionFactory.GetAdapter(config.DbType);
            return await adapter.CheckDbConnectionAsync(config);
        }
    }
}
