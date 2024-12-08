using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Code;

public class Day8 : BaseDay
{
    private readonly char[,] _grid;
    private readonly int _width;
    private readonly int _height;

    public Day8(string? filename = null) : base(filename)
    {
        _grid = new char[Input[0].Length, Input.Length];
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[0].Length; x++)
            {
                _grid[x, y] = Input[y][x];
            }
        }

        _width = _grid.GetLength(0);
        _height = _grid.GetLength(1);
    }

    public override string Solution1()
    {
        var antinodes = new List<(int x, int y)>();
        var frequenciesWithAntennaPairs = GetFrequenciesWithAntennaPairs();
        foreach (var frequencyWithAntennaPairs in frequenciesWithAntennaPairs)
        {
            foreach (var antennaPair in frequencyWithAntennaPairs.Value)
            {
                (int x, int y) offset1 = (antennaPair.Position1.positionX - antennaPair.Position2.positionX, antennaPair.Position1.positionY - antennaPair.Position2.positionY);
                antinodes.Add((antennaPair.Position1.positionX + offset1.x, antennaPair.Position1.positionY + offset1.y));
                
                (int x, int y) offset2 = (antennaPair.Position2.positionX - antennaPair.Position1.positionX, antennaPair.Position2.positionY - antennaPair.Position1.positionY);
                antinodes.Add((antennaPair.Position2.positionX + offset2.x, antennaPair.Position2.positionY + offset2.y));
            }
        }

        return antinodes.Where(antinode => antinode.x >= 0 && antinode.y >= 0 && antinode.x < _width && antinode.y < _height).Distinct().Count().ToString();
    }

    public override string Solution2()
    {
        var antinodes = new List<(int x, int y)>();
        var frequenciesWithAntennaPairs = GetFrequenciesWithAntennaPairs();
        foreach (var frequencyWithAntennaPairs in frequenciesWithAntennaPairs)
        {
            foreach (var antennaPair in frequencyWithAntennaPairs.Value)
            {
                (int x, int y) antinode;

                (int x, int y) offset1 = (antennaPair.Position1.positionX - antennaPair.Position2.positionX, antennaPair.Position1.positionY - antennaPair.Position2.positionY);
                antinode = (antennaPair.Position1.positionX + offset1.x, antennaPair.Position1.positionY + offset1.y);
                while (antinode.x >= 0 && antinode.y >= 0 && antinode.x < _width && antinode.y < _height)
                {
                    antinodes.Add(antinode);
                    antinode = (antinode.x + offset1.x, antinode.y + offset1.y);
                }
                
                (int x, int y) offset2 = (antennaPair.Position2.positionX - antennaPair.Position1.positionX, antennaPair.Position2.positionY - antennaPair.Position1.positionY);
                antinode = (antennaPair.Position1.positionX + offset2.x, antennaPair.Position1.positionY + offset2.y);
                while (antinode.x >= 0 && antinode.y >= 0 && antinode.x < _width && antinode.y < _height)
                {
                    antinodes.Add(antinode);
                    antinode = (antinode.x + offset2.x, antinode.y + offset2.y);
                }
            }
        }

        antinodes.AddRange(frequenciesWithAntennaPairs.Values.SelectMany(antennaPairs => antennaPairs).SelectMany(antennaPair => new[] { antennaPair.Position1, antennaPair.Position2 }));
        //Visualize(antinodes);

        return antinodes.Distinct().Count().ToString();
    }

    private Dictionary<char, List<((int positionX, int positionY) Position1, (int positionX, int positionY) Position2)>> GetFrequenciesWithAntennaPairs()
    {
        var cartesianProduct =
            from y in Enumerable.Range(0, _height)
            from x in Enumerable.Range(0, _width)
            where _grid[x, y] != '.'
            group new { Frequency = _grid[x, y], Position = (x, y) } by _grid[x, y] into groups
            from grp1 in groups
            from grp2 in groups
            where grp1.Frequency == grp2.Frequency && grp1.Position != grp2.Position
            select new { grp1.Frequency, Position1 = grp1.Position, Position2 = grp2.Position };

        var result = new Dictionary<char, List<((int positionX, int positionY) Position1, (int positionX, int positionY) Position2)>>();
        foreach (var item in cartesianProduct)
        {
            if (!result.ContainsKey(item.Frequency))
            {
                result.Add(item.Frequency, new());
            }
            if (
                result[item.Frequency].Contains((item.Position1, item.Position2)) ||
                result[item.Frequency].Contains((item.Position2, item.Position1)))
            {
                continue;
            }
            result[item.Frequency].Add((item.Position1, item.Position2));
        }

        return result;
    }

    private void Visualize(List<(int x, int y)> antinodes)
    {
        Console.Clear();
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                if (antinodes.Contains((x, y)))
                {
                    if (_grid[x, y] != '.')
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                    }
                    Console.Write('#');
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.Write(_grid[x, y]);
                }
            }
            Console.WriteLine();
        }
    }
}
