namespace advent_of_code.utils;

internal struct Line(Vector2Int start, Vector2Int end)
{
    public Vector2Int start { get; } = start;
    public Vector2Int end { get; } = end;
    public bool IsHorizontal => (this.start.x == this.end.x);
    public bool IsVertical => (this.start.y == this.end.y);


    // code found on: https://csharphelper.com/howtos/howto_segment_intersection.html
    public Vector2? Intersects(Line other)
    {
        if (Equals(other)) { throw new ArgumentException("lines are the same"); }

        double dx12 = end.x - start.x;
        double dy12 = end.y - start.y;
        double dx34 = other.end.x - other.start.x;
        double dy34 = other.end.y - other.start.y;

        double denominator = dy12 * dx34 - dx12 * dy34;
        double t1 = ((start.x - other.start.x) * dy34 + (other.start.y - start.y) * dx34)
                    / denominator;

        if (double.IsInfinity(t1))
        {
            return null;
        }

        return new Vector2((float)(start.x + dx12 * t1), (float)(start.y + dy12 * t1));
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