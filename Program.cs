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

		foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == $"AdventOfCode._{year}"))
		{
			Day day = (Day)Activator.CreateInstance(type);
			days.Add(day);
		}

		days.Sort((d1, d2) => d1.DayNumber.CompareTo(d2.DayNumber));

		Stopwatch stopwatch	= new();
		foreach (Day day in days)
		{
			Console.WriteLine($"\nDay: {day.DayNumber}");
			day.Initialize();

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

		Console.Write("Press enter to close...");
		Console.ReadLine();
	}
}