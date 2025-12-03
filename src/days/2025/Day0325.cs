using System.Diagnostics;

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
            (byte value, int index) first =
                FindHighestInBatteryBank(i, 0, batteryBanks[i].Length - 1);
            (byte value, int index) second =
                FindHighestInBatteryBank(i, first.index + 1, batteryBanks[i].Length);

            int batteryOutput = (first.value * 10) + second.value;
            totalOutput += batteryOutput;
        }

        return totalOutput.ToString();
    }

    public string SolveStarTwo()
    {
        long totalOutput = 0;
        const int ENABLED_BATTERIES = 12;

        for (int i = 0; i < batteryBanks.Length; i++)
        {
            byte[] batteries = batteryBanks[i];
            byte[] enabledBatteries = new byte[ENABLED_BATTERIES];
            (byte value, int index) previousHighest = (0, -1);

            for (int ii = 0; ii < ENABLED_BATTERIES; ii++)
            {
                int end = batteries.Length - (ENABLED_BATTERIES - (ii + 1));
                previousHighest =
                    FindHighestInBatteryBank(i, previousHighest.index + 1, end);
                enabledBatteries[ii] = previousHighest.value;
            }

            long bankTotal = 0;
            Array.Reverse(enabledBatteries);
            for (int ii = 0; ii < enabledBatteries.Length; ii++)
            {
                bankTotal += enabledBatteries[ii] * (long)Math.Pow(10, ii);
            }

            totalOutput += bankTotal;
        }
        
        return totalOutput.ToString();
    }

    private (byte value, int index) FindHighestInBatteryBank(int bankIndex, int start, int end)
    {
        byte[] batteries = batteryBanks[bankIndex];
        (byte value, int index) highest = (0, -1);
        end = Math.Min(end, batteries.Length);

        for (int i = start; i < end; i++)
        {
            if (batteries[i] <= highest.value)
            {
                continue;
            }

            highest = (batteries[i], i);
        }

        if (highest.value <= 0 || highest.index < 0)
        {
            throw new Exception($"could not find highest for: {bankIndex}");
        }

        return highest;
    }
}