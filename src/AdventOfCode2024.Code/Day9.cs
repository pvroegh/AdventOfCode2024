using System.Runtime.CompilerServices;

namespace AdventOfCode2024.Code;

public class Day9 : BaseDay
{
    private readonly char[,] _grid;
    private readonly int _width;
    private readonly int _height;

    

    public Day9(string? filename = null) : base(filename)
    {
        }

    public override string Solution1()
    {
        var fileBlocks = Input[0]
            .Where((_, index) => index % 2 == 0)
            .Select((c, index) => new { Size = int.Parse(c.ToString()), FileId = index })
            .Select(x => Enumerable.Range(0, x.Size).Select(i => x.FileId))
            .SelectMany(x => x).ToArray();
        var fileBlocksReversed = fileBlocks.Reverse().ToArray();
        var freeBlocks = Input[0].Where((_, index) => index % 2 != 0).Select(c => int.Parse(c.ToString())).ToArray();

        var compressedSpace = new List<int>();

        long sum = 0;
        int currentFileBlock = fileBlocks[0];
        int currentFileBlockIndex = 0;
        int freeBlockIndex = 0;
        int indexFromBack = 0;
        for (int compressedSpaceIndex = 0; compressedSpaceIndex < fileBlocks.Length; compressedSpaceIndex++)
        {
            if (currentFileBlock == fileBlocks[currentFileBlockIndex])
            {
                currentFileBlockIndex++;
                compressedSpace.Add(currentFileBlock);
                sum += compressedSpaceIndex * currentFileBlock;
            }
            else
            {
                currentFileBlock = fileBlocks[currentFileBlockIndex];
                for (int freeBlock = 0; freeBlock < freeBlocks[freeBlockIndex]; freeBlock++)
                {
                    compressedSpace.Add(fileBlocks[fileBlocks.Length - indexFromBack - 1]);
                    sum += compressedSpaceIndex * fileBlocks[fileBlocks.Length - indexFromBack - 1];
                    indexFromBack++;
                    compressedSpaceIndex++;
                }
                freeBlockIndex++;
                compressedSpaceIndex--;
            }
        }

        return sum.ToString();
    }
}