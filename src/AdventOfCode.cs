using System.Diagnostics;
using System.Reflection;
using advent_of_code.utils;
using System.Text;
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
            else { days = GetDayRange(options.all, options.year).ToArray(); }
        }
        catch (Exception exception)
        {
            Logger.Error(exception.Message);
        }

        DataFetcher.SetSessionID();
    }

    public void Run()
    {
        if (days.Length < 1)
        {
            Logger.Error($"No days are initialized with the options:\t {options}");
            return;
        }

        if(!DataFetcher.IsValidSession)
        {
            Logger.Error($"The session id is not set correctly!");
            return;
        }


        Stopwatch sw = new();
        StringBuilder sb = new();
        int windowWidth = Console.WindowWidth - 2;

        Console.Write("advent of code ");
        Console.Write(new string('*', Math.Max(0, windowWidth - 28)));
        Console.WriteLine(" m.bevers");

        for (int i = 0; i < days.Length; ++i)
        {
            sb.Clear();

            IDay day = days[i];
            string dayString = day.FormatDayToString();
            long elapsed;
            string result;


            try
            {
                sw.Restart();
                string data = options.example ? day.ReadExample()
                    : Task.Run(day.ReadDataAsync).Result;
                day.PopulateData(data);
                elapsed = sw.ElapsedMilliseconds;
                sb.Append($"initialized in {elapsed}ms\t\t");
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'PopulateData\' method of day {dayString} is not implemented!");
            }

            try
            {
                sw.Restart();
                result = day.SolveStarOne();
                elapsed = sw.ElapsedMilliseconds;
                sb.Append($"[\u2605 1] {result} in {elapsed}ms\t\t");
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'SolveStarOne\' method of day {dayString} is not implemented!");
            }

            try
            {
                sw.Restart();
                result = day.SolveStarTwo();
                elapsed = sw.ElapsedMilliseconds;
                sb.Append($"[\u2605 2] {result} in {elapsed}ms");
            }
            catch (NotImplementedException)
            {
                Logger.Warning($"the \'SolveStarTwo\' method of day {dayString} is not implemented!");
            }

            Logger.Day(dayString, sb.ToString());
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