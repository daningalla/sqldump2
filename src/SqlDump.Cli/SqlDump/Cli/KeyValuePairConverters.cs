using System.Text.RegularExpressions;
using SqlDump.Data;
using Vertical.Cli.Conversion;

namespace SqlDump.Cli;

internal static partial class KeyValuePairMatcher
{
    internal static bool TryMatch(string str, out (string Key, string Value) result)
    {
        result = default;
        var match = MyRegex().Match(str);

        if (!match.Success)
            return false;

        result = (match.Groups[1].Value, match.Groups[2].Value);
        return true;
    }
    
    [GeneratedRegex(@"^([^=]+)=(.+)")]
    private static partial Regex MyRegex();
}

public class KeyValuePairConverter : ValueConverter<KeyValuePair<string, string>>
{
    /// <inheritdoc />
    public override KeyValuePair<string, string> Convert(string s)
    {
        if (!KeyValuePairMatcher.TryMatch(s, out var kv))
        {
            throw new FormatException($"The input string '{s}' was not in the correct format.");
        }

        return new KeyValuePair<string, string>(kv.Key, kv.Value);
    }
}

public class KeyValueSecretConverter : ValueConverter<KeyValuePair<string, Secret>>
{
    /// <inheritdoc />
    public override KeyValuePair<string, Secret> Convert(string s)
    {
        if (!KeyValuePairMatcher.TryMatch(s, out var kv))
        {
            throw new FormatException($"The input string '{s}' was not in the correct format.");
        }

        return new KeyValuePair<string, Secret>(kv.Key, new Secret(kv.Value));
    }
}