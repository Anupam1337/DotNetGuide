using Common.Connection;
using Npgsql;

namespace Common.Adapters
{
    public class PostgreSQLDatabaseAdapter : IDatabaseAdapter
    {
        private DatabaseConfig _config;

        public PostgreSQLDatabaseAdapter(DatabaseConfig config)
        {
            _config = config;
        }

        public string BuildConnectionString(DatabaseConfig config)
        {
            return $"Host={config.Server};Database={config.Database};Username={config.User};Password={config.Password}";
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString(_config)))
            {
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString(_config)))
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<string>> CheckConnectionAsync(DatabaseConfig config)
        {
            string connectionString = $"Host={config.Server};Username={config.User};Password={config.Password}";
            using (var connection = new NpgsqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<string>("SELECT datname FROM pg_database;");
                return result.ToList();
            }
        }

        public async Task<bool> CheckDbConnectionAsync(DatabaseConfig config)
        {
            using (var connection = new NpgsqlConnection(BuildConnectionString(config)))
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
