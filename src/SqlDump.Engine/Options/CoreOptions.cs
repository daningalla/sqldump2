using Microsoft.Extensions.Logging;
using SqlDump.Data;

namespace Options;

public class CoreOptions
{
    /// <summary>
    /// Gets the logging verbosity level.
    /// </summary>
    public LogLevel Verbosity { get; init; }

    /// <summary>
    /// Gets values to pass to data providers.
    /// </summary>
    public KeyValuePair<string, string>[] ProviderValues { get; init; } = [];

    /// <summary>
    /// Gets secrets to pass to data provider or other components.
    /// </summary>
    public KeyValuePair<string, Secret>[] Secrets { get; init; } = [];

    /// <summary>
    /// Gets the selected data provider for the operation.
    /// </summary>
    public string ProviderId { get; init; } = default!;
}