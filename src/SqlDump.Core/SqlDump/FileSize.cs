using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;

namespace SqlDump;

/// <summary>
/// Represents a file size in bytes.
/// </summary>
public readonly partial struct FileSize : IParsable<FileSize>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FileSize"/> type.
    /// </summary>
    /// <param name="bytes">Count of bytes.</param>
    public FileSize(int bytes)
    {
        Guard.IsGreaterThanOrEqualTo(bytes, 0);
        Bytes = bytes;
    }

    /// <summary>
    /// Gets the size in bytes.
    /// </summary>
    public int Bytes { get; }

    /// <summary>
    /// Gets the size in kilobytes.
    /// </summary>
    public double KiloBytes => Bytes > 0 ? Math.Round((double)Bytes / 1000, 1) : 0;

    /// <summary>
    /// Gets the size in megabytes.
    /// </summary>
    public double MegaBytes => Bytes > 0 ? Math.Round((double)Bytes / 1_000_000, 1) : 0;

    /// <summary>
    /// Gets the size in gigabytes.
    /// </summary>
    public double GigaBytes => Bytes > 0 ? Math.Round((double)Bytes / 1_000_000_000, 1) : 0;

    /// <summary>
    /// Parses a string to the format of this type.
    /// </summary>
    /// <param name="s">String to parse.</param>
    /// <param name="provider">Format provider</param>
    /// <returns></returns>
    public static FileSize Parse(string s, IFormatProvider? provider)
    {
        return TryParse(s, provider, out var result)
            ? result
            : throw new FormatException($"The input string '{s}' was not in a correct format.");
    }

    /// <summary>
    /// Tries to parse a string to the format of this type.
    /// </summary>
    /// <param name="s">String to parse.</param>
    /// <param name="provider">Format provider</param>
    /// <param name="result">The parsed value</param>
    /// <returns><c>true</c> if the operation succeeded.</returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out FileSize result)
    {
        result = default;

        if (s == null)
        {
            return false;
        }

        var match = MyRegex().Match(s);
        if (!match.Success)
            return false;

        var num = int.Parse(match.Groups[1].Value);
        var unit = match.Groups[2].Value.ToUpper();

        result = unit switch
        {
            "B" => new FileSize(num),
            "KB" => new FileSize(num * 1000),
            "MB" => new FileSize(num * 1_000_000),
            _ => new FileSize(num * 1_000_000_000)
        };

        return true;
    }

    [GeneratedRegex(@"^(\d+)([Bb]|[KkMmGg][Bb])")]
    private static partial Regex MyRegex();

    /// <inheritdoc />
    public override string ToString()
    {
        return Bytes switch
        {
            < 1_000 => $"{Bytes}b",
            < 1_000_000 => $"{KiloBytes}kb",
            < 1_000_000_000 => $"{MegaBytes}mb",
            _ => $"{GigaBytes}gb"
        };
    }
}