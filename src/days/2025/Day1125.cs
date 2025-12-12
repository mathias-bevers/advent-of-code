using advent_of_code.utils;
using PreCalcData = (string key, bool fft, bool dac);

namespace advent_of_code.days;

internal class Day1125 : IDay
{
    public DateTime date { get; } = new(2025, 12, 11);

    private const string YOU = "you";
    private const string OUT = "out";
    private const string SVR = "svr";
    private const string DAC = "dac";
    private const string FFT = "fft";


    private Dictionary<string, string[]> devices = [];
    private Dictionary<string, string[]> example = [];

    bool isExample = false;
    // Dictionary<int, List<int>> devices = []; //TODO: test if faster.

    public void PopulateData(string raw)
    {
        string[] chunks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);
        isExample = chunks.Length > 1;

        string[] lines = chunks[0].Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

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

        if (!isExample)
        {
            return;
        }

        lines = chunks[1].Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++)
        {
            // get the first three chars as the key, then start splitting from 5 to get the routes.
            string device = lines[i];
            string key = device[..3];
            string[] routes = device[5..].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (example.ContainsKey(key))
            {
                Logger.Warning("overwriting value of key: " + key);
            }

            example[key] = routes;
        }
    }

    public string SolveStarOne()
    {
        Dictionary<string, long> preCalc = [];

        try
        {
            long numberOfPaths = FindPathsStarOne(YOU, ref preCalc, 0);
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
        Dictionary<PreCalcData, long> preCalc = [];

        try
        {
            long numberOfPaths = FindPathsStarTwo(SVR, ref preCalc, [], 0);
            return numberOfPaths.ToString();
        }
        catch (FailSaveException e)
        {
            Logger.Info(e.Message);
            return "error";
        }
    }

    private long FindPathsStarOne(string key, ref Dictionary<string, long> preCalc, int n)
    {
        if (n == 30) // tested max depth in my case this is 22 so allow for wiggle room.
        {
            throw new FailSaveException($"triggered fail save on: " + key);
        }

        if (string.Equals(key, OUT))
        {
            return 1;
        }

        if (preCalc.TryGetValue(key, out long value))
        {
            return value;
        }

        value = 0;
        string[] routes = devices[key];
        for (int i = 0; i < routes.Length; ++i)
        {
            value += FindPathsStarOne(routes[i], ref preCalc, ++n);
        }

        preCalc[key] = value;
        return value;
    }

    private long FindPathsStarTwo(string key, ref Dictionary<PreCalcData, long> preCalc,
                              List<string> path, int n)
    {
        if (n == 60) // tested max depth in my case this is 50 so allow for wiggle room.
        {
            throw new FailSaveException($"triggered fail save on: " + key);
        }

        PreCalcData data = GenerateDataForKey(key, ref path);

        if (string.Equals(key, OUT))
        {
            if (data.fft && data.dac)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        if (preCalc.TryGetValue(data, out long value))
        {
            return value;
        }

        value = 0;
        string[] routes = isExample ? example[key] : devices[key];
        List<string> newPath = [.. path, key];
        for (int i = 0; i < routes.Length; ++i)
        {
            value += FindPathsStarTwo(routes[i], ref preCalc, newPath, ++n);
        }

        preCalc[data] = value;
        return value;
    }

    private static PreCalcData GenerateDataForKey(string key, ref List<string> path)
    {
        bool fft = false;
        bool dac = false;

        for (int i = 0; i < path.Count; ++i)
        {
            if (path[i].Equals(FFT))
            {
                fft = true;
            }
            else if (path[i].Equals(DAC))
            {
                dac = true;
            }

            if (fft && dac)
            {
                break;
            }
        }

        return (key, fft, dac);
    }
}