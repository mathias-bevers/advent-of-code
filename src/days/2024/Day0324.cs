using System.Text.RegularExpressions;

namespace advent_of_code.days;

internal partial class Day0324 : IDay
{
    public DateTime date { get; } = new(2024, 12, 03);

    private string[] instructions = [];

    public void PopulateData(string raw)
    {
        MatchCollection maches = InstructionRegex().Matches(raw);

        instructions = new string[maches.Count];
        for (int i = 0; i < instructions.Length; ++i)
        {
            Match match = maches[i];
            instructions[i] = match.Value;
        }
    }

    public string SolveStarOne()
    {
        int instructionSum = 0;

        for (int i = 0; i < instructions.Length; ++i)
        {
            string instruction = instructions[i];

            if (!instruction.StartsWith("mul(")) { continue; }

            instructionSum += ParseMuliplicationInstruction(instruction);
        }

        return instructionSum.ToString();
    }

    public string SolveStarTwo()
    {
        int instructionSum = 0;
        bool isEnabled = true;

        for (int i = 0; i < instructions.Length; ++i)
        {
            string instruction = instructions[i];

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

    private static int ParseMuliplicationInstruction(string input)
    {
        input = input[4..];     // remove 'mul('.
        input = input[..^1];    // remove ')'.

        string[] digits = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
        return int.Parse(digits[0]) * int.Parse(digits[1]);
    }

    [GeneratedRegex(@"(mul\([0-9]{1,3},[0-9]{1,3}\))|(don't\(\))|(do\(\))")]
    private static partial Regex InstructionRegex();
}