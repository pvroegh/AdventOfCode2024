using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day12Tests
{
    [Fact]
    public void Day12_Solution1()
    {
        var day = new Day12("Day12Test");
        Assert.Equal("1930", day.Solution1());
    } 

    [Fact]
    public void Day12_Solution2()
    {
        var day = new Day12("Day12Test");
        Assert.Equal("???", day.Solution2());
    }
}