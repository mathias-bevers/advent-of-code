internal static class Utils
{
    public const string EXAMPLE_SPLIT = "------------------------------------$example_split$"; 
    public readonly static string[] NEW_LINES = ["\r\n", "\r", "\n"];
    public readonly static string[] NEW_CHUNKS = ["\r\n\r\n", "\n\n"];

    public static string FormatDayToString(this IDay day) => 
        day.date.ToString("dd-MMM-yyyy").ToLower();

    public static string FormatDateToString(this DateTime date) =>
         date.ToString("dd-MMM-yyyy").ToLower();
}