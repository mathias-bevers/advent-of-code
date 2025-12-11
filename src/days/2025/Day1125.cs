using advent_of_code.utils;
using CommandLine;

namespace advent_of_code.days;

internal class Day1125 : IDay
{
    public DateTime date { get; } = new(2025, 12, 11);

    private const string YOU = "you";
    private const string OUT = "out";


    private Dictionary<string, string[]> devices = [];
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

            if (devices.ContainsKey(key))
            {
                Logger.Warning("overwriting value of key: " + key);
            }

            devices[key] = routes;
        }
    }

    public string SolveStarOne()
    {
        Dictionary<string, long> preCalculated = new();

        try
        {
            long numberOfPaths = FindPaths(YOU, ref preCalculated, 0);
            return numberOfPaths.ToString();
        }
        catch (FailSaveException e)
        {
            Logger.Info(e.Message);
            return "error";
        }
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private long FindPaths(string key, ref Dictionary<string, long> preCalculated, int n)
    {
        if (n == 1_000_000)
        {
            throw new FailSaveException($"triggered fail save on: " + key);
        }

        if (string.Equals(key, OUT))
        {
            return 1;
        }

        if (preCalculated.TryGetValue(key, out long value))
        {
            return value;
        }

        value = 0;
        string[] routes = devices[key];
        for (int i = 0; i < routes.Length; ++i)
        {
            value += FindPaths(routes[i], ref preCalculated, ++n);
        }

        return value;
    }
}