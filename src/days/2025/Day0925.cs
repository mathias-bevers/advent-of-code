using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0925 : IDay
{
    public DateTime date { get; } = new(2025, 12, 09);

    private Rectangle[] rectangles = [];
    private Vector2Int[] positions = [];
    private Line[] lines = [];

    public void PopulateData(string raw)
    {
        string[] input = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        positions = new Vector2Int[input.Length];
        lines = new Line[input.Length];

        // parse input to vector 3 ints.
        for (int i = 0; i < positions.Length; i++)
        {
            string[] position = input[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            positions[i] = new Vector2Int(int.Parse(position[0]), int.Parse(position[1]));

            if (i == 0) { continue; }

            // add a new line from previous to current position.
            lines[i - 1] = ContructLine(positions[i - 1], positions[i]);
        }

        // lastly add the line from the last position to the first to complete the circle.
        lines[^1] = ContructLine(positions[^1], positions[0]);

        // compute all posible rectangles.
        HashSet<Rectangle> tmp = new();
        for (int i = 0; i < positions.Length; i++)
        {
            for (int ii = i + 1; ii < positions.Length; ii++)
            {
                Rectangle rectangle = Rectangle.CreateFromPoints(positions[i], positions[ii]);
                tmp.Add(rectangle);
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
        ulong? largestArea = null;
        Rectangle? largest = null;

        for (int i = 0; i < rectangles.Length; i++)
        {
            Rectangle rect = rectangles[i];
            if (rect.area <= largestArea)
            {
                continue;
            }

            if (!IsValid(ref rect))
            {
                continue;
            }

            largest = rect;
            largestArea = rect.area;
        }

        if (largestArea.HasValue && largest.HasValue)
        {
            Logger.Info(string.Concat(largest.Value.position, "->", largest.Value.bottomRight));
            return largestArea.Value.ToString();
        }


        Logger.Error("largest is null, so none has been found");
        return "ERROR";
    }

    private bool IsValid(ref Rectangle rect)
    {
        for (int i = 0; i < lines.Length; i++)
        {
            Line line = lines[i];

            if (line.start.x >= rect.bottomRight.x)
            {
                continue;
            }

            if (line.end.x <= rect.position.x)
            {
                continue;
            }

            if (line.start.y >= rect.bottomRight.y)
            {
                continue;
            }

            if (line.end.y <= rect.position.y)
            {
                continue;
            }

            return false;
        }

        return true;
    }

    private static Line ContructLine(Vector2Int a, Vector2Int b)
    {
        int xs = Math.Min(a.x, b.x);
        int ys = Math.Min(a.y, b.y);
        int xe = Math.Max(a.x, b.x);
        int ye = Math.Max(a.y, b.y);
        return new Line(xs, ys, xe, ye);
    }
}