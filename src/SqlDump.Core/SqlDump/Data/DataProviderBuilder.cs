using Microsoft.Extensions.DependencyInjection;

namespace SqlDump.Data;

/// <summary>
/// Used to configure data providers.
/// </summary>
public sealed class DataProviderBuilder(IServiceCollection services)
{
    /// <summary>
    /// Gets the services collection.
    /// </summary>
    public IServiceCollection Services => services;

    /// <summary>
    /// Adds a data provider factory registration.
    /// </summary>
    /// <param name="providerType">Provider type</param>
    /// <param name="factory">Factory that creates an instance.</param>
    /// <returns>A reference to this instance.</returns>
    public DataProviderBuilder Add(string providerType, DataProviderFactory factory)
    {
        services.AddKeyedSingleton(providerType, factory);
        return this;
    }
}