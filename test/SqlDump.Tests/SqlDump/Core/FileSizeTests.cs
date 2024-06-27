using Shouldly;

namespace SqlDump.Core;

public class FileSizeTests
{
    [Theory, MemberData(nameof(Theories))]
    public void Parse_Returns_Expected(string s, int expected)
    {
        FileSize.Parse(s, null).Bytes.ShouldBe(expected);
    }
    
    public static TheoryData<string, int> Theories = new()
    {
        { "100b", 100 },
        { "10kb", 10000 },
        { "10mb", 10_000_000 },
        { "2gb", 2_000_000_000 }
    };
}