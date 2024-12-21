using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

namespace AdventOfCode2024.Code;

public class Day12 : BaseDay
{
    private HashSet<(int x, int y)> _visited = new();

    public Day12(string? filename = null) : base(filename)
    {
    }

    public override string Solution1()
    {
        HashSet<(int x, int y)> visited = new();
        int sum = 0;
        for (int x = 0; x < Input.Length; x++)
        {
            for (int y = 0; y < Input[x].Length; y++)
            {
                if (!visited.Contains((x, y)))
                {
                    (var area, var perimeter, _) = WalkPlot(x, y, visited);
                    sum += area * perimeter;
                }
            }
        }

        return sum.ToString();
    }

    public override string Solution2()
    {
        HashSet<(int x, int y)> visited = new();
        int sum = 0;
        for (int x = 0; x < Input.Length; x++)
        {
            for (int y = 0; y < Input[x].Length; y++)
            {
                if (!visited.Contains((x, y)))
                {
                    (var area, var perimeter, var outline) = WalkPlot(x, y, visited);
                    perimeter = CalculatePerimeterForBulkDiscount(outline);
                    sum += area * perimeter;
                }
            }
        }

        return sum.ToString();
    }

    private int CalculatePerimeterForBulkDiscount(List<(int startX, int startY, int endX, int endY)> outline)
    {
        var sideCount = 1;
        (int x, int y) currentPoint = (outline[0].endX, outline[0].endY);
        (int x, int y) currentDirection = (outline[0].endX - outline[0].startX, outline[0].endY - outline[0].startY);
        while (true)
        {
            var nextPoint = GetNextPoint(currentPoint, outline);
            if (nextPoint == (outline[0].startX, outline[0].startY) || nextPoint == null) break;

            var newDirection = (nextPoint.Value.x - currentPoint.x, nextPoint.Value.y - currentPoint.y);
            if (currentDirection != newDirection)
            {
                sideCount++;
            }

            currentPoint = nextPoint.Value;
            currentDirection = newDirection;
        }

        return sideCount;
    }

    private (int x, int y)? GetNextPoint((int x, int y) currentPoint, List<(int startX, int startY, int endX, int endY)> outline)
    {
        (int startX, int startY, int endX, int endY)? side = outline.Skip(1).SingleOrDefault(o => o.startX == currentPoint.x && o.startY == currentPoint.y);
        if (side == (0, 0, 0, 0))
        {
            side = outline.Skip(1).SingleOrDefault(o => o.endX == currentPoint.x && o.endY == currentPoint.y);
            if (side == (0, 0, 0, 0))
            {
                return null;
            }
            else
            {
                return (side.Value.startX, side.Value.startY);
            }
        }
        else
        {
            return (side.Value.endX, side.Value.endY);
        }

        
    }

    private (int area, int perimeter, List<(int startX, int startY, int endX, int endY)> outline) WalkPlot(int x, int y, HashSet<(int x, int y)> visited)
    {
        List<(int startX, int startY, int endX, int endY)> outline = new();
        int perimeter = 4 - NeighbourCount(x, y, out var newOutline);
        outline.AddRange(newOutline);
        int area = 1;
        visited.Add((x, y));

        if (y > 0 && !visited.Contains((x, y - 1)) && Input[y - 1][x] == Input[y][x])
        {
            (var  a, var p, var o) = WalkPlot(x, y - 1, visited);
            area += a;
            perimeter += p;
            outline.AddRange(o);
        }
        if (y < Input[x].Length - 1 && !visited.Contains((x, y + 1)) && Input[y + 1][x] == Input[y][x])
        {
            (var a, var p, var o) = WalkPlot(x, y + 1, visited);
            area += a;
            perimeter += p;
            outline.AddRange(o);
        }
        if (x > 0 && !visited.Contains((x - 1, y)) && Input[y][x - 1] == Input[y][x])
        {
            (var a, var p, var o) = WalkPlot(x - 1, y, visited);
            area += a;
            perimeter += p;
            outline.AddRange(o);
        }
        if (x < Input.Length - 1 && !visited.Contains((x + 1, y)) && Input[y][x + 1] == Input[y][x])
        {
            (var a, var p, var o) = WalkPlot(x + 1, y, visited);
            area += a;
            perimeter += p;
            outline.AddRange(o);
        }

        return (area, perimeter, outline);
    }

    private int NeighbourCount(int x, int y, out List<(int startX, int startY, int endX, int endY)> outline)
    {
        outline = new();
        var neighbourCount = 0;
        if (y > 0 && Input[y - 1][x] == Input[y][x])
        {
            neighbourCount++;
        }
        else
        {
            outline.Add((x, y, x + 1, y));
        }

        if (y < Input[x].Length - 1 && Input[y + 1][x] == Input[y][x])
        {
            neighbourCount++;
        }
        else
        {
            outline.Add((x, y + 1, x + 1, y + 1));
        }

        if (x > 0 && Input[y][x - 1] == Input[y][x])
        {
            neighbourCount++;
        }
        else
        {
            outline.Add((x, y, x, y + 1));
        }

        if (x < Input.Length - 1 && Input[y][x + 1] == Input[y][x])
        {
            neighbourCount++;
        }
        else
        {
            outline.Add((x + 1, y, x + 1, y + 1));
        }

        Debug.WriteLine($"x: {x}, y: {y}, nc: {4 - neighbourCount}");
        return neighbourCount;
    }
}