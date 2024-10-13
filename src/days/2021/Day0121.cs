namespace advent_of_code.days;

public class Day0121 : IDay
{
    public DateTime date { get; } = new(2021, 12, 01);

    private int[] data = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data = new int[lines.Length];

        for(int i = 0; i < lines.Length; ++i)
        {
            data[i] = int.Parse(lines[i]);
        }
    }

    public string SolveStarOne()
    {
        int increased = 0;
        for (int i = 0; i < data.Length - 1; i++)
        {
            if (data[i + 1] - data[i] <= 0) { continue; }

            increased++;
        }

        return increased.ToString();
    }

    public string SolveStarTwo()
    {
        int increased = 0;

        int previousSum = data[0] + data[1] + data[2];
        for (int i = 1; i < data.Length - 2; i++)
        {
            int currentSum = data[i] + data[i + 1] + data[i + 2];

            if (currentSum - previousSum > 0) { increased++; }

            previousSum = currentSum;
        }

        return increased.ToString();
    }
}