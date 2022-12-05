using AdventOfCode.Tools;

namespace AdventOfCode._2021
{
	public class DayOne : Day
	{
		public override int DayNumber => 1;

		private int[] data = new int[0];

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();
			data = DataRetriever.AsIntArray(this);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int increased = 0;
			for (int i = 0; i < data.Length - 1; i++)
			{
				if (data[i + 1] - data[i] <= 0) { continue; }

				increased++;
			}

			return increased.ToString();
		}

		public override string StarTwo()
		{
			int increased = 0;

			int previousSum = data[0] + data[1] + data[2];
			for (int i = 1; i < data.Length - 2; i++)
			{
				int currentSum = data[i] + data[i + 1] + data[i + 2];

				if (currentSum - previousSum > 0) { increased++; }

				previousSum = currentSum;
			}

			return increased.ToString();
		}
	}
}