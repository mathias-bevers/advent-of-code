
namespace advent_of_code.days;

internal class Day0424 : IDay
{
    public DateTime date { get; } = new(2024, 12, 04);

    private char[,] grid = new char[0, 0];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new char[rows[0].Length, rows.Length];

        for (int y = 0; y < rows.Length; ++y)
        {
            for (int x = 0; x < rows[y].Length; ++x)
            {
                grid[x,y] = rows[y][x];
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