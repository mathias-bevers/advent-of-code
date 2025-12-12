using System.Numerics;

internal static class Utils
{
    public const string EXAMPLE_SPLIT = "------------------------------------$example_split$";
    public readonly static string[] NEW_LINES = ["\r\n", "\r", "\n"];
    public readonly static string[] NEW_CHUNKS = ["\r\n\r\n", "\n\n"];

    public static string FormatDayToString(this IDay day) =>
        day.date.ToString("dd-MMM-yyyy").ToLower();

    public static string FormatDateToString(this DateTime date) =>
         date.ToString("dd-MMM-yyyy").ToLower();

    public static long Product<T>(this IEnumerable<T> collection) where T : INumber<T>
    {
        T result = T.One;

        foreach (T number in collection)
        {
            result *= number;
        }

        return long.CreateChecked(result);
    }

    public static void AddRange<T>(this HashSet<T> source, IEnumerable<T> range)
    {
        foreach (T item in range)
        {
            source.Add(item);
        }
    }
}

namespace advent_of_code.utils
{
    public class FailSaveException(string message) : Exception(message) { }
}