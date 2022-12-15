using AdventOfCode.Tools;

namespace AdventOfCode._2018
{
	public class DayOne : Day
	{
		private readonly HashSet<int> visitedFrequencies = new();
		public override int DayNumber => 1;
		private int endFrequency;
		private int[] frequencies;

		public override double Initialize()
		{
			stopwatch.Start();

			
			frequencies = DataRetriever.AsIntArray(this);

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int frequency = 0;

			foreach (int f in frequencies)
			{
				frequency += f;
				visitedFrequencies.Add(frequency);
			}

			endFrequency = frequency;
			return endFrequency.ToString();
		}


		public override string StarTwo()
		{
			int frequency = endFrequency;

			while (true)
			{
				foreach (int f in frequencies)
				{
					frequency += f;
					if (visitedFrequencies.Contains(frequency)) { return frequency.ToString(); }

					visitedFrequencies.Add(frequency);
				}
			}
		}
	}
}