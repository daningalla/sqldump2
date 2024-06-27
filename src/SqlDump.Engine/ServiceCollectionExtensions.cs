using Microsoft.Extensions.DependencyInjection;
using SqlDump.Data;

namespace DefaultNamespace;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEngine(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection ConfigureDataProviders(
        this IServiceCollection services,
        Action<DataProviderBuilder> builder)
    {
        builder(new DataProviderBuilder(services));
        return services;
    }
}