using Common.Connection;
using MySqlConnector;

namespace Common.Adapters
{
    public class MySqlDatabaseAdapter : IDatabaseAdapter
    {
        private readonly DatabaseConfig _config;

        public MySqlDatabaseAdapter(DatabaseConfig config)
        {
            _config = config;
        }

        public string BuildConnectionString(DatabaseConfig config)
        {
            return $"Server={_config.Server};Database={_config.Database};User={_config.User};Password={_config.Password};";
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            using (var connection = new MySqlConnection(BuildConnectionString(_config)))
            {
                return await connection.QueryAsync<T>(query, parameters);
            }
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            using (var connection = new MySqlConnection(BuildConnectionString(_config)))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task<List<string>> CheckConnectionAsync(DatabaseConfig config)
        {
            var databases = new List<string>();
            string connectionString = $"Server={config.Server};User={config.User};Password={config.Password};";

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var query = "SHOW DATABASES";
                var result = await connection.QueryAsync<string>(query);
                databases.AddRange(result);
            }
            return databases;
        }

        public async Task<bool> CheckDbConnectionAsync(DatabaseConfig config)
        {
            using (var connection = new MySqlConnection(BuildConnectionString(config)))
            {
                try
                {
                    await connection.OpenAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to MySQL: {ex.Message}");
                    return false;
                }
            }
        }
    }
}
