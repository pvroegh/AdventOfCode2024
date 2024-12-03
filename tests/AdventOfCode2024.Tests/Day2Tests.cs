using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day2Tests
{
    [Fact]
    public void Day2_Solution1()
    {
        var day = new Day2("Day2Test");
        Assert.Equal("2", day.Solution1());
    }

    [Fact]
    public void Day2_Solution2()
    {
        var day = new Day2("Day2Test");
        Assert.Equal("4", day.Solution2());
    }
}