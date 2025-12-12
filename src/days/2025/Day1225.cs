using System.ComponentModel;
using advent_of_code.utils;
using Region = (advent_of_code.utils.Vector2Int size, byte[] presents);

namespace advent_of_code.days;

internal class Day1225 : IDay
{
    public DateTime date { get; } = new(2025, 12, 12);

    private const byte PRESENT_LINES = 4; // each present takes up 4 lines in example and puzzle.
    private const byte TOTAL_PRESENT_LINES = 24; // the present data takes up 24 lines.
    private Grid<char>[] presents = [];
    private Region[] regions = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        presents = new Grid<char>[6]; // checked input both example and puzzle have 6 presents.

        for (int i = 0; i < presents.Length; i++)
        {
            presents[i] = new Grid<char>(3, 3);

            string present = i.ToString();
            for (int ii = 1; ii < PRESENT_LINES; ii++) // skip first since its the index.
            {
                string line = lines[i * PRESENT_LINES + ii];

                for (int iii = 0; iii < line.Length; iii++)
                {
                    presents[i][iii, ii - 1] = line[iii];
                }
            }
        }

        regions = new Region[lines.Length - TOTAL_PRESENT_LINES];
        for (int i = TOTAL_PRESENT_LINES; i < lines.Length; i++)
        {
            string line = lines[i];
            int xIndex = line.IndexOf('x');
            int colIndex = line.IndexOf(':');

            int width = int.Parse(line.Substring(0, xIndex++));
            int height = int.Parse(line.Substring(xIndex, colIndex - xIndex));
            string[] presentsStr = line.Substring(colIndex + 1)
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            byte[] presents = new byte[presentsStr.Length];
            for (int ii = 0; ii < presentsStr.Length; ++ii)
            {
                presents[ii] = byte.Parse(presentsStr[ii]);
            }

            regions[i - TOTAL_PRESENT_LINES] = (new Vector2Int(width, height), presents);
        }
    }

    public string SolveStarOne()
    {
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}