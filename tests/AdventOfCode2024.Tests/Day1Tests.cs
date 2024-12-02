using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day3Tests
{
    [Fact]
    public void Day1_Solution1()
    {
        var day = new Day1("Day1Test");
        Assert.Equal("4361", day.Solution1());
    }

    [Fact]
    public void Day1_Solution2()
    {
        var day = new Day1("Day1Test");
        Assert.Equal("467835", day.Solution2());
    }
}