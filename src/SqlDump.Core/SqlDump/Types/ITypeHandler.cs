namespace SqlDump.Types;

/// <summary>
/// Performs efficient operations on objects of a specific type.
/// </summary>
public interface ITypeHandler
{
    /// <summary>
    /// Gets the type being managed.
    /// </summary>
    Type Type { get; }
    
    /// <summary>
    /// Gets whether values of the type can be <c>null</c>.
    /// </summary>
    bool IsNullable { get; }

    /// <summary>
    /// Casts, converts, or parses the given string to the target type.
    /// </summary>
    /// <param name="str">String to convert.</param>
    /// <returns>Object</returns>
    object Convert(string str);
}