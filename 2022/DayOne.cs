﻿using AdventOfCode.Tools;

namespace AdventOfCode._2022
{
	public class DayOne : Day
	{
		public override int DayNumber => 1;
		private List<int> chunkSums;
		private string[] data;

		public override double Initialize()
		{
			stopwatch.Start();
			
			data = DataRetriever.AsFile(this).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			chunkSums = new List<int>();
			foreach (string chunk in data)
			{
				int sum = 0;
				string[] lines = chunk.Split(new[] { "\n" }, StringSplitOptions.None);

				foreach (string line in lines)
				{
					if (string.IsNullOrEmpty(line)) { continue; }

					sum += int.Parse(line);
				}

				chunkSums.Add(sum);
			}

			chunkSums.Sort((i1, i2) => i1.CompareTo(i2));
			return chunkSums.Last().ToString();
		}

		public override string StarTwo()
		{
			int sumOfThree = 0;

			for (int i = 1; i <= 3; ++i) { sumOfThree += chunkSums[^i]; }

			return sumOfThree.ToString();
		}
	}
}