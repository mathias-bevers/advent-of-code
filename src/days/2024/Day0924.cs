
using System.Text;

namespace advent_of_code.days;

internal class Day0924 : IDay
{
    public DateTime date { get; } = new(2024, 12, 09);

    private const char EMPTY_CHAR = '.';
    private const long EMPTY_LONG = -1;

    private string denseDiskMap = string.Empty;

    public void PopulateData(string raw)
    {
        denseDiskMap = raw.Trim();
    }

    public string SolveStarOne()
    {
        List<long> fileBlocks = new();

        for (int i = 0; i < denseDiskMap.Length; ++i)
        {
            bool isBlockFile = i % 2 == 0;

            int count = (int)Char.GetNumericValue(denseDiskMap[i]);

            if (isBlockFile)
            {
                long id = i / 2;
                fileBlocks.AddRange(Enumerable.Repeat(id, count));
            }
            else
            {
                fileBlocks.AddRange(Enumerable.Repeat(EMPTY_LONG, count));
            }
        }

        do
        {
            int firstEmpty = -1;
            int lastDigit = -1;

            for (int i = 0; i < fileBlocks.Count; ++i)
            {
                if (fileBlocks[i] != EMPTY_LONG) { continue; }

                firstEmpty = i;
                break;
            }

            for (int i = fileBlocks.Count - 1; i >= 0; --i)
            {
                if (fileBlocks[i] == EMPTY_LONG) { continue; }

                lastDigit = i;
                break;
            }

            fileBlocks[firstEmpty] = fileBlocks[lastDigit];
            fileBlocks[lastDigit] = EMPTY_LONG;
        }
        while (!IsCompacted(fileBlocks));

        long fileSystemCheckSum = 0;

        for (int i = 0; i < fileBlocks.Count; ++i)
        {
            if (fileBlocks[i] == -1) { break; }

            long temp = i * fileBlocks[i];
            fileSystemCheckSum += temp;
        }

        return fileSystemCheckSum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private static bool IsCompacted(List<long> input)
    {
        bool hasVisitedEmpty = false;

        for (int i = 0; i < input.Count; ++i)
        {
            if (!hasVisitedEmpty)
            {
                hasVisitedEmpty = input[i] == EMPTY_LONG;
                continue;
            }

            if (input[i] > 0)
            {
                return false;
            }
        }

        return true;
    }
}