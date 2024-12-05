
namespace AdventOfCode2024.Code;

public class Day5 : BaseDay
{
    private Dictionary<int, int[]> _rulesForward;
    private Dictionary<int, int[]> _rulesBackward;
    private List<int[]> _toProduce;

    public Day5(string? fileName = null) : base(fileName)
    {
        var separatorIndex = Input.ToList().IndexOf("");
        var rules = Input.Take(separatorIndex).ToArray();
        _rulesForward = rules.GroupBy(s => int.Parse(s.Split("|")[0]), s => s.Split("|")[1]).ToDictionary(g => g.Key, g => g.Select(int.Parse).ToArray());
        _rulesBackward = rules.GroupBy(s => int.Parse(s.Split("|")[1]), s => s.Split("|")[0]).ToDictionary(g => g.Key, g => g.Select(int.Parse).ToArray());
        _toProduce = Input.Skip(separatorIndex + 1).Select(s => s.Split(",").Select(int.Parse).ToArray()).ToList();
    }

    public override string Solution1()
    {
        return _toProduce.Select((toProduce, index) => GetMiddlePageNumber(toProduce, index)).Sum().ToString();
    }

    private int GetMiddlePageNumber(int[] toProduce, int index)
    {
        if (IsCorrectOrder(toProduce))
        {
            Console.WriteLine($"Correct order for {index}: {string.Join(',', toProduce)}");
            return toProduce[toProduce.Length / 2];
        }

        return 0;
    }

    private bool IsCorrectOrder(int[] toProduce)
    {
        for (int index = 0; index < toProduce.Length - 1; index++)
        {
            if (_rulesForward.ContainsKey(toProduce[index]) && !_rulesForward[toProduce[index]].Contains(toProduce[index + 1]))
            {
                return false;
            }
        }
        for (int index = toProduce.Length - 1; index > 0; index--) 
        {
            if (_rulesBackward.ContainsKey(toProduce[index]) && !_rulesBackward[toProduce[index]].Contains(toProduce[index - 1]))
            {
                return false;
            }
        }

        return true;
    }
}
