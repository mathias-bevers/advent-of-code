namespace advent_of_code.days;

public class DayTen : IDay
{
    public DateTime date { get; } = new(2020, 12, 10);
    private List<int> data = [];

    public void PopulateData(string raw)
    {
		string[] asLines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
		data = new List<int>(asLines.Length);

		for(int i = 0; i < asLines.Length; ++i)
		{
			data.Add(int.Parse(asLines[i]));
		}

        data.Add(0);
        data.Sort();
        data.Add(data.Last() + 3);
    }

    public string SolveStarOne()
    {
        int oneDifference = 0;
        int threeDifference = 0;

        for (int i = 1; i < data.Count; i++)
        {
            if (data[i] - data[i - 1] == 1)
            {
                oneDifference++;
                continue;
            }

            if (data[i] - data[i - 1] == 3) { threeDifference++; }
        }

        return (oneDifference * threeDifference).ToString();
    }

    public string SolveStarTwo()
    {
        Dictionary<int, long> checkedAdapters = [];

        for (int i = data.Count - 1; i >= 0; i--)
        {
            int adapter = data[i];
            if (checkedAdapters.ContainsKey(adapter)) { continue; }

            long amount = 0;
            for (int j = 1; j < 4; j++)
            {
                if (i + j >= data.Count)
                {
                    amount = 1;
                    break;
                }

                int checkingAdapter = data[i + j];
                if (checkingAdapter - adapter < 1 || checkingAdapter - adapter > 3) { continue; }

                amount += checkedAdapters[checkingAdapter];
            }

            checkedAdapters.Add(adapter, amount);
        }

        return checkedAdapters[data.Min()].ToString();
    }
}