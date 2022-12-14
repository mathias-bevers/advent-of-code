using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
	public class DayOne : Day
	{
		public override int DayNumber => 1;
		private int[] data;

		public override double Initialize()
		{
			stopwatch.Start();
			
			data = DataRetriever.AsIntArray(this);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			for (int i = 0; i < data.Length; i++)
			{
				for (int j = 0; j < data.Length; j++)
				{
					if (j == i) { continue; }

					int sum = data[i] + data[j];
					if (sum != 2020) { continue; }

					return (data[i] * data[j]).ToString();
				}
			}

			return "NO AWNSER FOUND";
		}

		public override string StarTwo()
		{
			for (int i = 0; i < data.Length; i++)
			{
				for (int j = 0; j < data.Length; j++)
				{
					if (j == i) { continue; }

					for (int k = 0; k < data.Length; k++)
					{
						if (k == i || k == j) { continue; }

						int sum = data[i] + data[j] + data[k];
						if (sum != 2020) { continue; }

						return (data[i] * data[j] * data[k]).ToString();
					}
				}
			}

			return "NO AWNSER FOUND";
		}
	}
}