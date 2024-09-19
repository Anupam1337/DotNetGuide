using Common.Connection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Common.Adapters
{
    public class MongoDBDatabaseAdapter : IDatabaseAdapter
    {
        private DatabaseConfig _config;
        public MongoDBDatabaseAdapter(DatabaseConfig config)
        {
            _config = config;
        }
        public string BuildConnectionString(DatabaseConfig config)
        {
            return $"mongodb://{config.User}:{config.Password}@{config.Server}/{config.Database}";
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            var client = new MongoClient(BuildConnectionString(_config));
            var database = client.GetDatabase(_config.Database);
            var collection = database.GetCollection<T>(query); // In MongoDB, the query is often the collection name
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<int> ExecuteAsync(string query, object parameters = null)
        {
            var client = new MongoClient(BuildConnectionString(_config));
            var database = client.GetDatabase(_config.Database);
            var collection = database.GetCollection<BsonDocument>(query);
            await collection.InsertOneAsync(parameters.ToBsonDocument());
            return 1; // MongoDB inserts don't return rows affected
        }

        public async Task<List<string>> CheckConnectionAsync(DatabaseConfig config)
        {
            var client = new MongoClient(BuildConnectionString(config));
            var databaseNames = await client.ListDatabaseNamesAsync();
            return await databaseNames.ToListAsync();
        }

        public async Task<bool> CheckDbConnectionAsync(DatabaseConfig config)
        {
            try
            {
                var client = new MongoClient(BuildConnectionString(config));
                var databaseNames = await client.ListDatabaseNamesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to MongoDB: {ex.Message}");
                return false;
            }
        }
    }
}
