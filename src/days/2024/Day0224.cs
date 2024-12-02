
using System.Xml;

namespace advent_of_code.days;

internal class Day0224 : IDay
{
    public DateTime date { get; } = new(2024, 12, 02);

    private int[,] data = new int[0, 0];

    public void PopulateData(string raw)
    {
        string[] levels = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        string[] tmp = levels[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        data = new int[tmp.Length, levels.Length];

        for (int y = 0; y < data.GetLength(1); ++y)
        {
            string[] levelValues = levels[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            for (int x = 0; x < data.GetLength(0); ++x)
            {
                data[x,y] = int.Parse(levelValues[x]);
            }
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