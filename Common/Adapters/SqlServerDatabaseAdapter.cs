using Common.Connection;
using Microsoft.Data.SqlClient;

namespace Common.Adapters
{
    public class SqlServerDatabaseAdapter : IDatabaseAdapter
    {
        private DatabaseConfig _config;
        public SqlServerDatabaseAdapter(DatabaseConfig config)
        {
            _config = config;
        }

        public string BuildConnectionString(DatabaseConfig config)
        {
            return $"Server={config.Server};Database={config.Database};User Id={config.User};Password={config.Password};";
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(BuildConnectionString(_config)))
            {
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = new SqlConnection(BuildConnectionString(_config)))
            {
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<string>> CheckConnectionAsync(DatabaseConfig config)
        {
            string connectionString = $"Server={config.Server};User Id={config.User};Password={config.Password};";
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var result = await connection.QueryAsync<string>("SELECT name FROM sys.databases;");
                return result.ToList();
            }
        }

        public async Task<bool> CheckDbConnectionAsync(DatabaseConfig config)
        {
            using (var connection = new SqlConnection(BuildConnectionString(config)))
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
