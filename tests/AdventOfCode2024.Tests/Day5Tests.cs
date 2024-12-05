using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day5Tests
{
    [Fact]
    public void Day5_Solution1()
    {
        var day = new Day5("Day5Test");
        Assert.Equal("143", day.Solution1());
    }

    [Fact]
    public void Day5_Solution2()
    {
        var day = new Day5("Day5Test");
        Assert.Equal("123", day.Solution2());
    }
}