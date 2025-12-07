using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0725 : IDay
{
    public DateTime date { get; } = new(2025, 12, 07);

    private const char START = 'S';
    private const char SPLITTER = '^';

    private Grid<char> manifold = new(0, 0);
    private Vector2Int startPosition = new(0, 0);

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        manifold = new Grid<char>(rows[0].Length, rows.Length);

        for (int y = 0; y < manifold.height; ++y)
        {
            for (int x = 0; x < manifold.width; x++)
            {
                manifold[x, y] = rows[y][x];

                if (manifold[x, y] != START)
                {
                    continue;
                }

                startPosition = new Vector2Int(x, y);
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