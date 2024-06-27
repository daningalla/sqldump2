namespace SqlDump.Types;

internal abstract class TypeHandler<T> : ITypeHandler
{
    protected TypeHandler(bool isNullable) => IsNullable = isNullable;
    
    /// <inheritdoc />
    public Type Type => typeof(T);

    /// <inheritdoc />
    public bool IsNullable { get; }

    /// <inheritdoc />
    public abstract object Convert(string str);
}