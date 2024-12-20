using advent_of_code.utils;
using System.Diagnostics;
using System.Reflection;
using CommandLine;

namespace advent_of_code;

public class AdventOfCode
{
    private readonly IDay[] days = [];
    private readonly Options options;

    public AdventOfCode(string[] args)
    {
        options = Parser.Default.ParseArguments<Options>(args).Value;

        try
        {
            if (options.today) { days = [GetDay(DateTime.Today)]; }
            else if (options.day > 0) { days = [GetDay(new DateTime(options.year, 12, options.day))]; }
            else { days = [.. GetDayRange(options.all, options.year)]; }
        }
        catch (Exception exception) { Logger.Error(exception.Message); }

        try { DataFetcher.Initialize(); }
        catch (Exception exception) { Logger.Error(exception.Message); }
    }

    public void Run()
    {
        if (days.Length < 1) { return; }

        if (!DataFetcher.IsInitialized) { return; }


        Stopwatch sw = new();
        int windowWidth = Console.WindowWidth - 2;

        Console.Write("advent of code ");
        Console.Write(new string('*', Math.Max(0, windowWidth - 28)));
        Console.WriteLine(" m.bevers");

        for (int i = 0; i < days.Length; ++i)
        {
            IDay day = days[i];
            string dayString = day.FormatDayToString();
            DayCompletionRecord dcr = new();

            try
            {
                string data = options.example ? day.ReadExample()
                    : Task.Run(day.ReadDataAsync).Result;

                if(string.IsNullOrEmpty(data))
                {
                    Logger.Error($"the loaded data for {dayString} is null.");
                    return;
                }

                sw.Restart();
                day.PopulateData(data);
                sw.Stop();
                dcr.initializationTime = sw.ElapsedMilliseconds;
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'PopulateData\' method of day {dayString} is not implemented!");
            }

            try
            {
                sw.Restart();
                string result = day.SolveStarOne();
                sw.Stop();

                dcr.starOne = new StarCompletionRecord(result, sw.ElapsedMilliseconds);
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'SolveStarOne\' method of day {dayString} is not implemented!");
            }

            try
            {
                sw.Restart();
                string result = day.SolveStarTwo();
                sw.Stop();

                dcr.starTwo = new StarCompletionRecord(result, sw.ElapsedMilliseconds);
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'SolveStarTwo\' method of day {dayString} is not implemented!");
            }

            Logger.Day(dayString, dcr);
        }
    }

    private static IDay GetDay(DateTime date)
    {
        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (!type.GetInterfaces().Contains(typeof(IDay))) { continue; }

            if (Activator.CreateInstance(type) is not IDay day)
            {
                throw new InvalidCastException($"Something went wrong with activating the {type.Name} class as a day");
            }

            if (day.date == date) { return day; }
        }

        throw new NullReferenceException($"Could not find a day for date: {date.FormatDateToString()}");
    }

    private static IDay[] GetDayRange(bool all, int year)
    {
        List<IDay> temp = [];

        foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (!type.GetInterfaces().Contains(typeof(IDay))) { continue; }

            if (Activator.CreateInstance(type) is not IDay day)
            {
                Logger.Error($"Something went wrong with activating the {type.Name} class as a day");
                throw new InvalidCastException();
            }

            if (!all && year != day.date.Year) { continue; }

            temp.Add(day);
        }

        if (temp.Count < 1) { throw new NullReferenceException($"Could not find any days for year: {year}"); }


        temp.Sort((a, b) => a.date.CompareTo(b.date));
        return [.. temp];
    }


    public class Options
    {
        [Option('a', "all", HelpText = "Run all days in the project")]
        public bool all { get; set; } = false;

        [Option('t', "today", HelpText = "Run only the today's parts")]
        public bool today { get; set; } = false;

        [Option('e', "example", HelpText = "Run in example mode, which takes the example text")]
        public bool example { get; set; } = false;

        [Option('d', "day", HelpText = "Run a specific day, if year is not set from this year.")]
        public int day { get; set; } = 0;

        [Option('y', "year", HelpText = "Specify a specific year to run, when not specified only this year is ran")]
        public int year { get; set; } = DateTime.Now.Year;

        public override string ToString()
        {
            return string.Concat("|all: ", all, "|today: ", today, "|example: ", example,
             "|day: ", day, "|year: ", year, '|');
        }
    }
}