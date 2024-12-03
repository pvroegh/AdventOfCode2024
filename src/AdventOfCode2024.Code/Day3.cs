using System.Text.RegularExpressions;

namespace AdventOfCode2024.Code;

public class Day3(string? filename = null) : BaseDay(filename)
{
    public override string Solution1()
    {
        string oneline = string.Join("", Input);

        var matches = Regex.Matches(oneline, "mul\\((\\d+),(\\d+)\\)");
        return matches
            .Select(m => int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value))
            .Sum()
            .ToString()!;
    }

    override public string Solution2()
    {
        string oneline = string.Join("", Input);
        var matches = Regex.Matches(oneline, "(mul\\((\\d+),(\\d+)\\))|don\\'t|do");

        var solution = 0;
        var mulEnabled = true;
        foreach (Match match in matches)
        {
            switch (match.Value)
            {
                case "do":
                    mulEnabled = true;
                    break;
                case "don't":
                    mulEnabled = false;
                    break;
                default:
                    if (mulEnabled)
                    {
                        solution += int.Parse(match.Groups[2].Value) * int.Parse(match.Groups[3].Value);
                    }
                    break;
            }
        }

        return solution.ToString()!;
    }
}
