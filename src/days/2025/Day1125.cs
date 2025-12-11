using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1125 : IDay
{
    public DateTime date { get; } = new(2025, 12, 11);
    Dictionary<string, string[]> devices = [];
    // Dictionary<int, List<int>> devices = []; //TODO: test if faster.

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < lines.Length; i++)
        {
            // get the first three chars as the key, then start splitting from 5 to get the routes.
            string device = lines[i];
            string key = device[..3];
            string[] routes = device[5..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if(devices.ContainsKey(key))
            {
                Logger.Warning("overwriting value of key: " + key);
            }

            devices[key] = routes;
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