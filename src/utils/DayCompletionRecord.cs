namespace advent_of_code.utils;

internal struct DayCompletionRecord
{
    public long initializationTime { get; set; }

    public StarCompletionRecord starOne { get; set; }
    public StarCompletionRecord starTwo { get; set; }

    public readonly bool isInitialized => 
        !(string.IsNullOrEmpty(starOne.result) || string.IsNullOrEmpty(starTwo.result));
}

internal readonly struct StarCompletionRecord(string result, long completionTime)
{
    public string result { get; } = result;
    public long completionTime { get; } = completionTime;
}