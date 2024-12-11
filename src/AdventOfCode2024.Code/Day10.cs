using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2024.Code;

public class Day10 : BaseDay
{
    private readonly int[,] _grid;
    private readonly int _width;
    private readonly int _height;
    private readonly List<List<(int x, int y, int value)>> _trails = new();

    public Day10(string? filename = null) : base(filename)
    {
        _grid = new int[Input[0].Length, Input.Length];
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[0].Length; x++)
            {
                _grid[x, y] = int.Parse(Input[y][x].ToString());
                if (_grid[x, y] == 0)
                {
                    _trails.Add([(x, y, _grid[x, y])]);
                }
            }
        }

        _width = _grid.GetLength(0);
        _height = _grid.GetLength(1);
    }

    public override string Solution1()
    {
        bool atLeastOneTrailAdvanced = true;
        while (atLeastOneTrailAdvanced)
        {
            atLeastOneTrailAdvanced = false;
            for (int trailIndex = _trails.Count - 1; trailIndex >= 0; trailIndex--)
            {
                if (AdvanceTrail(_trails[trailIndex]))
                {
                    atLeastOneTrailAdvanced = true;
                }                
            }
        }

        var startsAndEnds = _trails.Select(trail => (trail[0].x, trail[0].y, trail[^1].x, trail[^1].y));

        return startsAndEnds.Distinct().Count().ToString();
    }

    public override string Solution2()
    {
        bool atLeastOneTrailAdvanced = true;
        while (atLeastOneTrailAdvanced)
        {
            atLeastOneTrailAdvanced = false;
            for (int trailIndex = _trails.Count - 1; trailIndex >= 0; trailIndex--)
            {
                if (AdvanceTrail(_trails[trailIndex]))
                {
                    atLeastOneTrailAdvanced = true;
                }
            }
        }

        return _trails.Count().ToString();
    }


    private (int offsetX, int offsetY)[] _offsets =
    [
        (0, 1),
        (1, 0),
        (0, -1),
        (-1, 0)
    ];

    private bool AdvanceTrail(List<(int x, int y, int value)> trail)
    {
        var advanced = false;
        var multipleTrails = false;
        var position = trail[^1];
        var value = _grid[position.x, position.y];
        
        if (value < 9)
        {
            foreach (var direction in _offsets)
            {
                if (position.x + direction.offsetX < 0 || position.x + direction.offsetX >= _width ||
                    position.y + direction.offsetY < 0 || position.y + direction.offsetY >= _height)
                {
                    continue;
                }

                if (value + 1 == _grid[position.x + direction.offsetX, position.y + direction.offsetY])
                {
                    if (!multipleTrails)
                    {
                        trail.Add((position.x + direction.offsetX, position.y + direction.offsetY, _grid[position.x + direction.offsetX, position.y + direction.offsetY]));
                        multipleTrails = true;
                        advanced = true;
                    }
                    else
                    {
                        var newTrail = new List<(int x, int y, int value)>(trail[..^1]);
                        newTrail.Add((position.x + direction.offsetX, position.y + direction.offsetY, _grid[position.x + direction.offsetX, position.y + direction.offsetY]));
                        _trails.Add(newTrail);
                        advanced = true;
                    }
                }
            }

            if (!advanced)
            {
                _trails.Remove(trail);
            }
        }

        return advanced;
    }
}