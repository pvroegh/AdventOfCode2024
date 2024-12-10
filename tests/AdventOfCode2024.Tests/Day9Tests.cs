using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day9Tests
{
    [Fact]
    public void Day9_Solution1()
    {
        var day = new Day9("Day9Test");
        Assert.Equal("1928", day.Solution1());
    }

    [Fact]
    public void Day9_Solution2()
    {
        var day = new Day9("Day9Test");
        Assert.Equal("2858", day.Solution2());
    }
}