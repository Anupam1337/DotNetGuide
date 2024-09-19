namespace Common.QueryProviders
{
    public static class QueryProvider
    {
        public static string InsertQuery(string tableName, object data)
        {
            var columns = string.Join(",", data.GetType().GetProperties().Select(p => p.Name));
            var values = string.Join(",", data.GetType().GetProperties().Select(p => "@" + p.Name));
            return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        }

        public static string SelectAllQuery(string tableName)
        {
            return $"SELECT * FROM {tableName}";
        }

        public static string CreateTableQuery(string tableName, string columns)
        {
            return $"CREATE TABLE {tableName} ({columns})";
        }

        // Add other DDL and DML queries as needed
    }
}
