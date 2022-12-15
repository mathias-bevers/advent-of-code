using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
	public class DayTen : Day
	{
		public override int DayNumber => 10;
		private List<int> data;

		public override double Initialize()
		{
			stopwatch.Start();
			
			data = DataRetriever.AsIntArray(this).ToList();
			data.Add(0);
			data.Sort();
			data.Add(data.Last() + 3);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int oneDifference = 0;
			int threeDifference = 0;

			for (int i = 1; i < data.Count; i++)
			{
				if (data[i] - data[i - 1] == 1)
				{
					oneDifference++;
					continue;
				}

				if (data[i] - data[i - 1] == 3) { threeDifference++; }
			}

			return (oneDifference * threeDifference).ToString();
		}

		public override string StarTwo()
		{
			Dictionary<int, long> checkedAdapters = new();

			for (int i = data.Count - 1; i >= 0; i--)
			{
				int adapter = data[i];
				if (checkedAdapters.ContainsKey(adapter)) { continue; }

				long amount = 0;
				for (int j = 1; j < 4; j++)
				{
					if (i + j >= data.Count)
					{
						amount = 1;
						break;
					}

					int checkingAdapter = data[i + j];
					if (checkingAdapter - adapter < 1 || checkingAdapter - adapter > 3) { continue; }

					amount += checkedAdapters[checkingAdapter];
				}

				checkedAdapters.Add(adapter, amount);
			}

			return checkedAdapters[data.Min()].ToString();
		}

		//17006112
		//113387824750592
	}
}