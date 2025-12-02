namespace advent_of_code.days;
using Range = (int min, int max);

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
            int min = int.Parse(parts[0]);
            int max = int.Parse(parts[1]);

            ranges[i] = (min, max);
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