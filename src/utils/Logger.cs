using System.Text;

namespace advent_of_code.utils;

public static class Logger
{
    private const byte INIT_PADDING = 25;
    private const byte STAR_PADDING = 27;

    private static readonly StringBuilder BUILDER = new();

    private static readonly string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
    private static readonly string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
    private static readonly string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
    private static readonly string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
    private static readonly string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
    private static readonly string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
    private static readonly string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
    private static readonly string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
    private static readonly string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
    private static readonly string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
    private static readonly string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
    private static readonly string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
    private static readonly string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
    private static readonly string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

    private static readonly string LOG_PATH =
        Path.Join(Directory.GetCurrentDirectory(), "aoc.log");



    public static void Info(object? message) =>
        Console.WriteLine($"{NORMAL}[INFO] {NORMAL}{message}");
    public static void Warning(object? message) =>
        Console.WriteLine($"{YELLOW}[WARN] {NORMAL}{message}");
    public static void Error(object? message) =>
        Console.WriteLine($"{RED}[ERR ] {NORMAL}{message}");

    internal static void Day(string day, DayCompletionRecord record)
    {
        BUILDER.Append(MAGENTA);
        BUILDER.Append("[DAY ]");
        BUILDER.Append(BLUE);
        BUILDER.Append($"[{day}] ");

        BUILDER.Append(NORMAL);
        // padding is with the assumption that the init time is a three digit number.
        BUILDER.Append($"initialized in {record.initializationTime}ms".PadRight(INIT_PADDING));

        BUILDER.Append(YELLOW);
        BUILDER.Append("[\u2605 1] ");
        BUILDER.Append(NORMAL);
        // padding is with the assumption that the result is a max 27 chars long.
        var temp = string.IsNullOrEmpty(record.starOne.result) ? "null" : record.starOne.result;
        BUILDER.Append(temp.PadRight(STAR_PADDING));
        BUILDER.Append($"completed in {record.starOne.completionTime.ToString().PadLeft(5)}ms");

        BUILDER.Append(YELLOW);
        BUILDER.Append(" [\u2605 2] ");
        BUILDER.Append(NORMAL);
        // padding is with the assumption that the result is a max 27 chars long.
        temp = string.IsNullOrEmpty(record.starTwo.result) ? "null" : record.starTwo.result;
        BUILDER.Append(temp.PadRight(STAR_PADDING));
        BUILDER.Append($"completed in {record.starTwo.completionTime.ToString().PadLeft(5)}ms");


        Console.WriteLine(BUILDER.ToString());
        BUILDER.Clear();
    }

    public static void WriteToLogFile(string content, bool append = false)
    {
        try
        {
            if (append)
            {
                File.AppendAllText(LOG_PATH, Environment.NewLine + content);
            }
            else
            {
                File.WriteAllText(LOG_PATH, content);
                Info("written log to: " + LOG_PATH);
            }
        }
        catch (Exception e)
        {
            Error("something went wrong while writing log file: \n\t" + e.Message);
        }
    }
}