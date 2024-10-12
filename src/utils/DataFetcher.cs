using System.Net;

namespace advent_of_code.utils;

internal static class DataFetcher
{
    private static readonly Uri BASE_ADRESS = new("https://www.adventofcode.com");
    private static string sessionID = string.Empty;
    public static bool IsValidSession => !string.IsNullOrEmpty(sessionID);


    internal static async Task<string> ReadDataAsync(this IDay day)
    {    
        using HttpClientHandler handler = new() { CookieContainer = new CookieContainer() };

        handler.CookieContainer.Add(BASE_ADRESS, new Cookie("session", sessionID, "/", "adventofcode.com"));

        using HttpClient client = new (handler);
        client.BaseAddress = BASE_ADRESS;

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