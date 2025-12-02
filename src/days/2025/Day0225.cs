namespace advent_of_code.days;

using advent_of_code.utils;
using Range = (long min, long max);

internal class Day0225 : IDay
{
    public DateTime date { get; } = new(2025, 12, 02);
    private Range[] ranges = Array.Empty<Range>();

    public void PopulateData(string raw)
    {
        string[] rawRanges = raw.Split(',', StringSplitOptions.RemoveEmptyEntries);
        ranges = new Range[rawRanges.Length];

        for (int i = 0; i < ranges.Length; i++)
        {
            string[] parts = rawRanges[i].Split('-');
            long min = long.Parse(parts[0]);
            long max = long.Parse(parts[1]);

            ranges[i] = (min, max);
        }
    }

    public string SolveStarOne()
    {
        long sumInvalidIDs = 0;

        for (int i = 0; i < ranges.Length; ++i)
        {
            for (long ii = ranges[i].min; ii <= ranges[i].max; ii++)
            {
                // check if the number has an even amount of digids.
                if (Math.Floor(Math.Log10(ii) + 1) % 2 != 0)
                {
                    continue;
                }

                // split the string in two so compare.
                string id = ii.ToString();
                string begin = id[..(id.Length / 2)];
                string end = id[(id.Length / 2)..];

                if (!string.Equals(begin, end))
                {
                    continue;
                }

                sumInvalidIDs += ii;
            }

        }

        return sumInvalidIDs.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}