using System.Numerics;
using Microsoft.VisualBasic;

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
        List<int> tmp = [];
        for (int i = 0; i < denseDiskMap.Length; ++i)
        {
            bool isBlockFile = i % 2 == 0;

            int count = (int)char.GetNumericValue(denseDiskMap[i]);

            if (isBlockFile)
            {
                int id = i / 2;
                tmp.AddRange(Enumerable.Repeat(id, count));
            }
            else
            {
                tmp.AddRange(Enumerable.Repeat(EMPTY_INT, count));
            }
        }

        int[] fileBlocks = [.. tmp];

        int lastOccurance = fileBlocks.Length - 1;
        int currentID = fileBlocks[lastOccurance];

        for (int i = fileBlocks.Length - 1; i >= 0; --i)
        {
            if (fileBlocks[i] == currentID) { continue; }

            if (currentID != EMPTY_INT)
            {
                int size = lastOccurance - i;

                int emptyIndex = FindFirstEmptySpace(fileBlocks, i, size);

                if (emptyIndex >= 0)
                {
                    for (int ii = 0; ii < size; ++ii)
                    {
                        int t = emptyIndex + ii;
                        fileBlocks[t] = currentID;
                        fileBlocks[lastOccurance - ii] = EMPTY_INT;
                    }
                }
            }

            lastOccurance = i;
            currentID = fileBlocks[i];
        }

        string log = string.Empty;

        long fileSystemCheckSum = 0;

        for (int i = 0; i < fileBlocks.Length; ++i)
        {
            if (fileBlocks[i] == EMPTY_INT)
            {
                log += '.';
                continue;
            }

            log += fileBlocks[i];
            fileSystemCheckSum += i * fileBlocks[i];
        }
        
        return fileSystemCheckSum.ToString();
    }

    private static int FindFirstEmptySpace(int[] source, int index, int size)
    {
        for (int i = 0; i < index; ++i)
        {
            if (source[i] != EMPTY_INT) { continue; }

            for (int ii = 0; ii < size; ++ii)
            {
                if (source[i + ii] != EMPTY_INT) { break; }

                if (ii == size - 1) { return i; }
            }
        }

        return -1;
    }
}