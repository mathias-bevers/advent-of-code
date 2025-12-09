using System.Diagnostics;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0925 : IDay
{
    public DateTime date { get; } = new(2025, 12, 09);

    private Rectangle[] rectangles = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        Vector2Int[] positions = new Vector2Int[lines.Length];

        // parse input to vector 3 ints.
        for (int i = 0; i < positions.Length; i++)
        {
            string[] position = lines[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            positions[i] = new Vector2Int(int.Parse(position[0]), int.Parse(position[1]));
        }

        // compute all posible rectangles.
        HashSet<Rectangle> tmp = new();
        for (int i = 0; i < positions.Length; i++)
        {
            for (int ii = i; ii < positions.Length; ii++)
            {
                tmp.Add(Rectangle.CreateFromPoints(positions[i], positions[ii]));
            }
        }

        rectangles = [.. tmp];
    }

    public string SolveStarOne()
    {
        ulong? largest = null;

        for (int i = 0; i < rectangles.Length; i++)
        {
            if (largest > rectangles[i].area)
            {
                continue;
            }

            largest = rectangles[i].area;
        }

        if (!largest.HasValue)
        {
            throw new NullReferenceException("the largest area has not been found");
        }

        return largest.Value.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}