using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day1Tests
{
    [Fact]
    public void Day1_Solution1()
    {
        var day = new Day1("Day1Test");
        Assert.Equal("11", day.Solution1());
    }

    [Fact]
    public void Day1_Solution2()
    {
        var day = new Day1("Day1Test");
        Assert.Equal("31", day.Solution2());
    }
}