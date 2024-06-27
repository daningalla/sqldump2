namespace SqlDump.Types;

internal sealed class DelegatedTypeHandler<T> : TypeHandler<T>
{
    private readonly Func<string, object> _converter;

    internal DelegatedTypeHandler(Func<string, object> converter, bool isNullable) : base(isNullable)
    {
        _converter = converter;
    }

    /// <inheritdoc />
    public override object Convert(string str) => _converter(str);
}