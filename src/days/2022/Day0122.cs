namespace advent_of_code.days;

public class Day0122 : IDay
{
    public DateTime date { get; } = new(2022, 12, 01);

    private List<int> chunkSums = [];
    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        chunkSums = [];
        foreach (string chunk in data)
        {
            int sum = 0;
            string[] lines = chunk.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                sum += int.Parse(line);
            }

            chunkSums.Add(sum);
        }

        chunkSums.Sort((i1, i2) => i1.CompareTo(i2));
        return chunkSums.Last().ToString();
    }

    public string SolveStarTwo()
    {
        int sumOfThree = 0;

        for (int i = 1; i <= 3; ++i) { sumOfThree += chunkSums[^i]; }

        return sumOfThree.ToString();
    }
}