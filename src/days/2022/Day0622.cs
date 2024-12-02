namespace advent_of_code.days;

public class Day0622 : IDay
{
    public DateTime date { get; } = new(2022, 12, 06);

    private string[] dataStreamBuffers = [];

    public void PopulateData(string raw)
    {
        dataStreamBuffers = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne() =>
        dataStreamBuffers.Aggregate(string.Empty, (current, dataStreamBuffer)
            => current + FindMarkerIndex(dataStreamBuffer, 4));

    public string SolveStarTwo() =>
        dataStreamBuffers.Aggregate(string.Empty, (current, dataStreamBuffer)
            => current + FindMarkerIndex(dataStreamBuffer, 14));

    private static int FindMarkerIndex(string source, int range)
    {
        for (int i = 0; i < source.Length - range; i++)
        {
            string subString = source.Substring(i, range);

            if (subString.Distinct().Count() != range) { continue; }

            return i + range;
        }

        return -1;
    }
}
