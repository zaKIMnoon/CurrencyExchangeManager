namespace CurrencyExchangeManagerLib.Connections
{
    public class DatabaseConfig
    {
        public string? ConnectionString { get; set; }
        public static DatabaseConfig NewDatabaseConfig(string connectionString)
        {
            return new() { ConnectionString = connectionString };
        }
    }
}
