using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0125 : IDay
{
    public DateTime date { get; } = new(2025, 12, 01);

    private int[] rotations = Array.Empty<int>();

    public void PopulateData(string raw)
    {
        raw = raw.Replace('L', '-');
        raw = raw.Replace('R', '+');

        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        rotations = new int[lines.Length];

        for (int i = 0; i < rotations.Length; ++i) {  rotations[i] = int.Parse(lines[i]); }
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