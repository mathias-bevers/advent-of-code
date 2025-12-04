using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0425 : IDay
{
    public DateTime date { get; } = new(2025, 12, 04);
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
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}