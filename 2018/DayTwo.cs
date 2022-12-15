using AdventOfCode.Tools;

namespace AdventOfCode._2018
{
	public class DayTwo : Day
	{
		public override int DayNumber => 2;

		private string[] boxIDs;

		public override double Initialize()
		{
			stopwatch.Start();
			
			boxIDs = DataRetriever.AsLines(this);
			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int doubles = 0;
			int triples = 0;

			foreach (string boxID in boxIDs)
			{
				Dictionary<char, int> charOccurrences = new();

				foreach (char letter in boxID)
				{
					if (charOccurrences.ContainsKey(letter))
					{
						++charOccurrences[letter];
						continue;
					}

					charOccurrences.Add(letter, 1);
				}


				bool containsDoubles = false;
				bool containsTriples = false;

				foreach (KeyValuePair<char, int> charOccurrence in charOccurrences)
				{
					switch (charOccurrence.Value)
					{
						case 2:
							containsDoubles = true;
							break;

						case 3:
							containsTriples = true;
							break;

						default: continue;
					}
				}

				if (containsDoubles) { ++doubles; }

				if (containsTriples) { ++triples; }
			}

			return (doubles * triples).ToString();
		}

		public override string StarTwo()
		{
			for (int i = 0; i < boxIDs.Length; ++i)
			{
				string boxID1 = boxIDs[i];

				for (int j = 0; j < boxIDs.Length; ++j)
				{
					if (i == j) { continue; }

					string boxID2 = boxIDs[j];

					List<char> similarCharacters = boxID1.Where((character, k) => character == boxID2[k]).ToList();
					if (similarCharacters.Count >= 25) { return string.Join(null, similarCharacters); }
				}
			}

			return "FAILED";
		}
	}
}