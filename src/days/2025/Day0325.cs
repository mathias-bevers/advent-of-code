namespace advent_of_code.days;

internal class Day0325 : IDay
{
    public DateTime date { get; } = new(2025, 12, 03);
    private byte[][] batteryBanks = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        batteryBanks = new byte[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            batteryBanks[i] = new byte[lines[i].Length];
            for (int ii = 0; ii < batteryBanks[i].Length; ii++)
            {
                batteryBanks[i][ii] = (byte)Char.GetNumericValue(lines[i][ii]);
            }
        }
    }

    public string SolveStarOne()
    {
        int totalOutput = 0;

        for (int i = 0; i < batteryBanks.Length; i++)
        {
            byte[] batteries = batteryBanks[i];
            (byte value, int index) firstHighest = (0, -1);
            for (int ii = 0; ii < batteries.Length - 1; ii++)
            {
                if (batteries[ii] <= firstHighest.value)
                {
                    continue;
                }

                firstHighest = (batteries[ii], ii);
            }

            byte secondHighest = 0;
            for (int ii = firstHighest.index + 1; ii < batteries.Length; ii++)
            {
                if (batteries[ii] <= secondHighest)
                {
                    continue;
                }

                secondHighest = batteries[ii];
            }

            int batteryOutput = (firstHighest.value * 10) + secondHighest;
            totalOutput += batteryOutput;
        }

        return totalOutput.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}