using Spectre.Console;
using SqlDump.Cli;
using Vertical.Cli;

var rootCommand = CliConfiguration.Create();

try
{
    await rootCommand.InvokeAsync(args);
}
catch (CommandLineException exception)
{
    AnsiConsole.MarkupLineInterpolated($"[red]{exception.Message}[/]");
}
catch (Exception exception)
{
    AnsiConsole.WriteException(exception);
}