using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0624 : IDay
{
    public DateTime date { get; } = new(2024, 12, 06);

    private const char START_POINT = '^';

    private char[,] map = new char[0, 0];
    private Vector2Int position = new();

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        map = new char[rows.Length, rows[0].Length];

        for (int y = 0; y < rows.Length; ++y)
        {
            for (int x = 0; x < rows[y].Length; ++x)
            {

                char current = rows[y][x];
                map[x, y] = current;

                if (current != START_POINT) { continue; }

                position.x = x;
                position.y = y;
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