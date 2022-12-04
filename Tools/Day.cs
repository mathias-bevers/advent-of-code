using System.Data;

namespace AdventOfCode.Tools
{
	public abstract class Day
	{
		public abstract int DayNumber { get; }
		public string Year { get; private set; } = string.Empty;
		
		public static void Answer(int dayNumber, int starNumber, string answer, double time)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[ANSWER] ");
			Console.ResetColor();
			Console.Write($"(day:{dayNumber} *{starNumber})\t{answer}\t");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"solved in {time}ms");
		}

		public virtual void Initialize()
		{
			Type day = GetType();

			string ns = day.Namespace;
			if (string.IsNullOrEmpty(ns))
			{
				throw new NoNullAllowedException("All days should be in a namespace");
			}

			string[] splitNamespace= ns.Split('_');

			if (splitNamespace.Length != 2)
			{
				throw new FormatException("All day classes should be in a namespace with the format \'AdventOfCode._{year}\'");
			}

			Year = splitNamespace[1];
		}

		public abstract string StarOne();
		public abstract string StarTwo();
	}
}