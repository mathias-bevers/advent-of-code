using AdventOfCode.Tools;
using System.Collections.Generic;

namespace AdventOfCode._2022
{
	public class DaySix : Day
	{
		public override int DayNumber => 6;
		private string[] dataStreamBuffers;

		public override double Initialize()
		{
			stopwatch.Start();
			
			dataStreamBuffers = DataRetriever.AsLines(this);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne() => dataStreamBuffers.Aggregate(string.Empty, (current, dataStreamBuffer) => current + FindMarkerIndex(dataStreamBuffer, 4) + "\t");
		
		public override string StarTwo() => dataStreamBuffers.Aggregate(string.Empty, (current, dataStreamBuffer) => current + FindMarkerIndex(dataStreamBuffer, 14) + "\t");

		private static int FindMarkerIndex(string source, int range)
		{
			for (int i = 0; i < source.Length - range; i++)
			{
				string subString = source.Substring(i, range);

				if(subString.Distinct().Count() != range) { continue; }

				return i + range;
			}

			return -1;
		}
	}
}