using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SqlDump.Data;

namespace SqlDump.Providers.MySql;

/// <summary>
/// Data provider.
/// </summary>
internal sealed class MySqlDataProvider : IDataProvider
{
    private readonly ILogger<MySqlDataProvider> _logger;
    private readonly ConnectionFactory _connectorFactory;
    public const string ProviderId = "mysql";

    private MySqlDataProvider(
        ILogger<MySqlDataProvider> logger,
        ConnectionFactory connectorFactory)
    {
        _logger = logger;
        _connectorFactory = connectorFactory;
    }
    
    internal static IDataProvider Create(
        IServiceProvider services,
        KeyValuePair<string, object>[] properties)
    {
        return new MySqlDataProvider(
            services.GetService<ILogger<MySqlDataProvider>>() ?? NullLogger<MySqlDataProvider>.Instance,
            ConnectionFactory.Create(properties));
    }
}