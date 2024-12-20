
namespace advent_of_code.days;

internal class Day0124 : IDay
{
    public DateTime date { get; } = new DateTime(2024, 12, 01);

    private readonly int[][] data = new int[2][];

    public void PopulateData(string raw)
    {
        string[] pairLines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data[0] = new int[pairLines.Length];
        data[1] = new int[pairLines.Length];

        for (int i = 0; i < pairLines.Length; ++i)
        {
            string[] numbers = pairLines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            data[0][i] = int.Parse(numbers[0]);
            data[1][i] = int.Parse(numbers[1]);
        }

        Array.Sort(data[0]);
        Array.Sort(data[1]);
    }

    public string SolveStarOne()
    {
        int difference = 0;

        for (int i = 0; i < data[0].Length; ++i)
        {
            difference += Math.Abs(data[0][i] - data[1][i]);
        }

        return difference.ToString();
    }

    public string SolveStarTwo()
    {
        int simmularityScore = 0;

        for (int i = 0; i < data[0].Length; ++i)
        {
            int occurance = 0;
            for (int ii = 0; ii < data[1].Length; ++ii)
            {
                if (data[0][i] != data[1][ii]) { continue; }

                ++occurance;
            }

            simmularityScore += data[0][i] * occurance;
        }

        return simmularityScore.ToString();
    }
}