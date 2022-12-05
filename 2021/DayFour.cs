using AdventOfCode.Tools;

namespace AdventOfCode._2021
{
	public class DayFour : Day
	{
		public override int DayNumber => 4;

		private int[] drawnNumbers = new int[0];
		private int[][,] bingoCards;

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();

			string firstLine = DataRetriever.AsLines(this).First();
			List<int> drawnNumbersList = new();
			foreach (string number in firstLine.Split(",")) { drawnNumbersList.Add(int.Parse(number)); }

			drawnNumbers = drawnNumbersList.ToArray();

			string[] splitFile = DataRetriever.AsFile(this).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			List<int[,]> bingoCardsList = new();
			for (int i = 1; i < splitFile.Length; i++) //Whole cards
			{
				int[,] bingoCard = new int[5, 5];
				string[] splitLines = splitFile[i].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

				for (int y = 0; y < splitLines.Length; y++)
				{
					string[] individualNumbers = splitLines[y].Split(new char[0], StringSplitOptions.RemoveEmptyEntries);

					for (int x = 0; x < individualNumbers.Length; x++) { bingoCard[x, y] = int.Parse(individualNumbers[x]); }
				}

				bingoCardsList.Add(bingoCard);
			}

			bingoCards = bingoCardsList.ToArray();

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne() => throw new NotImplementedException();

		public override string StarTwo() => throw new NotImplementedException();
	}
}