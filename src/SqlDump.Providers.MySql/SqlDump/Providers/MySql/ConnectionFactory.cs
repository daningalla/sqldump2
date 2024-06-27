using MySqlConnector;
using SqlDump.Data;

namespace SqlDump.Providers.MySql;

internal sealed class ConnectionFactory
{
    private readonly MySqlConnectionStringBuilder _builder;
    private readonly string _connectionString;

    private ConnectionFactory(string connectionString)
    {
        _builder = new MySqlConnectionStringBuilder(_connectionString = connectionString);
    }

    internal async Task<MySqlConnection> GetConnection(CancellationToken cancellationToken)
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        return connection;
    }
    
    internal static ConnectionFactory Create(KeyValuePair<string, object>[] properties)
    {
        var entries = properties
            .Where(kvp => kvp.Key.StartsWith("mysql_", StringComparison.OrdinalIgnoreCase))
            .Select(kvp =>
            {
                var value = kvp.Value switch
                {
                    string str => str,
                    Secret secret => secret.GetValue(),
                    { } obj => obj.ToString()
                };

                return $"{kvp.Key[6..]}={value}";
            });

        var connectionString = string.Join(';', entries);

        return new ConnectionFactory(connectionString);
    }
}