using SqlDump;

namespace Options;

public class ExportOptions : CoreOptions
{
    /// <summary>
    /// Gets the schema name
    /// </summary>
    public string SchemaName { get; init; } = default!;

    /// <summary>
    /// Gets the table name
    /// </summary>
    public string TableName { get; init; } = default!;

    /// <summary>
    /// Gets the names of columns to exclude.
    /// </summary>
    public string[] ExcludedColumns { get; init; } = [];

    /// <summary>
    /// Gets columns that determine the batch query sort order.
    /// </summary>
    public string[] SortColumns { get; init; } = [];
    
    /// <summary>
    /// Gets whether to auto-detect sort columns.
    /// </summary>
    public bool AutoDetectSortColumns { get; init; }
    
    /// <summary>
    /// Gets the number of rows to constrain batch queries to.
    /// </summary>
    public int QueryLimit { get; init; }

    /// <summary>
    /// Gets the initial parameters for the first query.
    /// </summary>
    public KeyValuePair<string, string>[] Parameters { get; init; } = [];

    /// <summary>
    /// Gets the directory where output files will be written.
    /// </summary>
    public DirectoryInfo OutputDirectory { get; set; } = default!;
    
    /// <summary>
    /// Gets the maximum total number of rows to write to output files.
    /// </summary>
    public int? MaxRows { get; init; }
    
    /// <summary>
    /// Gets the output file size.
    /// </summary>
    public FileSize OutputFileSize { get; init; }
    
    /// <summary>
    /// Gets the compression type.
    /// </summary>
    public CompressionType CompressionType { get; init; }
    
    /// <summary>
    /// Gets the test mode
    /// </summary>
    public bool Test { get; init; }
}