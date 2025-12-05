using Range = (long min, long max);

namespace advent_of_code.days;

internal class Day0525 : IDay
{
    public DateTime date { get; } = new(2025, 12, 05);

    private Range[] ranges = [];
    private long[] ids = [];

    public void PopulateData(string raw)
    {
        string[] blocks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        // parse the ranges.
        string[] rangeStrings = blocks[0].Split(Utils.NEW_LINES,
                                                StringSplitOptions.RemoveEmptyEntries);
        ranges = new Range[rangeStrings.Length];
        for (int i = 0; i < ranges.Length; ++i)
        {
            string[] range = rangeStrings[i].Split('-');
            ranges[i].min = long.Parse(range[0]);
            ranges[i].max = long.Parse(range[1]);
        }

        // parse the ids.
        string[] idStrings = blocks[1].Split(Utils.NEW_LINES,
                                             StringSplitOptions.RemoveEmptyEntries);
        ids = new long[idStrings.Length];
        for (int i = 0; i < ids.Length; ++i)
        {
            ids[i] = long.Parse(idStrings[i]);
        }
    }

    public string SolveStarOne()
    {
        long freshIngredients = 0;

        for (int i = 0; i < ids.Length; ++i)
        {
            for (int ii = 0; ii < ranges.Length; ++ii)
            {
                // check if in range
                if(ids[i] < ranges[ii].min)
                {
                    continue;
                }

                if(ids[i] > ranges[ii].max)
                {
                    continue;
                }

                // increase the fresh ingredient count and break so it will not double count.
                ++freshIngredients;
                break;
            }
        }

        return freshIngredients.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}