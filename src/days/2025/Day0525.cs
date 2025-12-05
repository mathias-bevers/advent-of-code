using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using advent_of_code.utils;
using CommandLine;
using Range = (long min, long max);

namespace advent_of_code.days;

internal class Day0525 : IDay
{
    public DateTime date { get; } = new(2025, 12, 05);

    private Range[] ranges = [];
    private long[] ids = [];

    public void PopulateData(string raw)
    {
        string[] blocks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        // parse the ranges.
        string[] rangeStrings = blocks[0].Split(Utils.NEW_LINES,
                                                StringSplitOptions.RemoveEmptyEntries);
        List<Range> tmp = new(rangeStrings.Length);
        for (int i = 0; i < rangeStrings.Length; ++i)
        {
            string[] range = rangeStrings[i].Split('-');
            long min = long.Parse(range[0]);
            long max = long.Parse(range[1]);

            tmp.Add((min, max));
        }

        // remove encapsuled rCountanges.
        for (int i = tmp.Count - 1; i >= 0; --i)
        {
            for (int ii = 0; ii < tmp.Count; ii++)
            {
                if (ii == i) { continue; }

                if (tmp[i].min < tmp[ii].min) { continue; }

                if (tmp[i].max > tmp[ii].max) { continue; }

                tmp.RemoveAt(i);
            }
        }

        ranges = [.. tmp];

        // parse the ids.
        string[] idStrings = blocks[1].Split(Utils.NEW_LINES,
                                             StringSplitOptions.RemoveEmptyEntries);
        ids = new long[idStrings.Length];
        for (int i = 0; i < ids.Length; ++i)
        {
            ids[i] = long.Parse(idStrings[i]);
        }

        Array.Sort(ids);
    }

    public string SolveStarOne()
    {
        long freshIngredients = 0;

        for (int i = 0; i < ids.Length; ++i)
        {
            //returns -1 if not in range
            if (InRanges(ids[i]) < 0)
            {
                continue;
            }

            ++freshIngredients;
        }

        return freshIngredients.ToString();
    }

    public string SolveStarTwo()
    {
        bool hasUpdated;
        int n = 0;

        do
        {
            ++n;
            hasUpdated = false;

            for (int i = 0; i < ranges.Length; i++)
            {
                // returns -1 if not in range
                int index = InRanges(ranges[i].min, i);

                if (index < 0) { continue; }

                ranges[i].min = ranges[index].max + 1;
                hasUpdated = true;
            }

        } while (hasUpdated && n < 1000);

        long freshIDs = 0;

        for (int i = 0; i < ranges.Length; i++)
        {
            long difference = ranges[i].max - ranges[i].min + 1;
            freshIDs += difference;
        }

        return freshIDs.ToString();
    }

    private int InRanges(long value, int rangeIndex = -1)
    {
        for (int i = 0; i < ranges.Length; ++i)
        {
            // skip if we are looking for a range.
            if (rangeIndex == i)
            {
                continue;
            }

            if (value >= ranges[i].min && value <= ranges[i].max)
            {
                return i;
            }
        }

        return -1;
    }
}