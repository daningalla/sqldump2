using SqlDump.Types;

namespace SqlDump.Data;

/// <summary>
/// Defines metadata of a database column.
/// </summary>
public class ColumnMetadata
{
    /// <summary>
    /// Gets the name of the column.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Gets the expression used in provider queries.
    /// </summary>
    public string QueryExpression { get; init; } = default!;

    /// <summary>
    /// Gets the mapped clr type.
    /// </summary>
    public Type ClrType { get; init; } = default!;

    /// <summary>
    /// Gets the type handler.
    /// </summary>
    public ITypeHandler ClrTypeHandler { get; init; } = default!;
    
    /// <summary>
    /// Gets whether this column is part of the primary key.
    /// </summary>
    public bool IsKeyColumn { get; init; }
    
    /// <summary>
    /// Gets whether null values are expected.
    /// </summary>
    public bool IsNullable { get; init; }
    
    /// <summary>
    /// Gets the ordinal position of the column.
    /// </summary>
    public int OrdinalPosition { get; init; }

    /// <summary>
    /// Gets the data provider.
    /// </summary>
    public IDataProvider Provider { get; init; } = default!;
    
    /// <summary>
    /// Gets the maximum character length.
    /// </summary>
    public int? CharacterLength { get; init; }
    
    /// <summary>
    /// Gets the precision of a real number.
    /// </summary>
    public int? Precision { get; init; }
    
    /// <summary>
    /// Gets the scale of a real number.
    /// </summary>
    public int? Scale { get; init; }

    /// <inheritdoc />
    public override string ToString() => $"{Name} ({ClrType})";
}