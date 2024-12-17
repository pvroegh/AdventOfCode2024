using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace AdventOfCode2024.Code;

public class Day11 : BaseDay
{
    public Day11(string? filename = null) : base(filename)
    {
    }

    public override string Solution1()
    {
        var rocks = new List<long>(Input[0].Split(' ').Select(long.Parse)).ToDictionary(r => r, _ => (long)1);
        foreach (var index in Enumerable.Range(0, 25))
        {
            rocks = Blink(rocks);
        }

        return rocks.Values.Sum().ToString();
    }

    public override string Solution2()
    {
        var rocks = new List<long>(Input[0].Split(' ').Select(long.Parse)).ToDictionary(r => r, _ => (long)1);
        foreach (var index in Enumerable.Range(0, 75))
        {
            rocks = Blink(rocks);
        }

        return rocks.Values.Sum().ToString();
    }

    private Dictionary<long, long> Blink(Dictionary<long, long> rocks)
    {
        Dictionary<long, long> result = [];
        foreach (var (rock, count) in rocks)
        {
            var newRocks = BlinkRock(rock);
            foreach (var newRock in newRocks)
            {
                result.TryAdd(newRock, 0);
                result[newRock] += count;
            }
        }
        
        return result;
    }

    private List<long> BlinkRock(long rock)
    {
        if (rock == 0)
        {
            return [1];
        }
        else if (HasEvenDigitCount(rock, out var digitCount))
        {
            var divisor = (long)Math.Pow(10, digitCount / 2);
            return [rock / divisor, rock % divisor];
        }
        else
        {
            return [rock * 2024];
        }
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