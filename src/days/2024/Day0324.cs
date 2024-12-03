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
        int instructionSum = 0;

        for(int i = 0; i < multiplications.Length; ++i)
        {
            string multiplication = multiplications[i];
            multiplication = multiplication[4..];
            multiplication = multiplication[..^1];

            string[] digits = multiplication.Split(',', StringSplitOptions.RemoveEmptyEntries);
            instructionSum += int.Parse(digits[0]) * int.Parse(digits[1]);
        }

        return instructionSum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}