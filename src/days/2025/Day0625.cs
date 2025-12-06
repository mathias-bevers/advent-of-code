using System.Net.Security;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0625 : IDay
{
    public DateTime date { get; } = new(2025, 12, 06);

    private Grid<int> problems = new(0, 0);
    private char[] operators = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        // add the operators to their own char array.
        string[] stringOperators = lines[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
        operators = new char[stringOperators.Length];
        for (int i = 0; i < operators.Length; i++)
        {
            operators[i] = stringOperators[i][0];
        }

        // skip the last line since it holds the opertators.
        List<List<int>> tmp = new();
        for (int i = 0; i < lines.Length - 1; ++i)
        {
            List<int> innerTmp = new();
            string[] numbers = lines[i].Split(" ", StringSplitOptions.RemoveEmptyEntries);
            for (int ii = 0; ii < numbers.Length; ++ii)
            {
                innerTmp.Add(int.Parse(numbers[ii]));
            }

            tmp.Add(innerTmp);
        }

        // parse tmp lists to grid.
        problems = new Grid<int>(tmp[0].Count, tmp.Count);
        for (int y = 0; y < problems.height; ++y)
        {
            for (int x = 0; x < problems.width; ++x)
            {
                problems[x,y] = tmp[y][x];
            }
        }

        Logger.Info(problems);
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