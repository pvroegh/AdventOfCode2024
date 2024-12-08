using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day6Tests
{
    [Fact]
    public void Day6_Solution1()
    {
        var day = new Day6("Day6Test");
        Assert.Equal("41", day.Solution1());
    }

    [Fact]
    public void Day6_Solution2()
    {
        var day = new Day6("Day6Test");
        Assert.Equal("6", day.Solution2());
    }
}