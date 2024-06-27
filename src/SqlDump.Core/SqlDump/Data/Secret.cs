namespace SqlDump.Data;

public readonly struct Secret
{
    private readonly string _value;

    public Secret(string value)
    {
        _value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <returns><c>string</c></returns>
    public string GetValue() => _value;

    /// <inheritdoc />
    public override string ToString() => "(secret)";
}