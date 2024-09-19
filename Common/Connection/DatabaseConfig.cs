namespace Common.Connection
{
    public class DatabaseConfig
    {
        public string DbType { get; set; }  // mysql, postgresql, mssql, mongodb
        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public int MaxConnections { get; set; } = 5;  // Default value for max connections
    }

}
