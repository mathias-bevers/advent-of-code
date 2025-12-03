using Battery = (byte value, int index);

namespace advent_of_code.days;

internal class Day0325 : IDay
{
    public DateTime date { get; } = new(2025, 12, 03);
    private byte[][] batteryBanks = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        batteryBanks = new byte[lines.Length][];

        // parse data into a array of byte arrays.
        for (int i = 0; i < lines.Length; i++)
        {
            batteryBanks[i] = new byte[lines[i].Length];
            for (int ii = 0; ii < batteryBanks[i].Length; ii++)
            {
                batteryBanks[i][ii] = (byte)char.GetNumericValue(lines[i][ii]);
            }
        }
    }

    public string SolveStarOne()
    {
        int totalOutput = 0;

        for (int i = 0; i < batteryBanks.Length; i++)
        {
            Battery first = FindHighestInBatteryBank(i, 0, batteryBanks[i].Length - 1);
            Battery second = FindHighestInBatteryBank(i, first.index + 1, batteryBanks[i].Length);

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
            Battery battery = (0, -1);
            
            long bankTotal = 0;

            for (int ii = 0; ii < ENABLED_BATTERIES; ii++)
            {
                int end = batteries.Length - (ENABLED_BATTERIES - (ii + 1));
                battery = FindHighestInBatteryBank(i, battery.index + 1, end);
                bankTotal = bankTotal * 10 + battery.value;
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