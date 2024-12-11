namespace advent_of_code.days;

internal class Day1124 : IDay
{
    public DateTime date { get; } = new(2024, 12, 11);

    private const int BLINK_COUNT_STAR_ONE = 25;
    private const int BLINK_COUNT_STAR_TWO = 75;

    private long[] stones = [];

    public void PopulateData(string raw)
    {
        string[] stonesRaw = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        stones = new long[stonesRaw.Length];

        for (long i = 0; i < stonesRaw.Length; ++i)
        {
            stones[i] = long.Parse(stonesRaw[i]);
        }
    }

    public string SolveStarOne() => ComputeBlinks(BLINK_COUNT_STAR_ONE, stones).ToString();

    public string SolveStarTwo() => ComputeBlinks(BLINK_COUNT_STAR_TWO, stones).ToString();

    // public string SolveStarTwo() => throw new NotImplementedException();

    private static long ComputeBlinks(int blinks, long[] source)
    {
        Dictionary<long, long> stones = [];

        for (int i = 0; i < source.Length; ++i)
        {
            stones.Add(source[i], 1L);
        }

        for (int i = 0; i < blinks; ++i)
        {
            Dictionary<long, long> newDictionary = [];
            for (int ii = 0; ii < stones.Count; ++ii)
            {
                KeyValuePair<long, long> stone = stones.ElementAt(ii);

                long[] processedStones = EvaluateStone(stone.Key);
                for (int iii = 0; iii < processedStones.Length; ++iii)
                {
                    long processedStone = processedStones[iii];
                    if (!newDictionary.TryAdd(processedStone, stone.Value))
                    {
                        newDictionary[processedStone] += stone.Value;
                    }
                }
            }

            stones = newDictionary;
        }

        return stones.Values.Sum();
    }

    private static long[] EvaluateStone(long stone)
    {
        if (stone == 0) { return [1]; }

        string stoneStr = stone.ToString();
        if (stoneStr.Length % 2 == 0)
        {
            int middle = stoneStr.Length / 2;
            long a = long.Parse(stoneStr[..middle]);
            long b = long.Parse(stoneStr[middle..]);
            return [a, b];
        }

        return [stone * 2024];
    }
}