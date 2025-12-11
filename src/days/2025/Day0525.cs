using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0525 : IDay
{
    public DateTime date { get; } = new(2025, 12, 05);

    private Interval<long>[] ranges = [];
    private long[] ids = [];

    public void PopulateData(string raw)
    {
        string[] blocks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        // parse the ranges.
        string[] rangeStrings = blocks[0].Split(Utils.NEW_LINES,
                                                StringSplitOptions.RemoveEmptyEntries);
        ranges = new Interval<long>[rangeStrings.Length];
        for (int i = 0; i < rangeStrings.Length; ++i)
        {
            string[] range = rangeStrings[i].Split('-');
            long start = long.Parse(range[0]);
            long end = long.Parse(range[1]);

            ranges[i] = new Interval<long>(start, end);
        }

        ranges = [.. Interval.Collapse(ranges)];

        // parse the ids.
        string[] idStrings = blocks[1].Split(Utils.NEW_LINES,
                                             StringSplitOptions.RemoveEmptyEntries);
        ids = new long[idStrings.Length];
        for (int i = 0; i < ids.Length; ++i)
        {
            ids[i] = long.Parse(idStrings[i]);
        }

        Array.Sort(ids);
    }

    public string SolveStarOne()
    {
        long freshIngredients = 0;
        bool inRanges;

        for (int i = 0; i < ids.Length; ++i)
        {
            inRanges = false;

            for (int ii = 0; ii < ranges.Length; ii++)
            {
                if (!ranges[ii].Contains(ids[i]))
                {
                    continue;
                }

                inRanges = true;
                break;
            }

            if (!inRanges) { continue; }

            ++freshIngredients;
        }

        return freshIngredients.ToString();
    }

    public string SolveStarTwo()
    {
        long freshIDs = 0;

        for (int i = 0; i < ranges.Length; i++)
        {
            freshIDs += ranges[i].difference;
        }

        return freshIDs.ToString();
    }
}