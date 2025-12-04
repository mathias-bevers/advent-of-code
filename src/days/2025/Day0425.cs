using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0425 : IDay
{
    public DateTime date { get; } = new(2025, 12, 04);

    private const char ROLL = '@';
    private const char REMOVED_ROLL = 'x';
    private char[,] paperRolls = new char[0, 0];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        paperRolls = new char[lines[0].Length, lines.Length];

        for (int y = 0; y < paperRolls.GetLength(1); y++)
        {
            for (int x = 0; x < paperRolls.GetLength(0); x++)
            {
                paperRolls[x, y] = lines[y][x];
            }
        }
    }

    public string SolveStarOne()
    {
        char[,] grid = paperRolls.Clone() as char[,] ??
            throw new NullReferenceException("cannot copy array.");
        int accesibleCount = GetAccesibleRolls(1, ref grid);
        return accesibleCount.ToString();
    }

    public string SolveStarTwo()
    {
        char[,] grid = paperRolls.Clone() as char[,] ??
            throw new NullReferenceException("cannot copy array.");
        int accesibleCount = GetAccesibleRolls(10000, ref grid);
        return accesibleCount.ToString();
    }

    private int GetAccesibleRolls(int depth, ref char[,] grid)
    {
        if (depth == 0)
        {
            return 0;
        }

        int accesibleCount = 0;

        List<(int x, int y)> removedRolls = [];

        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x, y] != ROLL)
                {
                    continue;
                }

                int neighborCount = GetNeighborCount(x, y, ref grid);

                if (neighborCount >= 4)
                {
                    continue;
                }

                removedRolls.Add((x, y));
                ++accesibleCount;
            }
        }

        if (accesibleCount == 0) { return 0; }

        for (int i = 0; i < removedRolls.Count; i++)
        {
            grid[removedRolls[i].x, removedRolls[i].y] = REMOVED_ROLL;
        }

        return accesibleCount + GetAccesibleRolls(depth - 1, ref grid);
    }

    private int GetNeighborCount(int x, int y, ref char[,] grid)
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

                if (nX < 0 || nY < 0)
                {
                    continue;
                }

                if (nX >= grid.GetLength(0) || nY >= grid.GetLength(1))
                {
                    continue;
                }

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