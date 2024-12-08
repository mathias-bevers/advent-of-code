
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0824 : IDay
{
    public DateTime date { get; } = new(2024, 12, 08);

    private const char EMPTY = '.';
    private char[,] grid = new char[0, 0];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new char[rows.Length, rows[0].Length];

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                grid[x, y] = rows[y][x];
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