namespace advent_of_code.utils;

internal struct Line
{
    public Vector2Int start { get; }
    public Vector2Int end { get; }
    public readonly bool IsHorizontal => start.y == end.y;
    public readonly bool IsVertical => start.x == end.x;

    public Line(Vector2Int start, Vector2Int end)
    {
        this.start = start;
        this.end = end;
    }

    public Line(int xs, int ys, int xe, int ye)
    {
        start = new Vector2Int(xs, ys);
        end = new Vector2Int(xe, ye);
    }


    // code found on: https://github.com/setchi/Unity-LineSegmentsIntersection/blob/master/Assets/LineSegmentIntersection/Scripts/Math2d.cs
    public bool Intersects(Line other, out Vector2 intersection)
    {
        intersection = Vector2.zero;
        int d = (end.x - start.x) * (other.end.y - other.start.y) -
                (end.y - start.y) * (other.end.x - other.start.x);

        if (Math.Abs(d) < 0.00001f)
        {
            return false;
        }

        var u = (float)((other.start.x - start.x) * (other.end.y - other.start.y) -
                        (other.start.y - start.y) * (other.end.x - other.start.x)) / d;

        var v = (float)((other.start.x - start.x) * (end.y - start.y) -
                        (other.start.y - start.y) * (end.x - start.x)) / d;

        if (u < 0.0f || u > 1.0f || v < 0.0f || v > 1.0f)
        {
            return false;
        }

        intersection.x = start.x + u * (end.x - start.x);
        intersection.y = start.y + u * (end.y - start.y);

        return true;
    }

    // code found on: https://stackoverflow.com/a/11908158/12806909
    public bool OnLine(Vector2 point, float delta = 0.00001f)
    {
        double dxc = point.x - start.x;
        double dyc = point.y - start.y;

        double dxl = end.x - start.x;
        double dyl = end.y - start.y;

        double cross = dxc * dyl - dyc * dxl;
        return Math.Abs(cross) < delta;
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Line other)
        {
            return false;
        }

        return other.start.Equals(start) && other.end.Equals(end);
    }

    public override readonly int GetHashCode() => HashCode.Combine(start, end);

    public override readonly string ToString() => string.Concat(start, " -> ", end);
}