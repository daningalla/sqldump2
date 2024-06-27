namespace SqlDump.Data;

/// <summary>
/// A function that creates data providers.
/// </summary>
public delegate IDataProvider DataProviderFactory(IServiceProvider services, KeyValuePair<string, object>[] properties);