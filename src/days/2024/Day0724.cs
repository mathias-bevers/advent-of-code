
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
        long validCalibrationSum = 0;

        for (int i = 0; i < calibrations.Length; ++i)
        {
            Calibration calibration = calibrations[i];
            bool isValid = false;

            for (int ii = 0; ii < calibration.possibleConfigurations; ++ii)
            {
                long equationResult = calibration.equation[0];
                for (int iii = 1; iii < calibration.equation.Length; ++iii)
                {
                    int lastBit = (ii >> iii - 1);
                    bool isMultiplication = ((lastBit & 1) == 1);

                    if (isMultiplication)
                    {
                        equationResult *= calibration.equation[iii];
                    }
                    else
                    {
                        equationResult += calibration.equation[iii];
                    }

                    if (equationResult > calibration.target) { break; }
                }

                if (equationResult != calibration.target) { continue; }

                isValid = true;
                break;
            }

            if (!isValid) { continue; }

            validCalibrationSum += calibration.target;
        }

        return validCalibrationSum.ToString();
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