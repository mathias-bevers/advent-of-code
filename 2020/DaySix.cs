using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
	public class DaySix : Day
	{
		public override int DayNumber => 6;
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
			int sumOfYes = 0;

			foreach (string group in data)
			{
				string trimmedGroup = string.Concat(group.Where(c => !char.IsWhiteSpace(c)));

				sumOfYes += trimmedGroup.Distinct().Count();
			}

			return sumOfYes.ToString();
		}

		public override string StarTwo()
		{
			int sumOfSimmilarAwnsers = 0;

			foreach (string group in data)
			{
				string[] persons = group.Split(new[] { '\n', '\r', ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
				Dictionary<char, int> awnsers = new();

				foreach (string person in persons)
				{
					int simmilarAwnsers = 0;

					foreach (char awnser in person)
					{
						if (awnsers.ContainsKey(awnser))
						{
							awnsers[awnser]++;
							continue;
						}

						awnsers.Add(awnser, 1);
					}

					foreach (KeyValuePair<char, int> keyValuePair in awnsers)
					{
						if (keyValuePair.Value != persons.Count()) { continue; }

						simmilarAwnsers++;
					}

					sumOfSimmilarAwnsers += simmilarAwnsers;
				}
			}

			return sumOfSimmilarAwnsers.ToString();
		}
	}
}