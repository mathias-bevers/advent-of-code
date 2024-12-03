using System.Text.RegularExpressions;

namespace advent_of_code.days;

internal class Day0324 : IDay
{
    public DateTime date { get; } = new(2024, 12, 03);

    (int index, string instruction)[] instructionPairs = [];

    public void PopulateData(string raw)
    {
        MatchCollection maches =
            Regex.Matches(raw, @"(mul\([0-9]{1,3},[0-9]{1,3}\))|(don't\(\))|(do\(\))");

        instructionPairs = new (int, string)[maches.Count];
        for (int i = 0; i < instructionPairs.Length; ++i)
        {
            Match match = maches[i];
            instructionPairs[i] = (match.Index, match.Value);
        }

        Array.Sort(instructionPairs, (a, b) => a.index.CompareTo(b.index));
    }

    public string SolveStarOne()
    {
        int instructionSum = 0;

        for (int i = 0; i < instructionPairs.Length; ++i)
        {
            string instruction = instructionPairs[i].instruction;

            if (!instruction.StartsWith("mul(")) { continue; }

            instructionSum += ParseMuliplicationInstruction(instruction);
        }

        return instructionSum.ToString();
    }

    public string SolveStarTwo()
    {
        int instructionSum = 0;
        bool isEnabled = true;

        for (int i = 0; i < instructionPairs.Length; ++i)
        {
            string instruction = instructionPairs[i].instruction;

            if (instruction.StartsWith("mul("))
            {
                if (!isEnabled) { continue; }
                instructionSum += ParseMuliplicationInstruction(instruction);
            }
            else if (instruction.Equals("don't()")) { isEnabled = false; }
            else if (instruction.Equals("do()")) { isEnabled = true; }
            else
            {
                throw new InvalidOperationException("cannot parse the instruction: "
                    + instruction);
            }
        }

        return instructionSum.ToString();
    }

    private int ParseMuliplicationInstruction(string input)
    {
        input = input[4..];     // remove 'mul('.
        input = input[..^1];    // remove ')'.

        string[] digits = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return int.Parse(digits[0]) * int.Parse(digits[1]);
    }
}