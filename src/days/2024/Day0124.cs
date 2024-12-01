
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0124 : IDay
{
    public DateTime date { get; } = new DateTime(2024, 12, 01);
    
    private int[][] data = new int[2][];

    public void PopulateData(string raw)
    {
        string[] pairLines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data[0] = new int[pairLines.Length];
        data[1] = new int[pairLines.Length];

        for(int i = 0; i < pairLines.Length; ++i)
        {
            string[] numbers = pairLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            data[0][i] = int.Parse(numbers[0]);
            data[1][i] = int.Parse(numbers[1]);       
        }

        Array.Sort(data[0]);
        Array.Sort(data[1]);
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