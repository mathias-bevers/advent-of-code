using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0320 : IDay
{
    public DateTime date { get; } = new(2020, 12, 03);

    //private char[,] data = new char[0, 0];
    private Grid<char> grid = new(0, 0);

    public void PopulateData(string raw)
    {
        string[] file = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new Grid<char>(file[0].Length, file.Length);

        for (int y = 0; y < file.Length; y++)
        {
            for (int x = 0; x < file[y].Length; x++)
            {
                grid[x, y] = file[y][x];
            }
        }
    }

    public string SolveStarOne() => CheckPath(3, 1).ToString();

    public string SolveStarTwo()
    {
        int totalTreeEncounters = CheckPath(1, 1) * CheckPath(3, 1) * CheckPath(5, 1) 
            * CheckPath(7, 1) * CheckPath(1, 2);
        return totalTreeEncounters.ToString();
    }

    private int CheckPath(int right, int down)
    {
        int treeEncounters = 0;

        for (int y = 0, x = 0; y < grid.height; y += down, x += right)
        {
            if (x >= grid.width) { x -= grid.width; }

            if (grid[x, y] == '#') { treeEncounters++; }
        }

        return treeEncounters;
    }
}