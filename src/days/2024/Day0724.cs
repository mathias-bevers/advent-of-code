
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0724 : IDay
{
    public DateTime date { get; } = new(2024, 12, 07);

    private Calibration[] calibrations = [];

    public void PopulateData(string raw)
    {
        string[] calibrationsRaw =
            raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        calibrations = new Calibration[calibrationsRaw.Length];

        for (int i = 0; i < calibrations.Length; ++i)
        {
            string[] parts = calibrationsRaw[i].Split(": ");
            long target = long.Parse(parts[0]);

            string[] equationRaw = parts[1].Split(' ',
                StringSplitOptions.RemoveEmptyEntries);

            int[] equation = new int[equationRaw.Length];
            for (int ii = 0; ii < equationRaw.Length; ++ii)
            {
                equation[ii] = int.Parse(equationRaw[ii]);
            }

            calibrations[i] = new Calibration(target, equation);
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

    private readonly struct Calibration(long target, int[] equation)
    {
        public long target { get; } = target;
        public int[] equation { get; } = equation;

        public readonly int possibleConfigurations = (int)Math.Pow(2, equation.Length - 1);
    }
}