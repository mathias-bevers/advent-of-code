using System.Collections;

namespace advent_of_code.days;

internal class Day1124 : IDay
{
    public DateTime date { get; } = new(2024, 12, 11);

    private const int BLINK_COUNT_STAR_ONE = 25;
    private const int BLINK_COUNT_STAR_TWO = 75;

    private readonly CancellationTokenSource cts;

    private long[] stones = [];

    public Day1124()
    {
        cts = new CancellationTokenSource();
    }

    ~Day1124()
    {
        cts.Cancel();
    }

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

    private int ComputeBlinks(int blinks, long[] source)
    {
        ArrayList stones = [.. source];
        List<Task> tasks = [];

        for (int i = 0; i < blinks; ++i)
        {
            for (int ii = stones.Count - 1; ii >= 0; ii--)
            {
                int index = ii;
                ArrayList current = ArrayList.Synchronized(stones);
                Task task = Task.Run(async () => await ProcessStone(index, current), cts.Token);
                tasks.Add(task);
            }

            Task.WaitAll([.. tasks]);
            tasks.Clear();
        }

        return stones.Count;
    }

    private static async Task ProcessStone(int index, ArrayList stones)
    {
        await Task.Yield();

        long stone = Convert.ToInt64(stones[index]);
        string stoneStr = stone.ToString();

        if (stone == 0)
        {
            stones[index] = 1L;
        }
        else if (stoneStr.Length % 2 == 0)
        {

            int middle = stoneStr.Length / 2;
            stones[index] = long.Parse(stoneStr.Substring(0, middle));
            stones.Insert(index + 1, long.Parse(stoneStr.Substring(middle)));

        }
        else
        {
            stone *= 2024;
            stones[index] = stone;
        }
    }
}
