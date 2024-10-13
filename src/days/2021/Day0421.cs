using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0421 : IDay
{
    public DateTime date { get; } = new(2021, 12, 04);

    private int[] drawnNumbers = [];
    private readonly List<BingoCard> bingoCards = [];

    public void PopulateData(string raw)
    {
		string[] asChunks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        string firstLine = asChunks[0];
        drawnNumbers = firstLine.Split(",").Select(int.Parse).ToArray();

        for (int i = 1; i < asChunks.Length; i++)
        {
            int[,] bingoCard = new int[5, 5];
            string[] rows = asChunks[i].Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

            for (int y = 0; y < rows.Length; y++)
            {
                string[] individualNumbers = rows[y].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                for (int x = 0; x < individualNumbers.Length; x++) { bingoCard[x, y] = int.Parse(individualNumbers[x]); }
            }

            bingoCards.Add(new BingoCard(bingoCard));
        }
    }

    public string SolveStarOne()
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

    public string SolveStarTwo()
    {
        List<BingoCard> completedBingoCards = [];

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
                intCard.GetLength(1) != 5) { Logger.Error("Each bingo card should be 5x5."); }

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

        private List<KeyValuePair<int, bool>> GetColumn(int columnIndex)
        {
            List<KeyValuePair<int, bool>> column = [];

            for (int y = 0; y < card.GetLength(1); y++) { column.Add(card[columnIndex, y]); }

            return column;
        }

        private List<KeyValuePair<int, bool>> GetRow(int rowIndex)
        {
            List<KeyValuePair<int, bool>> row = [];

            for (int x = 0; x < card.GetLength(0); x++) { row.Add(card[x, rowIndex]); }

            return row;
        }
    }
}