using Microsoft.Extensions.Logging;

namespace SqlDump.Diagnostics;

public interface IDiagnostic
{
    /// <summary>
    /// When implemented by a component, logs verbose diagnostic data.
    /// </summary>
    /// <param name="logger">Logger</param>
    void LogInfo(ILogger logger);

    /// <summary>
    /// Gets child diagnostics to log.
    /// </summary>
    IEnumerable<IDiagnostic> ChildDiagnostics => [];
}