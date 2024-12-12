using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day11Tests
{
    [Fact]
    public void Day11_Solution1()
    {
        var day = new Day11("Day11Test");
        Assert.Equal("55312", day.Solution1());
    } 

    [Fact]
    public void Day11_Solution2()
    {
        var day = new Day11("Day11Test");
        Assert.Equal("???", day.Solution2());
    }
}