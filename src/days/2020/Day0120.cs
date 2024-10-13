namespace advent_of_code.utils;

public class Day0120 : IDay
{
    public DateTime date { get; } = new (2020, 12, 01);
	
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
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (j == i) { continue; }

                int sum = data[i] + data[j];
                if (sum != 2020) { continue; }

                return (data[i] * data[j]).ToString();
            }
        }

        return "NO AWNSER FOUND";
    }

    public string SolveStarTwo()
    {
        for (int i = 0; i < data.Length; i++)
        {
            for (int j = 0; j < data.Length; j++)
            {
                if (j == i) { continue; }

                for (int k = 0; k < data.Length; k++)
                {
                    if (k == i || k == j) { continue; }

                    int sum = data[i] + data[j] + data[k];
                    if (sum != 2020) { continue; }

                    return (data[i] * data[j] * data[k]).ToString();
                }
            }
        }

        return "NO AWNSER FOUND";
    }
}
