using AdventOfCode.Tools;

namespace AdventOfCode._2021
{
	public class DayFour : Day
	{
		public override int DayNumber => 4;

		private int[] drawnNumbers;
		private List<BingoCard> bingoCards = new();

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();

			string firstLine = DataRetriever.AsLines(this).First();
			drawnNumbers = firstLine.Split(",").Select(int.Parse).ToArray();

			string[] splitFile = DataRetriever.AsFile(this).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			for (int i = 1; i < splitFile.Length; i++)
			{
				int[,] bingoCard = new int[5, 5];
				string[] rows = splitFile[i].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

				for (int y = 0; y < rows.Length; y++)
				{
					string[] individualNumbers = rows[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);

					for (int x = 0; x < individualNumbers.Length; x++) { bingoCard[x, y] = int.Parse(individualNumbers[x]); }
				}

				bingoCards.Add(new BingoCard(bingoCard));
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			foreach (int drawnNumber in drawnNumbers)
			{
				foreach (BingoCard bingoCard in bingoCards)
				{
					bingoCard.TryCrossOff(drawnNumber);
					if (bingoCard.IsCompleted()) { return bingoCard.CalculateScore(drawnNumber).ToString(); }
				}
			}

			return "FAILED";
		}

		public override string StarTwo()
		{
			List<BingoCard> completedBingoCards = new();

			foreach (int drawnNumber in drawnNumbers)
			{
				foreach (BingoCard bingoCard in bingoCards)
				{
					if (completedBingoCards.Contains(bingoCard)) { continue; }

					bingoCard.TryCrossOff(drawnNumber);
					if (bingoCard.IsCompleted()) { completedBingoCards.Add(bingoCard); }

					if (completedBingoCards.Count < bingoCards.Count) { continue; }

					BingoCard lastToWin = completedBingoCards.Last();
					return lastToWin.CalculateScore(drawnNumber).ToString();
				}
			}

			return "FAILED";
		}

		private readonly struct BingoCard
		{
			private readonly KeyValuePair<int, bool>[,] card = new KeyValuePair<int, bool>[5, 5];

			public BingoCard(int[,] intCard)
			{
				if (intCard.GetLength(0) != 5 ||
				    intCard.GetLength(1) != 5) { Debug.LogError("Each bingo card should be 5x5."); }

				for (int x = 0; x < intCard.GetLength(0); ++x)
				for (int y = 0; y < intCard.GetLength(1); ++y) { card[x, y] = new KeyValuePair<int, bool>(intCard[x, y], false); }
			}

			public bool IsCompleted()
			{
				for (int x = 0; x < card.GetLength(0); ++x)
				{
					if (GetColumn(x).All(kvp => kvp.Value)) { return true; }
				}

				for (int y = 0; y < card.GetLength(1); ++y)
				{
					if (GetRow(y).All(kvp => kvp.Value)) { return true; }
				}

				return false;
			}

			public void TryCrossOff(int drawnNumber)
			{
				for (int x = 0; x < card.GetLength(0); ++x)
				for (int y = 0; y < card.GetLength(1); ++y)
				{
					if (card[x, y].Key == drawnNumber) { card[x, y] = new KeyValuePair<int, bool>(drawnNumber, true); }
				}
			}

			public int CalculateScore(int drawnNumber)
			{
				int notCrossedSum = 0;

				for (int x = 0; x < card.GetLength(0); ++x)
				for (int y = 0; y < card.GetLength(1); ++y)
				{
					if (card[x, y].Value) { continue; }

					notCrossedSum += card[x, y].Key;
				}

				return notCrossedSum * drawnNumber;
			}

			private IEnumerable<KeyValuePair<int, bool>> GetColumn(int columnIndex)
			{
				List<KeyValuePair<int, bool>> column = new();

				for (int y = 0; y < card.GetLength(1); y++) { column.Add(card[columnIndex, y]); }

				return column;
			}

			private IEnumerable<KeyValuePair<int, bool>> GetRow(int rowIndex)
			{
				List<KeyValuePair<int, bool>> row = new();

				for (int x = 0; x < card.GetLength(0); x++) { row.Add(card[x, rowIndex]); }

				return row;
			}
		}
	}
}