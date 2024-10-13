using advent_of_code.utils;
using System.Text.RegularExpressions;

namespace advent_of_code.days;

internal class Day0123 : IDay
{
    public DateTime date => new(2023, 12, 01);

    private string[] data = [];
    private readonly Dictionary<string, int> spelledOut = new(){
            {"one", 1},
            {"two", 2},
            {"three",3 },
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
    };

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int sum = 0;

        foreach (string line in data)
        {
            string number = string.Empty;

            for (int i = 0; i < line.Length; ++i)
            {
                if (!char.IsDigit(line[i])) { continue; }

                number += line[i];
                break;
            }

            for (int i = line.Length - 1; i >= 0; --i)
            {
                if (!char.IsDigit(line[i])) { continue; }

                number += line[i];
                break;
            }

            try { sum += int.Parse(number); }
            catch (FormatException) { Logger.Error($"Cannot parse \'{number}\' to int! Sum is unchanged."); }
        }

        return sum.ToString();
    }

    public string SolveStarTwo()
    {
        int sum = 0;
        const string matchString = "[1-9]|one|two|three|four|five|six|seven|eight|nine";
        Regex startToEnd = new(matchString);
        Regex endToStart = new(matchString, RegexOptions.RightToLeft);

        for (int i = 0; i < data.Length; ++i)
        {
            string line = data[i];

            string first = startToEnd.Matches(line).First().Value;
            if (first.Length > 1) { first = spelledOut[first].ToString(); }

            string last = endToStart.Matches(line).First().Value;
            if (last.Length > 1) { last = spelledOut[last].ToString(); }

            sum += int.Parse(first + last);
        }

        return sum.ToString();
    }
}