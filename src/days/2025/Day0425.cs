using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0425 : IDay
{
    public DateTime date { get; } = new(2025, 12, 04);

    private const char ROLL = '@';
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
        int accesibleCount = 0;

        for (int y = 0; y < paperRolls.GetLength(1); y++)
        {
            for (int x = 0; x < paperRolls.GetLength(0); x++)
            {
                if (paperRolls[x, y] != ROLL)
                {
                    continue;
                }

                int neighborCount = GetNeighborCount(x, y);

                if (neighborCount >= 4)
                {
                    continue;
                }

                ++accesibleCount;
            }
        }
        
        return accesibleCount.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private int GetNeighborCount(int x, int y)
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

                if(nX >= paperRolls.GetLength(0) || nY >= paperRolls.GetLength(1))
                {
                    continue;
                }

                if(paperRolls[nX, nY] != ROLL)
                {
                    continue;
                }

                ++neighborCount;
            }
        }

        return neighborCount;
    }
}