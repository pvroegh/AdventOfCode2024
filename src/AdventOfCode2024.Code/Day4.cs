using System.Text.RegularExpressions;

namespace AdventOfCode2024.Code;

public class Day4 : BaseDay
{
    private readonly char[,] _grid;
    private readonly int width;
    private readonly int height;

    public Day4(string? filename = null) : base(filename)
    {
        _grid = new char[Input.Length, Input[0].Length];
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[0].Length; x++)
            {
                _grid[x, y] = Input[y][x];
            }
        }

        width = _grid.GetLength(0);
        height = _grid.GetLength(1);
    }

    public override string Solution1()
    {
        var count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_grid[x, y] == 'X')
                {
                    count += CountWord(x, y);
                }
            }
        }

        return count.ToString();
    }

    private int CountWord(int x, int y)
    {
        var count = 0;
        count += HasHorizontalWord(x, y) ? 1 : 0;
        count += HasVerticalWord(x, y) ? 1 : 0;
        count += HasDiagonalSEWord(x, y) ? 1 : 0;
        count += HasDiagonalSWWord(x, y) ? 1 : 0;
        count += HasHorizontalInvertedWord(x, y) ? 1 : 0;
        count += HasVerticalInvertedWord(x, y) ? 1 : 0;
        count += HasDiagonalNWWord(x, y) ? 1 : 0;
        count += HasDiagonalNEWord(x, y) ? 1 : 0;

        return count;
    }

    private bool HasDiagonalNWWord(int x, int y)
    {
        if (y - 3 < 0 || x - 3 < 0)
        {
            return false;
        }

        return _grid[x - 1, y - 1] == 'M' && _grid[x - 2, y - 2] == 'A' && _grid[x - 3, y - 3] == 'S';

    }

    private bool HasDiagonalSWWord(int x, int y)
    {
        if (y + 3 >= height || x - 3 < 0)
        {
            return false;
        }

        return _grid[x - 1, y + 1] == 'M' && _grid[x - 2, y + 2] == 'A' && _grid[x - 3, y + 3] == 'S';

    }

    private bool HasVerticalInvertedWord(int x, int y)
    {
        if (y - 3 < 0)
        {
            return false;
        }

        return _grid[x, y - 1] == 'M' && _grid[x, y - 2] == 'A' && _grid[x, y - 3] == 'S';
    }

    private bool HasHorizontalInvertedWord(int x, int y)
    {
        if (x - 3 < 0)
        {
            return false;
        }

        return _grid[x - 1, y] == 'M' && _grid[x - 2, y] == 'A' && _grid[x - 3, y] == 'S';
    }

    private bool HasDiagonalSEWord(int x, int y)
    {
        if (y + 3 >= height || x + 3 >= width)
        {
            return false;
        }

        return _grid[x + 1, y + 1] == 'M' && _grid[x + 2, y + 2] == 'A' && _grid[x + 3, y + 3] == 'S';
    }

    private bool HasDiagonalNEWord(int x, int y)
    {
        if (y - 3 < 0 || x + 3 >= width)
        {
            return false;
        }

        return _grid[x + 1, y - 1] == 'M' && _grid[x + 2, y - 2] == 'A' && _grid[x + 3, y - 3] == 'S';
    }

    private bool HasVerticalWord(int x, int y)
    {
        if (y + 3 >= height)
        {
            return false;
        }

        return _grid[x, y + 1] == 'M' && _grid[x, y + 2] == 'A' && _grid[x, y + 3] == 'S';
    }

    private bool HasHorizontalWord(int x, int y)
    {
        if (x + 3 >= width)
        {
            return false;
        }

        return _grid[x + 1, y] == 'M' && _grid[x + 2, y] == 'A' && _grid[x + 3, y] == 'S';
    }

    override public string Solution2()
    {
        var count = 0;
        for (int x = 1; x < width - 1; x++)
        {
            for (int y = 1; y < height - 1; y++)
            {
                if (_grid[x, y] == 'A')
                {
                    count += IsXMAS(x, y) ? 1 : 0;
                }
            }
        }

        return count.ToString();
    }

    private bool IsXMAS(int x, int y)
    {
        var characters = new string(new char[] { _grid[x - 1, y - 1], _grid[x + 1, y - 1], _grid[x - 1, y + 1], _grid[x + 1, y + 1] });

        return
            characters == "MMSS" ||
            characters == "SMSM" ||
            characters == "SSMM" ||
            characters == "MSMS";
    }
}
