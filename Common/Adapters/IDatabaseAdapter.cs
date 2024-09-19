using Common.Connection;

namespace Common.Adapters
{
    public interface IDatabaseAdapter
    {
        string BuildConnectionString(DatabaseConfig config);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null);
        Task<int> ExecuteAsync(string query, object parameters = null);
        Task<List<string>> CheckConnectionAsync(DatabaseConfig config);
        Task<bool> CheckDbConnectionAsync(DatabaseConfig config);
    }
}
