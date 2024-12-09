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

        int lastOccurance = fileBlocks.Count - 1;
        int idA = fileBlocks[lastOccurance];

        for (int i = fileBlocks.Count - 1; i >= 0; --i)
        {
            if (fileBlocks[i] == idA) { continue; }

            if (idA != EMPTY_INT)
            {
                int sizeA = lastOccurance - i;

                int firstOccurrance = 0;
                int idB = fileBlocks[firstOccurrance];

                for (int ii = 0; ii < fileBlocks.Count; ++ii)
                {
                    if (fileBlocks[ii] == idB) { continue; }

                    if (ii >= i) { break; }

                    if (idB == EMPTY_INT)
                    {
                        int sizeB = ii - firstOccurrance;

                        if (sizeA <= sizeB)
                        {
                            for (int iii = 0; iii < sizeA; ++iii)
                            {
                                fileBlocks[firstOccurrance + iii] = idA;
                                fileBlocks[lastOccurance - iii] = EMPTY_INT;
                            }

                            break;
                        }

                    }

                    firstOccurrance = ii;
                    idB = fileBlocks[ii];
                }
            }

            lastOccurance = i;
            idA = fileBlocks[i];
        }

        string log = string.Empty;

        long fileSystemCheckSum = 0;

        for (int i = 0; i < fileBlocks.Count; ++i)
        {
            if (fileBlocks[i] == EMPTY_INT)
            {
                log += '.';
                continue;
            }

            log += fileBlocks[i];
            fileSystemCheckSum += i * fileBlocks[i];
        }

        utils.Logger.WriteToLogFile(log);

        return fileSystemCheckSum.ToString();
    }
}