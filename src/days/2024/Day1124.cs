using Stone = (long value, int depth);

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

    private long ComputeBlinks(int blinks, long[] source)
    {
        Stack<Stone> stack = new();
        for (int i = 0; i < source.Length; ++i)
        {
            stack.Push((source[i], blinks));
        }

        long count = 0;

        while (stack.Count > 0)
        {
            Stone stone = stack.Pop();

            if (stone.depth == 0)
            {
                ++count;
                continue;
            }

            if (stone.value == 0)
            {
                stack.Push((1, stone.depth - 1));
                continue;
            }

            string stoneStr = stone.value.ToString();
            if (stoneStr.Length % 2 == 0)
            {
                int middle = stoneStr.Length / 2;
                stack.Push((long.Parse(stoneStr[..middle]), stone.depth - 1));
                stack.Push((long.Parse(stoneStr[middle..]), stone.depth - 1));
                continue;
            }

            stack.Push((stone.value * 2024, stone.depth - 1));
        }

        return count;
    }
}
