using DefaultNamespace;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Options;
using Serilog;
using Serilog.Events;
using SqlDump.Diagnostics;
using SqlDump.Providers.MySql;
using Vertical.SpectreLogger;

namespace SqlDump.Cli;

internal static class CliCommand
{
    public static async Task<int> ExecuteCommandAsync<TModel>(
        TModel options,
        CancellationToken cancellationToken)
        where TModel : CoreOptions
    {
        var services = new ServiceCollection();
        var logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "sqldump",
            $"{DateTime.Now:yyyyMMdd_hhmmss}_log.txt");

        services.AddLogging(logging => logging
            .SetMinimumLevel(options.Verbosity)
            .AddSpectreConsole(spectre => spectre.SetMinimumLevel(options.Verbosity))
            .AddSerilog(new LoggerConfiguration()
                .MinimumLevel.Is(options.Verbosity switch
                {
                    LogLevel.Trace => LogEventLevel.Verbose,
                    LogLevel.Debug => LogEventLevel.Debug,
                    LogLevel.Information => LogEventLevel.Information,
                    LogLevel.Warning => LogEventLevel.Warning,
                    LogLevel.Error => LogEventLevel.Error,
                    _ => LogEventLevel.Fatal
                })
                .WriteTo.File(path: logPath)
                .CreateLogger())
        );

        var collector = new DiagnosticCollector();

        services
            .AddEngine()
            .AddSingleton(new DiagnosticCollector())
            .ConfigureDataProviders(data => data.AddMySql());

        var serviceProvider = services.BuildServiceProvider();
        var handler = serviceProvider.GetRequiredService<ICommandHandler<TModel>>();
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(CliCommand));
        
        logger.LogInformation("File logs written to {path}", logPath);
        
        try
        {
            return await handler.ExecuteAsync(options, cancellationToken);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Un unhandled exception occurred.");
        }
        finally
        {
            collector.WriteData(logger);

            await serviceProvider.DisposeAsync();
        }

        return -1;
    }
}