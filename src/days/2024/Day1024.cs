
namespace advent_of_code.days;

internal class Day1024 : IDay
{
    public DateTime date { get; } = new(2024, 12, 10);

    private int[,] grid = new int[0, 0];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new int[rows.Length, rows[0].Length];

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                grid[x,y] = (int)char.GetNumericValue(rows[y][x]);
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