using Range = (uint min, uint max);

namespace advent_of_code.days;

internal class Day0525 : IDay
{
    public DateTime date { get; } = new(2025, 12, 05);

    private Range[] ranges = [];
    private uint[] ids = [];

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
            ranges[i].min = uint.Parse(range[0]);
            ranges[i].max = uint.Parse(range[1]);
        }

        // parse the ids.
        string[] idStrings = blocks[1].Split(Utils.NEW_LINES,
                                             StringSplitOptions.RemoveEmptyEntries);
        ids = new uint[idStrings.Length];
        for (int i = 0; i < ids.Length; ++i)
        {
            ids[i] = uint.Parse(idStrings[i]);
        }
    }

    public string SolveStarOne()
    {
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}