using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day3Tests
{
    [Fact]
    public void Day2_Solution1()
    {
        var day = new Day3("Day3Test");
        Assert.Equal("161", day.Solution1());
    }

    [Fact]
    public void Day2_Solution2()
    {
        var day = new Day3("Day3Test");
        Assert.Equal("48", day.Solution2());
    }
}