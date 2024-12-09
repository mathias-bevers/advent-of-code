
using System.Data.Common;
using System.Text;

namespace advent_of_code.days;

internal class Day0924 : IDay
{
    public DateTime date { get; } = new(2024, 12, 09);

    private const int EMPTY_INT = -1;

    private string denseDiskMap = string.Empty;

    public void PopulateData(string raw)
    {
        denseDiskMap = raw.Trim();
    }

    public string SolveStarOne()
    {
        List<int> fileBlocks = [];

        for (int i = 0; i < denseDiskMap.Length; ++i)
        {
            bool isBlockFile = i % 2 == 0;

            int count = (int)char.GetNumericValue(denseDiskMap[i]);

            if (isBlockFile)
            {
                int id = i / 2;
                fileBlocks.AddRange(Enumerable.Repeat(id, count));
            }
            else
            {
                fileBlocks.AddRange(Enumerable.Repeat(EMPTY_INT, count));
            }
        }

        do
        {
            int firstEmpty = -1;
            int lastDigit = -1;

            for (int i = 0; i < fileBlocks.Count; ++i)
            {
                if (fileBlocks[i] != EMPTY_INT) { continue; }

                firstEmpty = i;
                break;
            }

            for (int i = fileBlocks.Count - 1; i >= 0; --i)
            {
                if (fileBlocks[i] == EMPTY_INT) { continue; }

                lastDigit = i;
                break;
            }

            fileBlocks[firstEmpty] = fileBlocks[lastDigit];
            fileBlocks[lastDigit] = EMPTY_INT;
        }
        while (!IsCompacted());

        long fileSystemCheckSum = 0;

        for (int i = 0; i < fileBlocks.Count; ++i)
        {
            if (fileBlocks[i] == EMPTY_INT) { break; }

            fileSystemCheckSum += i * fileBlocks[i];
        }

        return fileSystemCheckSum.ToString();

        bool IsCompacted()
        {
            bool hasVisitedEmpty = false;

            for (int i = 0; i < fileBlocks.Count; ++i)
            {
                if (!hasVisitedEmpty)
                {
                    hasVisitedEmpty = fileBlocks[i] == EMPTY_INT;
                    continue;
                }

                if (fileBlocks[i] > 0)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public string SolveStarTwo()
    {
        List<FileBlock> fileBlocks = [];
        int index = 0;
        for (int i = 0; i < denseDiskMap.Length; ++i)
        {
            int id = (i % 2 == 0) ? i / 2 : EMPTY_INT;
            int size = (int)char.GetNumericValue(denseDiskMap[i]);
            index += size;

            fileBlocks.Add(new FileBlock(id, index, size));
        }

        for (int i = fileBlocks.Count - 1; i >= 0; --i)
        {
            FileBlock file = fileBlocks[i];

            if (file.id < 0) { continue; }

            for (int ii = 0; ii < fileBlocks.Count; ++ii)
            {
                FileBlock empty = fileBlocks[ii];
                if (empty.id >= 0 || empty.index > file.index) { continue; }

                if (empty.size < file.size) { continue; }

                (file.index, empty.index) = (empty.index, file.index);

                if (file.size < empty.size)
                {
                    int difference = empty.size - file.size;
                    empty.size = file.size;
                    fileBlocks.Add(new FileBlock(EMPTY_INT, file.index + file.size, difference));
                    ++i;
                }

                break;
            }
        }

        fileBlocks.Sort((a, b) => a.index.CompareTo(b.index));


        List<int> fileSystem = [];
        for (int i = 0; i < fileBlocks.Count; ++i)
        {
            FileBlock f = fileBlocks[i];
            fileSystem.AddRange(Enumerable.Repeat(f.id, f.size));
        }

        long fileSystemCheckSum = 0;

        for (int i = 0; i < fileSystem.Count; ++i)
        {
            if (fileSystem[i] == EMPTY_INT) { continue; }

            fileSystemCheckSum += i * fileSystem[i];
        }

        return fileSystemCheckSum.ToString();
    }

    private class FileBlock(int id, int index, int size)
    {
        public int id { get; } = id;
        public int index { get; set; } = index;
        public int size { get; set; } = size;

        public override string ToString()
        {
            if (id < 0) { return new string('.', size); }

            return new string((char)(id + 48), size);
        }
    }
}