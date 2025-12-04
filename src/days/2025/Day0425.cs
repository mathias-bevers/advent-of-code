using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0425 : IDay
{
    public DateTime date { get; } = new(2025, 12, 04);

    private const char ROLL = '@';
    private const char REMOVED_ROLL = '.';

    private Grid<char> original = new(0, 0);

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        original = new Grid<char>(lines[0].Length, lines.Length);

        for (int y = 0; y < original.height; y++)
        {
            for (int x = 0; x < original.width; x++)
            {
                original[x, y] = lines[y][x];
            }
        }
    }

    public string SolveStarOne()
    {
        Grid<char> grid = original.Copy();
        int accesibleCount = GetAccesibleRolls(1, grid);
        return accesibleCount.ToString();
    }

    public string SolveStarTwo()
    {
        Grid<char> grid = original.Copy();
        int accesibleCount = GetAccesibleRolls(10000, grid);
        return accesibleCount.ToString();
    }

    private int GetAccesibleRolls(int depth, Grid<char> grid)
    {
        if (depth == 0)
        {
            return 0;
        }

        int accesibleCount = 0;

        List<(int x, int y)> removedRolls = [];

        grid.Loop((roll, position) =>
        {
            if (roll != ROLL)
            {
                return;
            }

            int neighborCount = GetNeighborCount(position.x, position.y, grid);

            if (neighborCount >= 4)
            {
                return;
            }

            removedRolls.Add((position.x, position.y));
            ++accesibleCount;
        });

        if (accesibleCount == 0) { return 0; }

        for (int i = 0; i < removedRolls.Count; i++)
        {
            grid[removedRolls[i].x, removedRolls[i].y] = REMOVED_ROLL;
        }

        return accesibleCount + GetAccesibleRolls(depth - 1, grid);
    }

    private int GetNeighborCount(int x, int y, Grid<char> grid)
    {
        int neighborCount = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int ii = -1; ii <= 1; ii++)
            {
                if (i == 0 && ii == 0)
                {
                    continue;
                }

                int nX = x + ii;
                int nY = y + i;

                if (!grid.InGrid(nX, nY)) { continue; }

                if (grid[nX, nY] != ROLL)
                {
                    continue;
                }

                ++neighborCount;
            }
        }

        return neighborCount;
    }
}