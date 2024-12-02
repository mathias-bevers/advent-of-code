namespace advent_of_code.days;

public class Day0321 : IDay
{
    public DateTime date { get; } = new(2021, 12, 03);

    private int[][] data = new int[0][];

    public void PopulateData(string raw)
    {
        string[] asLines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        data = asLines
            .Select(line => line.Select(character => int.Parse(character.ToString())).ToArray())
            .ToArray();
    }

    public string SolveStarOne()
    {
        string gammaRate = string.Empty;
        string epsilon = string.Empty;

        for (int i = 0; i < data[0].Length; i++)
        {
            int one = 0;
            int zero = 0;

            for (int j = 0; j < data.Length; j++)
            {
                if (data[j][i] == 1)
                {
                    one++;
                    continue;
                }

                zero++;
            }

            gammaRate += one > zero ? "1" : "0";
            epsilon += zero > one ? "1" : "0";
        }

        int powerUsage = Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilon, 2);

        return powerUsage.ToString();
    }

    public string SolveStarTwo()
    {
        int oxygen = 0;
        int co2 = 0;

        List<int[]> oxygenCopy = data.ToList();
        int index = 0;
        while (oxygenCopy.Count > 1)
        {
            int one = 0;
            int zero = 0;

            for (int j = 0; j < oxygenCopy.Count; j++)
            {
                if (oxygenCopy[j][index] == 1)
                {
                    one++;
                    continue;
                }

                zero++;
            }

            List<int[]> passedBinaries = new();

            for (int j = 0; j < oxygenCopy.Count; j++)
            {
                if (one >= zero)
                {
                    if (oxygenCopy[j][index] == 0) { continue; }

                    passedBinaries.Add(oxygenCopy[j]);
                    continue;
                }

                if (oxygenCopy[j][index] == 1) { continue; }

                passedBinaries.Add(oxygenCopy[j]);
            }

            oxygenCopy = passedBinaries;
            index++;
        }

        List<int[]> co2Copy = data.ToList();
        index = 0;
        while (co2Copy.Count > 1)
        {
            int one = 0;
            int zero = 0;

            for (int j = 0; j < co2Copy.Count; j++)
            {
                if (co2Copy[j][index] == 1)
                {
                    one++;
                    continue;
                }

                zero++;
            }

            List<int[]> passedBinaries = new();

            for (int j = 0; j < co2Copy.Count; j++)
            {
                if (one < zero)
                {
                    if (co2Copy[j][index] == 0) { continue; }

                    passedBinaries.Add(co2Copy[j]);
                    continue;
                }

                if (co2Copy[j][index] == 1) { continue; }

                passedBinaries.Add(co2Copy[j]);
            }

            co2Copy = passedBinaries;
            index++;
        }

        oxygen = Convert.ToInt32(string.Join("", oxygenCopy[0]), 2);
        co2 = Convert.ToInt32(string.Join("", co2Copy[0]), 2);
        return (oxygen * co2).ToString();
    }
}