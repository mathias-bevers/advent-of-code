using System.Text.RegularExpressions;

namespace advent_of_code.days;

internal class Day0324 : IDay
{
    public DateTime date { get; } = new(2024, 12, 03);

    string[] multiplications = [];

    public void PopulateData(string raw)
    {
        MatchCollection maches = Regex.Matches(raw,  @"mul\([0-9]{1,3},[0-9]{1,3}\)");

        multiplications = new string[maches.Count];
        for (int i = 0; i < multiplications.Length; ++i)
        {
            multiplications[i] = maches[i].Value;
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