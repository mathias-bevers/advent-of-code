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
            lines[i - 1] = new Line(positions[i - 1], positions[i]);
        }

        // lastly add the line from the last position to the first to complete the circle.
        lines[^1] = new Line(positions[^1], positions[0]);

        // compute all posible rectangles.
        HashSet<Rectangle> tmp = new();
        for (int i = 0; i < positions.Length; i++)
        {
            for (int ii = i + 1; ii < positions.Length; ii++)
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
        ulong? largest = null;
        List<Rectangle> validRectangles = new(rectangles.Length);
        bool isValid;

        for (int i = 0; i < rectangles.Length; i++)
        {
            isValid = true;
            Rectangle rectangle = rectangles[i];
            Vector2Int[] corners = rectangle.GetCorners();

            for (int ii = 0; ii < 4; ii++) //corners is always 4.
            {
                if (InDataSet(corners[ii]))
                {
                    continue;
                }

                bool inside = IsInSide(corners[ii]);
                if (inside) { continue; }

                isValid = false;
                break;
            }

            if (!isValid)
            {
                continue;
            }

            validRectangles.Add(rectangle);
        }

        Grid<char> validator = new Grid<char>(10, 10);
        validator.Fill('.');

        for (int i = 0; i < validRectangles.Count; i++)
        {
            Rectangle rectangle = validRectangles[i];
            Logger.Info(rectangle);
            ulong area = rectangle.area;

            for (int y = 0; y < rectangle.size.y; ++y)
            {
                for (int x = 0; x < rectangle.size.x; ++x)
                {
                    int rX = rectangle.position.x + x;
                    validator[rX, rectangle.position.y + y] = 'x';
                }
            }

            if (largest > area)
            {
                continue;
            }

            largest = area;
        }


        if (!largest.HasValue)
        {
            Logger.Error("largest is null, so none has been found");
            return "ERROR";
        }

        if (positions.Length < 25)
        {
            Debug.Assert(largest == 24);
        }
        else
        {
            Debug.Assert(largest < 4636745093, $"largest ({largest}) >= 4636745093");
        }

        return largest.Value.ToString();
    }

    private bool IsInSide(Vector2Int point)
    {
        Line test = new Line(point, new Vector2Int(100_000, point.y));
        int intersectionCount = 0;

        for (int i = 0; i < lines.Length; ++i)
        {
            Line line = lines[i];

            if (line.IsHorizontal)
            {
                continue;
            }

            if (!line.Intersects(test, out Vector2 intersection))
            {
                continue;
            }

            ++intersectionCount;
        }

        bool inSide = intersectionCount % 2 == 1;
        Logger.Info(string.Concat(point, ": ", intersectionCount, " so ", inSide));
        return inSide;
    }

    private bool InDataSet(Vector2Int point)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (point != positions[i])
            {
                continue;
            }

            return true;
        }

        return false;
    }
}