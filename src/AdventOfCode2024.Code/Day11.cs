using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2024.Code;

public class Day11 : BaseDay
{
    public Day11(string? filename = null) : base(filename)
    {
    }

    public override string Solution1()
    {
        var stones = new List<long>(Input[0].Split(' ').Select(long.Parse));

        foreach (var index in Enumerable.Range(0, 25))
        {
            stones = Blink(stones);
        }

        return stones.Count.ToString();
    }

    public override string Solution2()
    {
        var stones = new List<long>(Input[0].Split(' ').Select(long.Parse));

        foreach (var index in Enumerable.Range(0, 75))
        {
            var previousStoneCount = stones.Count;
            stones = Blink(stones);
            Console.WriteLine($"Index: {index}, Count: {stones.Count}, Diff: {stones.Count - previousStoneCount}, Divided: {stones.Count / (index + 1)}");
        }
        // TODO: optimize this
        return stones.Count.ToString();
    }

    private List<long> Blink(List<long> stones)
    {
        var newStones = new List<long>();
        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                newStones.Add(1);
                continue;
            }
            else if (HasEvenDigitCount(stone, out var digitCount))
            {
                var divisor = (long)Math.Pow(10, digitCount / 2);
                newStones.Add(stone / divisor);
                newStones.Add(stone % divisor);
            }
            else
            {
                newStones.Add(stone * 2024);
            }
        }
        
        return newStones;
    }

    private bool HasEvenDigitCount(long number, out long digitCount)
    {
        digitCount = DigitCount(number);
        return digitCount % 2 == 0;
    }

    private int DigitCount(long number)
    {
        return number == 0 ? 1 : (int)Math.Floor(Math.Log10(number) + 1);
    }
}