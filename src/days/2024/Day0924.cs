
namespace advent_of_code.days;

internal class Day0924 : IDay
{
    public DateTime date { get; } = new(2024, 12, 09);

    private const char EMPTY = '.';

    private string denseDiskMap = string.Empty;

    public void PopulateData(string raw)
    {
        denseDiskMap = raw;
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