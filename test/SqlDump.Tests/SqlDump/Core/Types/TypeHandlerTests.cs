using Shouldly;
using SqlDump.Types;

namespace SqlDump.Core.Types;

public class TypeHandlerTests
{
    [Fact]
    public void Creates_String_Handler()
    {
        var unit = TypeHandlerFactory.Create<string>();
        unit.Convert("input").ShouldBe("input");
        unit.IsNullable.ShouldBeTrue();
    }
    
    [Fact]
    public void Creates_Null_String_Handler()
    {
        var unit = TypeHandlerFactory.Create<string?>();
        unit.Convert("input").ShouldBe("input");
        unit.IsNullable.ShouldBeTrue();
    }

    [Fact]
    public void Creates_Parsable_Handler()
    {
        var unit = TypeHandlerFactory.Create<int>();
        unit.Convert("10").ShouldBe(10);
        unit.IsNullable.ShouldBeFalse();
    }

    [Fact]
    public void Creates_Parsable_ValueType_Handler()
    {
        var unit = TypeHandlerFactory.Create<int?>();
        unit.Convert("10").ShouldBe(10);
        unit.IsNullable.ShouldBeTrue();
    }
}