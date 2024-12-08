
using System.Diagnostics;
using System.Text;

namespace AdventOfCode2024.Code;

public class Day6 : BaseDay
{
    private readonly char[,] _grid;
    private readonly int _width;
    private readonly int _height;
    private readonly int _startX;
    private readonly int _startY;
    private readonly HashSet<(int x, int y)> _guardPath;

    private readonly Dictionary<(int oldOffsetX, int oldOffsetY), (int newOffsetX, int newOffsetY)> _turnTable = new()
    {
        { ( 0, -1), ( 1,  0) },
        { ( 1,  0), ( 0,  1) },
        { ( 0,  1), (-1,  0) },
        { (-1,  0), ( 0, -1) }
    };

    public Day6(string? filename = null) : base(filename)
    {
        _grid = new char[Input[0].Length, Input.Length];
        for (int y = 0; y < Input.Length; y++)
        {
            for (int x = 0; x < Input[0].Length; x++)
            {
                _grid[x, y] = Input[y][x];
                if (_grid[x, y] == '^')
                {
                    _startX = x;
                    _startY = y;
                }
            }
        }

        _width = _grid.GetLength(0);
        _height = _grid.GetLength(1);

        _guardPath = GetVisitedPath(null, out _);
    }

    public override string Solution1()
    {
        return _guardPath.Count().ToString();
    }

    private HashSet<(int x, int y)> GetVisitedPath((int x, int y)? extraObstruction, out bool loopDetected)
    {
        loopDetected = false;
        var currentDirection = (0, -1);
        var currentPosition = (_startX, _startY);
        var positionVisited = new HashSet<(int x, int y)>([ currentPosition ]);
        var positionsAndDirections = new HashSet<((int x, int y) position, (int x, int y) direction)>([(currentPosition, currentDirection)]);
        var steps = 0;
        while (Walk(ref currentDirection, ref currentPosition, extraObstruction))
        {
            if (positionsAndDirections.Contains((currentPosition, currentDirection)))
            {
                loopDetected = true;
                break;
            }
            steps++;
            positionVisited.Add(currentPosition);
            positionsAndDirections.Add((currentPosition, currentDirection));
        }

        return positionVisited;
    }

    private bool Walk(ref (int offsetX, int offsetY) direction, ref (int positionX, int positionY) position, (int x, int y)? extraObstruction = null)
    {
        if (!StaysWithinBounds(direction, position))
        {
            return false;
        }
        while (WillCollide(direction, position, extraObstruction))
        {
            TurnLeft(ref direction);
        }
        StepForward(ref direction, ref position);
        return true;
    }

    private bool StaysWithinBounds((int offsetX, int offsetY) direction, (int positionX, int positionY) position)
    {
        (int x, int y) newPosition = (position.positionX + direction.offsetX, position.positionY + direction.offsetY);
        return !(newPosition.x < 0 || newPosition.x >= _width || newPosition.y < 0 || newPosition.y >= _height);
    }

    private bool WillCollide((int offsetX, int offsetY) direction, (int positionX, int positionY) position, (int x, int y)? extraObstruction)
    {
        (int x, int y) newPosition = (position.positionX + direction.offsetX, position.positionY + direction.offsetY);
        return _grid[newPosition.x, newPosition.y] == '#' || (extraObstruction.HasValue && newPosition == extraObstruction);
    }

    private void TurnLeft(ref (int offsetX, int offsetY) direction)
    {
        direction = _turnTable[(direction.offsetX, direction.offsetY)];
    }

    private void StepForward(ref (int offsetX, int offsetY) direction, ref (int positionX, int positionY) position)
    {
        position = (position.positionX + direction.offsetX, position.positionY + direction.offsetY);
    }

    public override string Solution2()
    {
        var loopCount = 0;
        foreach (var obstructionPosition in _guardPath.Where(x => x != (_startX, _startY)))
        {
            var _ = GetVisitedPath(obstructionPosition, out var loopDetected);
            if (loopDetected)
            {
                loopCount++;
            }
        }

        return loopCount.ToString();
    }
}
