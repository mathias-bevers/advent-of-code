using QueueItem = (int index, long result);

namespace advent_of_code.days;

internal class Day0724 : IDay
{
    public DateTime date { get; } = new(2024, 12, 07);

    private Calibration[] calibrations = [];
    private int[] validIndexes = [];

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
        List<int> tmp = new(calibrations.Length);

        for (int i = 0; i < calibrations.Length; ++i)
        {
            Calibration calibration = calibrations[i];
            bool isValid = false;

            for (int ii = 0; ii < calibration.possibleConfigurationsOne; ++ii)
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

            tmp.Add(i);
            validCalibrationSum += calibration.target;
        }

        validIndexes = [.. tmp];
        return validCalibrationSum.ToString();
    }

    public string SolveStarTwo()
    {
        long validCalibrationSum = 0;

        for (int i = 0; i < calibrations.Length; ++i)
        {
            if (InValidArray(i))
            {
                validCalibrationSum += calibrations[i].target;
                continue;
            }

            Calibration calibration = calibrations[i];
            bool isValid = false;

            Queue<QueueItem> queue = new();
            queue.Enqueue((0, calibration.equation[0]));

            do
            {
                QueueItem current = queue.Dequeue();
                for (int ii = 0; ii < 3; ++ii)
                {
                    long result = current.result;
                    int nextIndex = current.index + 1;

                    if (ii == 0) { result += calibration.equation[nextIndex]; }
                    else if (ii == 1) { result *= calibration.equation[nextIndex]; }
                    else if (ii == 2)
                    {
                        string concat = result.ToString() + calibration.equation[nextIndex];
                        result = long.Parse(concat);
                    }

                    if (result == calibration.target)
                    {
                        isValid = true;
                        break;
                    }

                    if (nextIndex >= calibration.equation.Length - 1) { continue; }
                    queue.Enqueue((nextIndex, result));
                }
            }
            while (!isValid && queue.Count > 0);

            if (!isValid) { continue; }
            
            validCalibrationSum += calibration.target;
        }

        return validCalibrationSum.ToString();
    }

    private bool InValidArray(int index)
    {
        for (int i = 0; i < validIndexes.Length; ++i)
        {
            int nextValid = validIndexes[i];

            if (nextValid == index) { return true; }
        }

        return false;
    }

    private readonly struct Calibration(long target, int[] equation)
    {
        public long target { get; } = target;
        public int[] equation { get; } = equation;

        public readonly int possibleConfigurationsOne = (int)Math.Pow(2, equation.Length - 1);
        public readonly int possibleConfigurationsTwo = (int)Math.Pow(3, equation.Length - 1);
    }
}