using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day7Tests
{
    [Fact]
    public void Day7_Solution1()
    {
        var day = new Day7("Day7Test");
        Assert.Equal("3749", day.Solution1());
    }

    [Fact]
    public void Day7_Solution2()
    {
        var day = new Day7("Day7Test");
        Assert.Equal("11387", day.Solution2());
    }
}