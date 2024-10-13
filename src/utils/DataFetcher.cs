using System.Net;

namespace advent_of_code.utils;

internal static class DataFetcher
{
    public static bool IsInitialized { get; private set; } = false;

    private static HttpClientHandler? handler;
    private static HttpClient? client;

    private static string sessionID = string.Empty;


    internal static async Task<string> ReadDataAsync(this IDay day)
    {
        if (client is null || handler is null)
        {
            throw new NullReferenceException("Make sure the initialize method is called first");
        }

        string getUrl = $"/{day.date.Year}/day/{day.date.Day}/input";
        HttpResponseMessage response = await client.GetAsync(getUrl);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    internal static string ReadExample(this IDay day)
    {
        string fileName = day.FormatDayToString() + ".txt";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "example", fileName);

        if (!File.Exists(filePath))
        {
            Logger.Error($"the file \'{fileName}\' does not exist, creating...");
            File.Create(filePath);
            return string.Empty;
        }

        return File.ReadAllText(filePath);
    }

    internal static void Initialize()
    {
        // Read the session ID
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "aoc.cookies");

        if (!File.Exists(filePath))
        {
            Logger.Error($"the file \'{filePath}\' does not exist, creating...");
            File.Create(filePath);
            return;
        }

        sessionID = File.ReadAllLines(filePath)[0];


        // Setup the http client.
        handler = new HttpClientHandler() { CookieContainer = new CookieContainer() };
        handler.CookieContainer.Add(new("https://www.adventofcode.com"),
            new Cookie("session", sessionID, "/", "adventofcode.com"));

        client = new(handler)
        {
            BaseAddress = new("https://www.adventofcode.com")
        };

        IsInitialized = true;
    }
}