using System.Diagnostics;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0125 : IDay
{
    public DateTime date { get; } = new(2025, 12, 01);

    private int[] rotations = Array.Empty<int>();
    private const int DAIL_MAX = 100;

    public void PopulateData(string raw)
    {
        // Replace the L and R with int parsable symbols.
        raw = raw.Replace('L', '-');
        raw = raw.Replace('R', '+');

        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        rotations = new int[lines.Length];

        for (int i = 0; i < rotations.Length; ++i) { rotations[i] = int.Parse(lines[i]); }
    }

    public string SolveStarOne()
    {
        int dial = 50;
        int zeroCount = 0;

        for (int i = 0; i < rotations.Length; ++i)
        {
            // only use the left overs of the 100's since they are full rotations.
            dial += (rotations[i] % DAIL_MAX);

            //clamp the values between 0 and 99.
            if (dial < 0)
            {
                dial += DAIL_MAX;
            }
            else if (dial >= DAIL_MAX)
            {
                dial -= DAIL_MAX;
            }

            if (dial == 0)
            {
                ++zeroCount;
            }
        }

        return zeroCount.ToString();
    }

    public string SolveStarTwo()
    {
        int dial = 50;
        int zeroPass = 0;

        for (int i = 0; i < rotations.Length; ++i)
        {
            // set a bool if the dial is at zero to prevent double counting.
            bool atZero = (dial == 0);

            // use only the 'left overs' for the dial calculations
            dial += (rotations[i] % DAIL_MAX);
            // add full rotations to the zero pass
            zeroPass += Math.Abs(rotations[i] / DAIL_MAX);

            if (dial < 0)
            {
                dial += DAIL_MAX;
                zeroPass += atZero ? 0 : 1;
            }
            else if (dial >= DAIL_MAX)
            {
                dial -= DAIL_MAX;
                zeroPass += atZero ? 0 : 1;
            }
            else if (dial == 0)
            {
                ++zeroPass;
            }
        }

        return zeroPass.ToString();
    }
}