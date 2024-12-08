using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Code;

public class Day7 : BaseDay
{
    private IEnumerable<(long target, long[] numbers)> _equations;
    private Dictionary<long, int> _digitCounts;

    public Day7(string? filename = null) : base(filename)
    {
        _equations = Input.Select(s => s.Split(": ")).Select(Input => (long.Parse(Input[0]), Input[1].Split(" ").Select(long.Parse).ToArray()));
        _digitCounts = _equations.SelectMany(e => e.numbers).Distinct().ToDictionary(k => k, k => DigitCount(k));
    }

    public override string Solution1()
    {
        long sum = 0;
        
        foreach (var equation in _equations)
        {
            var operatorCombinations = GenerateOperatorCombinations(equation.numbers.Length, false);
            foreach (var operators in operatorCombinations)
            {
                if (EquationResult(equation.numbers, operators) == equation.target)
                {
                    sum += equation.target;
                    break;
                }
            }
        }

        return sum.ToString();
    }

    public override string Solution2()
    {
        long sum = 0;

        Parallel.ForEach(_equations, equation =>
        {
            long innerSum = 0;
            var operatorCombinations = GenerateOperatorCombinations(equation.numbers.Length, true);
            foreach (var operators in operatorCombinations)
            {
                if (EquationResult(equation.numbers, operators) == equation.target)
                {
                    innerSum += equation.target;
                    break;
                }
            }

            Interlocked.Add(ref sum, innerSum);
        });

        return sum.ToString();
    }

    private static string[] GenerateOperatorCombinations(int valueCount, bool forSolution2 = false)
    {
        if (valueCount <= 0)
            return Array.Empty<string>();

        int operatorCount = valueCount - 1;

        return operatorCount == 1
            ? (forSolution2 ? new[] { "+", "*", "|" } : new[] { "+", "*" })
            : GenerateOperatorCombinations(operatorCount, forSolution2)
                .SelectMany(s => (forSolution2 ? new[] { s + "+", s + "*", s + "|" } : new[] { s + "+", s + "*" }))
                .ToArray();
    }

    private long EquationResult(long[] values, string operators)
    {
        var result = values[0];
        for (int index = 0; index < operators.Length; index++)
        {
            result = operators[index] switch
            {
                '+' => result + values[index + 1],
                '*' => result * values[index + 1],
                '|' => Concatenate(result, values[index + 1]),
                _ => throw new InvalidOperationException()
            };
        }
        return result;
    }

    private long Concatenate(long a, long b)
    {
        // 12, 345 => 12345
        // 1, 34 => 134
        // 1, 2 => 12
        var digits = _digitCounts[b];
        var left = a * (long)Math.Pow(10, digits);
        return left + b;
    }

    private int DigitCount(long number)
    {
        return number == 0 ? 1 : (int)Math.Floor(Math.Log10(number) + 1);
    }
}
