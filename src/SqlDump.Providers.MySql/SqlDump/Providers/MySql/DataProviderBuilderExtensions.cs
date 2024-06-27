using SqlDump.Data;

namespace SqlDump.Providers.MySql;

public static class DataProviderBuilderExtensions
{
    /// <summary>
    /// Adds MySql as a data provider.
    /// </summary>
    /// <param name="builder">Builder</param>
    /// <returns>A reference to the provided builder.</returns>
    public static DataProviderBuilder AddMySql(this DataProviderBuilder builder)
    {
        return builder.Add(MySqlDataProvider.ProviderId, MySqlDataProvider.Create);
    }    
}