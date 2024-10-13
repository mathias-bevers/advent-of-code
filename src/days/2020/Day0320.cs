namespace advent_of_code.days;

public class Day0320 : IDay
{
    public DateTime date { get; } = new(2020, 12, 03);

    private char[,] data = new char[0, 0];

    public void PopulateData(string raw)
    {
        string[] file = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data = new char[file[0].Length, file.Length];

        for (int i = 0; i < file.Length; i++)
        {
            for (int j = 0; j < file[i].Length; j++) { data[j, i] = file[i][j]; }
        }
    }

    public string SolveStarOne() => CheckPath(3, 1).ToString();

    public string SolveStarTwo()
    {
        int totalTreeEncounters = CheckPath(1, 1) * CheckPath(3, 1) * CheckPath(5, 1) * CheckPath(7, 1) * CheckPath(1, 2);
        return totalTreeEncounters.ToString();
    }

    private int CheckPath(int right, int down)
    {
        int treeEncounters = 0;

        for (int y = 0, x = 0; y < data.GetLength(1); y += down, x += right)
        {
            if (x >= data.GetLength(0)) { x -= data.GetLength(0); }

            if (data[x, y] == '#') { treeEncounters++; }
        }

        return treeEncounters;
    }
}