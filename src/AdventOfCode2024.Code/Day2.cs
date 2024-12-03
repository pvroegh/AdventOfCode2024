namespace AdventOfCode2024.Code;

public class Day2(string? filename = null) : BaseDay(filename)
{
    public override string Solution1()
    {
        var reports = Input.Select(i => new Report(i.Split(' ').Select(int.Parse).ToArray()));

        return reports.Count(r => r.IsSafe()).ToString();
    }

    override public string Solution2()
    {
        var reports = Input.Select(i => new Report(i.Split(' ').Select(int.Parse).ToArray()));

        return reports.Count(r => r.IsSafeWithDampener()).ToString(); ;
    }

    private class Report(int[] levels)
    {
        private bool IsIncreasing => levels[0] < levels[1];

        public bool IsSafe()
        {
            for (int i = 0; i < levels.Length - 1; i++)
            {
                var diff = levels[i + 1] - levels[i];
                if (IsIncreasing && !(diff >= 1 && diff <= 3)) return false;
                if (!IsIncreasing && !(diff <= -1 && diff >= -3)) return false;
            }

            return true;

        }

        public bool IsSafeWithDampener()
        {
            if (!IsSafe())
            {
                for (int omit = 0; omit < levels.Length; omit++)
                {
                    var levelsOmitted = levels.Where((_, i) => i != omit).ToArray();
                    if (new Report(levelsOmitted).IsSafe()) return true;
                }

                return false;
            }

            return true;
        }
    }
}
