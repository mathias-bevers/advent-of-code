using System.Diagnostics;
using System.Reflection;
using AdventOfCode.Tools;
using Debug = AdventOfCode.Tools.Debug;

public class Program
{
    public static void Main(string[] args)
    {
        string year = "2022";
        if (args.Length > 0) { year = args[0]; }

        List<Day> days = new();

        if (year == "all")
        {
            foreach (Type type in Assembly.GetExecutingAssembly()
                         .GetTypes()
                         .Where(t => t.IsSubclassOf(typeof(Day))))
            {
                Day day = (Day)Activator.CreateInstance(type);
                days.Add(day);
            }
        }
        else
        {
            foreach (Type type in Assembly.GetExecutingAssembly()
                         .GetTypes()
                         .Where(t => t.Namespace == $"AdventOfCode._{year}" && t.IsSubclassOf(typeof(Day))))
            {
                Day day = (Day)Activator.CreateInstance(type);
                days.Add(day);
            }
        }

        days.Sort();

        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("Advent Of Code by Mathias Bevers");
        Console.ResetColor();

        if (days.Count >= 1)
        {
            Stopwatch stopwatch = new();
            foreach (Day day in days)
            {
                double elapsedTime = day.Initialize();
                Console.WriteLine($"\nInitialized day: {day.Year}-{day.DayNumber} in {elapsedTime}ms");

                try
                {
                    stopwatch.Restart();
                    string answer = day.StarOne();
                    Day.Answer(day.DayNumber, 1, answer, stopwatch.ElapsedMilliseconds);
                }
                catch (NotImplementedException) { Debug.LogWaring("Star one is not implemented."); }

                try
                {
                    stopwatch.Restart();
                    string answer = day.StarTwo();
                    Day.Answer(day.DayNumber, 2, answer, stopwatch.ElapsedMilliseconds);
                }
                catch (NotImplementedException) { Debug.LogWaring("Star two is not implemented."); }
            }
        }
		else
		{
			Debug.LogWaring($"There are no days made in the year: {year}");
		}

        Console.Write("Press enter to close...");
        Console.ReadLine();
    }
}