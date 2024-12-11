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
        var freeBlocks = Input[0].Where((_, index) => index % 2 != 0).Select(c => int.Parse(c.ToString())).ToArray();

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
                sum += compressedSpaceIndex * currentFileBlock;
            }
            else
            {
                currentFileBlock = fileBlocks[currentFileBlockIndex];
                for (int freeBlock = 0; freeBlock < freeBlocks[freeBlockIndex]; freeBlock++)
                {
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

    private record FileBlock(int Size, int FileId)
    {
        public bool Processed { get; set; } = false;
        public bool Moved { get; set; } = false;

    }

    private record FreeBlock(int Size, List<FileBlock> FileBlocks)
    {
        public int FreeSpace => Size - FileBlocks.Sum(x => x.Size);
    }

    public override string Solution2()
    {
        var fileBlocks = Input[0].Where((_, i) => i % 2 == 0).Select((c, i) => new FileBlock(int.Parse(c.ToString()), i)).Reverse().ToArray();
        var freeBlocks = Input[0].Where((_, i) => i % 2 != 0).Select(c => new FreeBlock(int.Parse(c.ToString()), new())).ToArray();

        foreach (var freeBlock in freeBlocks)
        {
            foreach (var fileBlock in fileBlocks.Where(fileBlock => !fileBlock.Processed))
            {
                if (freeBlock.FreeSpace == 0) break;

                if (fileBlock.Size <= freeBlock.FreeSpace)
                {
                    freeBlock.FileBlocks.Add(fileBlock);
                    fileBlock.Processed = true;
                }
            }
        }

        foreach (var fileBlock in fileBlocks)
        {
            if (freeBlocks.Any(freeBlock => freeBlock.FileBlocks.Contains(fileBlock)))
            {
                fileBlock.Moved = true;
            }
        }

        long sum = 0;
        //var newFileId = 0;
        //var index = 0;
        //foreach (var fileBlock in fileBlocks.Reverse().ToArray())
        //{
        //    if (!fileBlock.Moved)
        //    {
        //        for (int fileBlockIndex = 0; fileBlockIndex < fileBlock.Size; fileBlockIndex++)
        //        {
        //            sum += fileBlock.FileId * newFileId++;
        //        }
        //    }
        //    else
        //    {
        //        newFileId += fileBlock.Size;
        //    }

        //    if (freeBlocks.Length == index) break;

        //    var freeBlock = freeBlocks[index];
        //    foreach (var movedFileBlock in freeBlock.FileBlocks)
        //    {
        //        for (int fileBlockIndex = 0; fileBlockIndex < movedFileBlock.Size; fileBlockIndex++)
        //        {
        //            sum += movedFileBlock.FileId * newFileId++;
        //        }
        //    }
        //    newFileId += freeBlock.FreeSpace;

        //    index++;
        //}





        return sum.ToString();
    }
}