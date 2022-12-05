using System.Data;
using System.Diagnostics;

namespace AdventOfCode.Tools
{
	public abstract class Day : IComparable<Day>
	{
		public abstract int DayNumber { get; }
		public string Year { get; private set; } = string.Empty;

		protected Stopwatch stopwatch = new();

		public static void Answer(int dayNumber, int starNumber, string answer, double time)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[ANSWER] ");
			Console.ResetColor();
			Console.Write($"(day:{dayNumber} *{starNumber})\t{answer}\t");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"solved in {time}ms");
		}

		//TODO: return double for timer. 
		public virtual double Initialize()
		{
			Type day = GetType();

			string ns = day.Namespace;
			if (string.IsNullOrEmpty(ns)) { throw new NoNullAllowedException("All days should be in a namespace"); }

			string[] splitNamespace = ns.Split('_');

			if (splitNamespace.Length != 2)
			{
				throw new FormatException("All day classes should be in a namespace with the format \'AdventOfCode._{year}\'");
			}

			Year = splitNamespace[1];

			return 0;
		}

		public abstract string StarOne();
		public abstract string StarTwo();

		public int CompareTo(Day? other)
		{
			if (other == null)
			{
				throw new NoNullAllowedException();
			}

			int yearCompare = string.Compare(Year, other.Year, StringComparison.Ordinal);
			return yearCompare != 0 ? yearCompare : DayNumber.CompareTo(other.DayNumber);
		}
	}
}