namespace AdventOfCode2024.Code;

public class Day1(string? filename = null) : BaseDay(filename)
{
    public override string Solution1()
    {
        var lists = Input.Select(i => i.Split("   "));
        var leftList = lists.Select(i => int.Parse(i[0])).OrderBy(i => i).ToList();
        var rightList = lists.Select(i => int.Parse(i[1])).OrderBy(i => i).ToList();

        return leftList.Select((i, index) => Math.Abs(rightList[index] - i)).Sum().ToString();
    }

    override public string Solution2()
    {
        var lists = Input.Select(i => i.Split("   "));
        var leftList = lists.Select(i => int.Parse(i[0])).OrderBy(i => i).ToList();
        var rightList = lists.Select(i => int.Parse(i[1])).OrderBy(i => i).ToList();

        return leftList.Select(l => l * rightList.Count(r => r == l)).Sum().ToString();
    }
}
