namespace SqlDump.Types;

internal sealed class ParsableTypeHandler<T> : TypeHandler<T> where T : IParsable<T>
{
    /// <inheritdoc />
    public ParsableTypeHandler(bool isNullable) : base(isNullable)
    {
    }

    /// <inheritdoc />
    public override object Convert(string str) => T.Parse(str, null);
}