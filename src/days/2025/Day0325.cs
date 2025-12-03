namespace advent_of_code.days;

internal class Day0325 : IDay
{
    public DateTime date { get; } = new(2025, 12, 03);
    private byte[][] batteries = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        batteries = new byte[lines.Length][];

        for (int i = 0; i < lines.Length; i++)
        {
            batteries[i] = new byte[lines[i].Length];
            for (int ii = 0; ii < batteries[i].Length; ii++)
            {
                batteries[i][ii] = (byte)Char.GetNumericValue(lines[i][ii]);
            }
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