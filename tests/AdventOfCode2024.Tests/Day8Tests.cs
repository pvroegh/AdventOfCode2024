using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day8Tests
{
    [Fact]
    public void Day8_Solution1()
    {
        var day = new Day8("Day8Test");
        Assert.Equal("14", day.Solution1());
    }

    [Fact]
    public void Day8_Solution2()
    {
        var day = new Day8("Day8Test");
        Assert.Equal("34", day.Solution2());
    }
}