namespace SqlDump.Types;

internal sealed class ParsableNullValueTypeHandler<T> : TypeHandler<T?> where T : struct, IParsable<T>
{
    public ParsableNullValueTypeHandler() : base(isNullable: true)
    {
    }

    /// <inheritdoc />
    public override object Convert(string str) => T.Parse(str, null);
}