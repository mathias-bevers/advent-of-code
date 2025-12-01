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
            throw new NullReferenceException("make sure the initialize method is called first");
        }

        string getUrl = $"/{day.date.Year}/day/{day.date.Day}/input";
        HttpResponseMessage response = await client.GetAsync(getUrl);

        try
        {
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();

        }
        catch (HttpRequestException e)
        {
            Logger.Error(e.Message);
            return string.Empty;
        }
    }

    internal static string ReadExample(this IDay day)
    {
        string fileName = $"day-{day.date.Day:D2}.example";
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "examples",
            day.date.Year.ToString(), fileName);

        if (!File.Exists(filePath))
        {
            Logger.Error($"the file \'{fileName}\' does not exist, creating...");

            try { File.Create(filePath); }
            catch (DirectoryNotFoundException exception) { Logger.Error(exception.Message); }

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
            Logger.Warning($"the file \'{filePath}\' does not exist, creating...");
            File.Create(filePath);
            return;
        }

        string[] fileContents = File.ReadAllLines(filePath);

        if (fileContents == null || fileContents.Length == 0)
        {
            throw new FileLoadException("the aoc.cookies file is empty!");
        }
      
        sessionID = fileContents[0];
        
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