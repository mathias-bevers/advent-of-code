
using System.Xml;

namespace advent_of_code.days;

internal class Day0224 : IDay
{
    public DateTime date { get; } = new(2024, 12, 02);

    private int[][] data = new int[0][];

    public void PopulateData(string raw)
    {
        string[] levels = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        data = new int[levels.Length][];

        for (int i = 0; i < data.Length; ++i)
        {
            string[] levelValues = levels[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            data[i] = new int[levelValues.Length];
            for (int ii = 0; ii < levelValues.Length; ++ii)
            {
                data[i][ii] = int.Parse(levelValues[ii]);
            }
        }
    }

    public string SolveStarOne()
    {
        int saveLevels = 0;
        for (int i = 0; i < data.Length; ++i)
        {
            bool isSave = true;
            bool? isIncline = null;
            for (int ii = 0; ii < data[i].Length - 1; ++ii)
            {
                int difference = data[i][ii + 1] - data[i][ii];
                int absoluteDifference = Math.Abs(difference);

                if (absoluteDifference < 1 || absoluteDifference > 3)
                {
                    isSave = false;
                    break;
                }

                if (!isIncline.HasValue)
                {
                    isIncline = difference > 0;
                    continue;
                }

                if (isIncline.Value)
                {
                    if (difference < 0)
                    {
                        isSave = false;
                        break;
                    }
                }
                else
                {
                    if (difference > 0)
                    {
                        isSave = false;
                        break;
                    }
                }
            }

            if (!isSave) { continue; }
            ++saveLevels;
        }

        return saveLevels.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}