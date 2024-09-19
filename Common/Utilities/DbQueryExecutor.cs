using Common.Adapters;

namespace Common.Utilities
{
    public class DbQueryExecutor
    {
        private readonly IDatabaseAdapter _adapter;

        public DbQueryExecutor(IDatabaseAdapter adapter)
        {
            _adapter = adapter;
        }

        public async Task<IEnumerable<T>> ExecuteSelect<T>(string query, object parameters = null)
        {
            return await _adapter.QueryAsync<T>(query, parameters);
        }

        public async Task<int> ExecuteCommand(string query, object parameters = null)
        {
            return await _adapter.ExecuteAsync(query, parameters);
        }
    }
}
