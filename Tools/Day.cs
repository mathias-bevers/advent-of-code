using System.Data;
using System.Diagnostics;

namespace AdventOfCode.Tools
{
	public abstract class Day : IComparable<Day>
	{
		public abstract int DayNumber { get; }
		private const int MAX_ANSWER_PADDING = 30;

		private string year = string.Empty;
		public string Year
		{
			get
			{
				if (string.IsNullOrEmpty(year))
				{
					Type day = GetType();

					string ns = day.Namespace;
					if (string.IsNullOrEmpty(ns)) { throw new NoNullAllowedException("All days should be in a namespace"); }

					string[] splitNamespace = ns.Split('_');

					if (splitNamespace.Length != 2)
					{
						throw new FormatException("All day classes should be in a namespace with the format \'AdventOfCode._{year}\'");
					}

					year = splitNamespace.Last();
				}

				return year;
			}
		}

		protected Stopwatch stopwatch = new();

		public static void Answer(int dayNumber, int starNumber, string answer, double time)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[ANSWER] ");
			Console.ResetColor();
			string trimmedAnswer = string.Concat(answer.Where(c => !char.IsWhiteSpace(c)));
			Console.Write($"(day:{dayNumber} *{starNumber})\t{trimmedAnswer}" + new string(' ', MAX_ANSWER_PADDING - trimmedAnswer.Length));
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"solved in {time}ms");
			Console.ResetColor();
		}

		//TODO: return double for timer. 
		public abstract double Initialize();

		public abstract string StarOne();
		public abstract string StarTwo();

		public int CompareTo(Day? other)
		{
			if (other == null)
			{
				throw new NoNullAllowedException();
			}

			int thisYear = int.Parse(Year);
			int otherYear = int.Parse(other.Year);

			int compareYear = thisYear.CompareTo(otherYear);

			return compareYear != 0 ? compareYear : DayNumber.CompareTo(other.DayNumber);
		}
	}
}