using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0520 : IDay
{
    public DateTime date {get;} = new(2020, 12, 05);
    
	private readonly List<BoardingPass> boardingPasses = [];
    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int FindRow(string row)
        {
            int min = 0;
            int max = 127;

            foreach (char character in row)
            {
                int difference = max - min;

                if (character == 'F')
                {
                    max = min + (int)Math.Floor(difference * 0.5f);
                    continue;
                }

                if (character == 'B')
                {
                    min += (int)Math.Ceiling(difference * 0.5f);
                    continue;
                }

                Logger.Error($"{character} is not a valid in put!");
            }

            if (min != max) { Logger.Error($"{min} != {max}"); }

            return max;
        }

        int FindColumn(string column)
        {
            int min = 0;
            int max = 7;

            foreach (char character in column)
            {
                int difference = max - min;

                if (character == 'L')
                {
                    //Floor
                    max = min + (int)Math.Floor(difference * 0.5f);
                    continue;
                }

                if (character == 'R')
                {
                    //Ceiling
                    min += (int)Math.Ceiling(difference * 0.5f);
                    continue;
                }

                Logger.Error($"{character} is not a valid in put!");
            }

            if (min != max) { Logger.Error($"{min} != {max}"); }

            return max;
        }

        foreach (string line in data)
        {
            int row = FindRow(line.Substring(0, 7));
            int column = FindColumn(line.Substring(7, 3));

            boardingPasses.Add(new BoardingPass(row, column));
        }

        int highestId = 0;

        foreach (BoardingPass boardingPass in boardingPasses)
        {
            if (boardingPass.Id <= highestId) { continue; }

            highestId = boardingPass.Id;
        }

        return highestId.ToString();
    }

    public string SolveStarTwo()
    {
        List<int> boardingPassIds = boardingPasses.ConvertAll(boardingPass => boardingPass.Id);
        int seatId = 0;

        for (int i = 0; i < boardingPassIds.Count; i++)
        {
            if (boardingPassIds.Contains(boardingPasses[i].Id + 1) || !boardingPassIds.Contains(boardingPasses[i].Id))
            {
                continue;
            }

            seatId = boardingPasses[i].Id + 1;
        }

        return seatId.ToString();
    }

    private struct BoardingPass
    {
        public int Column { get; private set; }
        public int Id { get; private set; }
        public int Row { get; private set; }

        public BoardingPass(int row, int column)
        {
            Row = row;
            Column = column;

            Id = (row * 8) + column;
        }
    }
}
