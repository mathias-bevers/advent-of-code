using System.Net;

namespace advent_of_code.utils;

internal static class DataFetcher
{
    private static string sessionID = string.Empty;
    public static bool IsValidSession => !string.IsNullOrEmpty(sessionID);

    internal static async Task<string> ReadDataAsync(this IDay day)
    {
        if (string.IsNullOrEmpty(sessionID))
        {
            throw new ArgumentNullException(nameof(sessionID), "To read the normal data, the 'sessionID' variable needs to be set.");
        }

        var url = $"https://www.adventofcode.com/{day.date.Year}/day/{day.date.Day}/input";

        using HttpClientHandler handler = new() { CookieContainer = new CookieContainer() };

        handler.CookieContainer.Add(new Uri("https://www.adventofcode.com/"), new Cookie("session", sessionID, "/", "adventofcode.com"));

        using HttpClient client = new (handler);
        HttpResponseMessage response = await client.GetAsync(url);
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

    internal static void SetSessionID()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "aoc.cookies");

        if(!File.Exists(filePath))
        {
            Logger.Error($"the file \'{filePath}\' does not exist, creating...");
            File.Create(filePath);
            return;
        }

        sessionID = File.ReadAllLines(filePath)[0];
    }
}