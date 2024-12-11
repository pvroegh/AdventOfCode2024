using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day10Tests
{
    [Fact]
    public void Day10_Solution1()
    {
        var day = new Day10("Day10Test");
        Assert.Equal("36", day.Solution1());
    }

    [Fact]
    public void Day10_Solution2()
    {
        var day = new Day10("Day10Test");
        Assert.Equal("81", day.Solution2());
    }
}