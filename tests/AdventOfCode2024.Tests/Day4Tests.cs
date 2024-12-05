using AdventOfCode2024.Code;

namespace AdventOfCode2024.Tests;

public class Day4Tests
{
    [Fact]
    public void Day4_Solution1()
    {
        var day = new Day4("Day4Test");
        Assert.Equal("18", day.Solution1());
    }

    [Fact]
    public void Day4_Solution2()
    {
        var day = new Day4("Day4Test");
        Assert.Equal("9", day.Solution2());
    }
}