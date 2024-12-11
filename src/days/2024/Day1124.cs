
namespace advent_of_code.days;

internal class Day1124 : IDay
{
    public DateTime date { get; } = new(2024, 12, 11);

    private const int BLINK_COUNT = 25;

    private long[] initialStones = [];

    public void PopulateData(string raw)
    {
        string[] stonesRaw = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        initialStones = new long[stonesRaw.Length];

        for (long i = 0; i < stonesRaw.Length; ++i)
        {
            initialStones[i] = long.Parse(stonesRaw[i]);
        }
    }

    public string SolveStarOne()
    {
        List<long> stones = [.. initialStones];

        for (int i = 0; i < BLINK_COUNT; ++i)
        {
            for (int ii = stones.Count - 1; ii >= 0; --ii)
            {
                long stone = stones[ii];
                if (stone == 0)
                {
                    stones[ii] = 1;
                    continue;
                }

                string stoneString = stone.ToString();
                if(stoneString.Length % 2 == 0) 
                {
                    int middle = stoneString.Length / 2;
                    stones[ii] = long.Parse(stoneString.Substring(0, middle));
                    stones.Insert(ii + 1, long.Parse(stoneString.Substring(middle)));
                    continue;
                }

                stone *= 2024;

                if(stone < 0) { throw new OverflowException("long is to small"); }

                stones[ii] = stone;
            }
        }

        return stones.Count.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}