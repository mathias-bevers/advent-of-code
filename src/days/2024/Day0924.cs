
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
            if (fileBlocks[i] == -1) { break; }

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
            int id = (i % 2 == 0) ? i / 2 : -1;
            int size = (int)char.GetNumericValue(denseDiskMap[i]);
            index += size;

            fileBlocks.Add(new FileBlock(id, index, size));
        }

        for (int i = fileBlocks.Count - 1; i >= 0; --i)
        {
            FileBlock fileBlock = fileBlocks[i];

            if (fileBlock.id < 0) { continue; }

            for (int ii = 0; ii < fileBlocks.Count; ++ii)
            {
                FileBlock other = fileBlocks[ii];
                if (other.id >= 0 || other.index > fileBlock.index) { continue; }

                if (other.size < fileBlock.size) { continue; }

                (fileBlock.index, other.index) = (other.index, fileBlock.index);
                break;
            }
        }

        utils.Logger.WriteToLogFile(string.Join("", fileBlocks));

        long fileSystemCheckSum = 0;

        return fileSystemCheckSum.ToString();
    }

    private class FileBlock(int id, int index, int size)
    {
        public int id { get; } = id;
        public int index { get; set; } = index;
        public int size { get; } = size;

        public override string ToString()
        {
            if (id < 0) { return new string('.', size); }

            return new string((char)(id + 48), size);
        }
    }
}