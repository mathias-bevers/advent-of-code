namespace advent_of_code.days;

public class Day0118 : IDay
{
    public DateTime date { get; } = new(2018, 12, 01);

    private readonly HashSet<int> visitedFrequencies = [];
    private int[] frequencies = [];
    private int endFrequency;

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        frequencies = new int[lines.Length];

        for (int i = 0; i < lines.Length; ++i)
        {
            frequencies[i] = int.Parse(lines[i]);
        }
    }

    public string SolveStarOne()
    {
        int frequency = 0;

        foreach (int f in frequencies)
        {
            frequency += f;
            visitedFrequencies.Add(frequency);
        }

        endFrequency = frequency;
        return endFrequency.ToString();
    }


    public string SolveStarTwo()
    {
        int frequency = endFrequency;

        while (true)
        {
            foreach (int f in frequencies)
            {
                frequency += f;
                if (visitedFrequencies.Contains(frequency)) { return frequency.ToString(); }

                visitedFrequencies.Add(frequency);
            }
        }
    }
}