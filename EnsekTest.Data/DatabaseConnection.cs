namespace EnsekTest.Data
{
    public sealed class DatabaseConnection
    {
        public DatabaseConnection(string connectionString) => Value = connectionString;

        public string Value { get; }
    }
}