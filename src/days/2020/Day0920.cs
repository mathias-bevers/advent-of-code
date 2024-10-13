namespace advent_of_code.days;

public class Day0920 : IDay
{
    public DateTime date { get; } = new(2020, 12, 09);
    
	private const int PREAMBLE = 25;
    private long invalidValue = 0;
    private long invalidIndex = 0;

    private long[] data = [];

    public void PopulateData(string raw)
    {
        string[] file = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data = new long[file.Length];

        for (int i = 0; i < file.Length; i++) { data[i] = long.Parse(file[i]); }
    }

    public string SolveStarOne()
    {
        for (int i = PREAMBLE; i < data.Length; i++)
        {
            if (!IsValid(i))
            {
                invalidIndex = i;
                invalidValue = data[i];
                return invalidValue.ToString();
            }
        }

        bool IsValid(int toCheck)
        {
            List<long> combinations = [];

            for (int i = toCheck - PREAMBLE; i < toCheck; i++)
            {
                for (int j = toCheck - PREAMBLE; j < toCheck; j++)
                {
                    if (i == j) { continue; }

                    combinations.Add(data[i] + data[j]);
                }
            }

            foreach (long combination in combinations)
            {
                if (combination == data[toCheck]) { return true; }
            }

            return false;
        }

        return "NO AWNSER FOUND";
    }

    public string SolveStarTwo()
    {
        int firstIndex = 0;
        int lastIndex = 1;

        while (firstIndex < invalidIndex)
        {
            List<long> checking = data.ToList().GetRange(firstIndex, (lastIndex - firstIndex) + 1);
            long sum = checking.Sum();

            if (sum == invalidValue)
            {
                checking.Sort();
                return (checking.First() + checking.Last()).ToString();
            }

            if (lastIndex < invalidIndex - 1) { lastIndex++; }
            else
            {
                firstIndex++;
                lastIndex = firstIndex + 1;
            }
        }

        return "NO AWNSER FOUND";
    }
}