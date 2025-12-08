using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0825 : IDay
{
    public DateTime date { get; } = new(2025, 12, 08);

    private Vector3Int[] junctionBoxes = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        junctionBoxes = new Vector3Int[lines.Length];

        for (int i = 0; i < junctionBoxes.Length; ++i)
        {
            string[] values = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            int z = int.Parse(values[2]);

            junctionBoxes[i] = new Vector3Int(x, y, z);
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