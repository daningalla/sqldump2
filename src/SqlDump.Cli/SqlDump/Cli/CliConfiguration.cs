using Microsoft.Extensions.Logging;
using Options;
using Vertical.Cli;
using Vertical.Cli.Configuration;

namespace SqlDump.Cli;

public static class CliConfiguration
{
    public static RootCommand<CoreOptions> Create()
    {
        var root = new RootCommand<CoreOptions>(
            "sqldump",
            "Utilities used to extract data from a relational database.");

        var export = root.AddSubCommand<ExportOptions>(
            "export",
            "Exports data from a table into data files.");
        
        ConfigureCoreOptions(root);
        ConfigureExportOptions(export);

        root.ConfigureOptions(options =>
        {
            options.EnableResponseFiles = true;
            options.ValueConverters.Add(new KeyValuePairConverter());
            options.ValueConverters.Add(new KeyValueSecretConverter());
        });

        return root;
    }

    private static void ConfigureCoreOptions(RootCommand<CoreOptions> command)
    {
        command
            .AddOption(x => x.ProviderId,
                names: ["-p", "--provider"],
                arity: Arity.One,
                description: "Id of the provider to use (e.g. mysql).",
                scope: CliScope.Descendants)
            .AddOption(x => x.ProviderValues,
                names: ["-p", "--property"],
                arity: Arity.ZeroOrMany,
                description: "Property used to configure the data provider.",
                operandSyntax: "KEY=VALUE",
                scope: CliScope.Descendants)
            .AddOption(x => x.Secrets,
                names: ["--secret"],
                arity: Arity.ZeroOrMany,
                description: "Property with a sensitive value used to configure the data provider.",
                operandSyntax: "KEY=VALUE",
                scope: CliScope.Descendants)
            .AddOption(x => x.Verbosity,
                names: ["-v", "--verbosity"],
                defaultProvider: () => LogLevel.Information,
                description: "Logging verbosity, one of Debug, Information, Warning, Error, or Critical.",
                operandSyntax: "LEVEL",
                scope: CliScope.Descendants)
            ;
    }

    private static void ConfigureExportOptions(CliCommand<ExportOptions> command)
    {
        command
            .AddOption(x => x.SchemaName,
                names: ["--schema"],
                Arity.One,
                description: "Name of the database schema.",
                operandSyntax: "SCHEMA")
            .AddOption(x => x.TableName,
                names: ["--table"],
                Arity.One,
                description: "Name of the database table.",
                operandSyntax: "TABLE")
            .AddOption(x => x.ExcludedColumns,
                names: ["--exclude-column"],
                Arity.ZeroOrMany,
                description: "Identifier of a column to exclude from the export.",
                operandSyntax: "COLUMN")
            .AddOption(x => x.SortColumns,
                names: ["--sort-by"],
                Arity.ZeroOrMany,
                description: "The identifier of a column to sort batch query operations (required unless --auto-detect-sort is used).",
                operandSyntax: "COLUMN",
                validation: v => v.Must(
                    (model, col) => model.AutoDetectSortColumns || col.Length > 0,
                    "Sort column(s) must be specified when --auto-detect-sort is not used."))
            .AddSwitch(x => x.AutoDetectSortColumns,
                names: ["--auto-detect-sort"],
                description: "Try to infer sort columns using table keys.")
            .AddOption(x => x.QueryLimit,
                names: ["--query-limit"],
                defaultProvider: () => 250,
                description: "The number of rows to limit batch queries to (defaults to 250).",
                operandSyntax: "COUNT",
                validation: v => v.GreaterThan(0))
            .AddOption(x => x.Parameters,
                names: ["--query-parameter"],
                description: "Parameter used in the first query in the export operation.",
                operandSyntax: "KEY=VALUE")
            .AddOption(x => x.OutputDirectory,
                names: ["--out", "--output-path"],
                defaultProvider: () => new DirectoryInfo(Directory.GetCurrentDirectory()),
                description: "Directory where output files will be written.",
                operandSyntax: "PATH")
            .AddOption(x => x.OutputFileSize,
                names: ["--output-file-size"],
                defaultProvider: () => new FileSize(25_000_000),
                description: "Approximate size of output files.",
                operandSyntax: "COUNT<UNIT=B|KB|MB|GB>")
            .AddOption(x => x.CompressionType,
                names: ["--output-compression"],
                defaultProvider: () => CompressionType.GZip,
                description: "Compression algorithm applied to output files.",
                operandSyntax: "NONE|GZIP|DEFLATE")
            .AddOption(x => x.MaxRows,
                names: ["--max-rows"],
                description: "Maximum total number of rows to write to output files. The operation stops when this value is reached.")
            .AddSwitch(x => x.Test,
                names: ["--test"],
                description: "Performs a test connection and query, but does not write any results.")
            ;

        command.HandleAsync(async (model, cancelToken) => await CliCommand.ExecuteCommandAsync(model, cancelToken));
    }
}