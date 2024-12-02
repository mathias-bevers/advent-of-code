
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
                if (!IsSafeDifference(data[i][ii + 1], data[i][ii], ref isIncline))
                {
                    isSave = false;
                    break;
                }
            }

            if (!isSave) { continue; }
            ++saveLevels;
        }

        return saveLevels.ToString();
    }

    public string SolveStarTwo()
    {
        int saveLevels = 0;
        for (int i = 0; i < data.Length; ++i)
        {
            bool isSave = true;
            bool? isIncline = null;

            for (int ii = 0; ii < data[i].Length - 1; ++ii)
            {
                if (!IsSafeDifference(data[i][ii + 1], data[i][ii], ref isIncline))
                {
                    isSave = false;
                    break;
                }
            }

            if (isSave)
            {
                ++saveLevels;
                continue;
            }


            for (int ii = 1; ii < data[i].Length - 1; ++ii)
            {
                isSave = true;
                for (int iii = 0; iii < data[i].Length - 1; ++iii)
                {
                    int right = ii == iii ? iii - 1 : iii;
                    if (!IsSafeDifference(data[i][iii + 1], data[i][right], ref isIncline))
                    {
                        isSave = false;
                        break;
                    }
                }

                if (isSave) { break; }
            }

            if (!isSave) { continue; }

            ++saveLevels;
        }

        return saveLevels.ToString();
    }

    private bool IsSafeDifference(int left, int right, ref bool? isIncline)
    {
        int difference = right - left;
        int absoluteDifference = Math.Abs(difference);

        if (absoluteDifference < 1 || absoluteDifference > 3)
        {
            return false;
        }

        if (!isIncline.HasValue)
        {
            isIncline = difference > 0;
            return true;
        }

        if (isIncline.Value)
        {
            if (difference < 0)
            {
                return false;
            }
        }
        else
        {
            if (difference > 0)
            {
                return false;
            }
        }

        return true;
    }
}