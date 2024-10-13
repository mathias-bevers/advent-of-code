namespace advent_of_code.days;

public class Day0620 : IDay
{
    public DateTime date { get; } = new(2020, 12, 06);

    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int sumOfYes = 0;

        foreach (string group in data)
        {
            string trimmedGroup = string.Concat(group.Where(c => !char.IsWhiteSpace(c)));

            sumOfYes += trimmedGroup.Distinct().Count();
        }

        return sumOfYes.ToString();
    }

    public string SolveStarTwo()
    {
        int sumOfSimmilarAwnsers = 0;
        char[] separator = ['\n', '\r', ' ', '\t'];

        foreach (string group in data)
        {
            string[] persons = group.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<char, int> awnsers = [];

            foreach (string person in persons)
            {
                int simmilarAwnsers = 0;

                foreach (char awnser in person)
                {
                    if (awnsers.TryGetValue(awnser, out int value))
                    {
                        awnsers[awnser] = ++value;
                        continue;
                    }

                    awnsers.Add(awnser, 1);
                }

                foreach (KeyValuePair<char, int> keyValuePair in awnsers)
                {
                    if (keyValuePair.Value != persons.Length) { continue; }

                    simmilarAwnsers++;
                }

                sumOfSimmilarAwnsers += simmilarAwnsers;
            }
        }

        return sumOfSimmilarAwnsers.ToString();
    }
}