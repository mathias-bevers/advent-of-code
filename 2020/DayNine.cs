using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
	public class DayNine : Day
	{
		public override int DayNumber => 9;
		private int preamble = 25;
		private long invalidValue = 0;
		private long invalidIndex = 0;

		private long[] data;

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();

			string[] file = DataRetriever.AsLines(this);
			data = new long[file.Length];

			for (int i = 0; i < file.Length; i++) { data[i] = long.Parse(file[i]); }

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			for (int i = preamble; i < data.Length; i++)
			{
				if (!isValid(i))
				{
					invalidIndex = i;
					invalidValue = data[i];
					return invalidValue.ToString();
				}
			}

			bool isValid(int toCheck)
			{
				List<long> combinations = new();

				for (int i = toCheck - preamble; i < toCheck; i++)
				{
					for (int j = toCheck - preamble; j < toCheck; j++)
					{
						if (i == j) { continue; }

						combinations.Add(data[i] + data[j]);
					}
				}

				foreach (long combination in combinations)
				{
					if (combination == data[toCheck]) { return true; }
				}

				return false;
			}

			return "NO AWNSER FOUND";
		}

		public override string StarTwo()
		{
			int firstIndex = 0;
			int lastIndex = 1;

			while (firstIndex < invalidIndex)
			{
				List<long> checking = data.ToList().GetRange(firstIndex, (lastIndex - firstIndex) + 1);
				long sum = checking.Sum();

				if (sum == invalidValue)
				{
					checking.Sort();
					return (checking.First() + checking.Last()).ToString();
				}

				if (lastIndex < invalidIndex - 1) { lastIndex++; }
				else
				{
					firstIndex++;
					lastIndex = firstIndex + 1;
				}
			}

			return "NO AWNSER FOUND";
		}

		private bool isValid(int toCheck)
		{
			List<long> combinations = new();

			for (int i = toCheck - preamble; i < toCheck; i++)
			{
				for (int j = i; j < toCheck; j++)
				{
					if (i == j) { continue; }

					combinations.Add(data[i] + data[j]);
				}
			}

			foreach (long combination in combinations)
			{
				if (combination == data[toCheck]) { return true; }
			}

			return false;
		}
	}
}