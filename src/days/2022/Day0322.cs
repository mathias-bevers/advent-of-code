namespace advent_of_code.days;

public class Day0322 : IDay
{
    public DateTime date { get; } = new(2022, 12, 03);

    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        List<(string, string)> compartments = new();
        foreach (string line in data)
        {
            int perCompartmentCount = line.Length / 2;
            (string, string) currentCompartments;

            currentCompartments.Item1 = line[..perCompartmentCount];
            currentCompartments.Item2 = line[perCompartmentCount..];

            compartments.Add(currentCompartments);
        }


        int sum = 0;
        foreach ((string, string) compartment in compartments)
        {
            int priorityIndex;

            char duplicate = compartment.Item1.First(item => compartment.Item2.Contains(item));

            priorityIndex = char.IsUpper(duplicate) ? (duplicate - 64) + 26 : duplicate - 96;

            sum += priorityIndex;
        }

        return sum.ToString();
    }

    public string SolveStarTwo()
    {
        List<string[]> groups = new();
        for (int i = 0; i < data.Length / 3; ++i)
        {
            string[] group = new string[3];
            for (int j = 0; j < 3; ++j) { group[j] = data[(i * 3) + j]; }

            groups.Add(group);
        }

        int sum = 0;

        foreach (string[] group in groups)
        {
            int priorityIndex;

            char duplicate = group[0].First(item => group[1].Contains(item) && group[2].Contains(item));

            priorityIndex = char.IsUpper(duplicate) ? (duplicate - 64) + 26 : duplicate - 96;

            sum += priorityIndex;
        }

        return sum.ToString();
    }
}
