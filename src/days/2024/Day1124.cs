
namespace advent_of_code.days;

internal class Day1124 : IDay
{
    public DateTime date { get; } = new(2024, 12, 11);

    private int[] initialStones = [];

    public void PopulateData(string raw)
    {
        string[] stonesRaw = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        initialStones = new int[stonesRaw.Length];

        for (int i = 0; i < stonesRaw.Length; ++i)
        {
            initialStones[i] = int.Parse(stonesRaw[i]);
        }
    }

    public string SolveStarOne()
    {
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}