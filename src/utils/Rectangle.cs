using System.Diagnostics.CodeAnalysis;

namespace advent_of_code.utils;

internal struct Rectangle
{
    public Vector2Int position { get; }
    public Vector2Int size { get; }
    public ulong area { get; }

    public Rectangle(Vector2Int position, Vector2Int size)
    {
        this.position = position;

        if (size.x < 0 || size.y < 0)
        {
            Logger.Error(size + " is invalid, setting to [0,0]");
            size = Vector2Int.zero;
        }

        this.size = size;

        area = (ulong)this.size.x * (ulong)this.size.y;
    }

    public Rectangle(int x, int y, int width, int height)
    {
        position = new Vector2Int(x, y);

        if (width < 0 || size.y < 0)
        {
            Logger.Error($"[{width}, {height}] is invalid, setting to [0,0]");
            width = height = 0;
        }


        size = new Vector2Int(width, height);
        area = (ulong)this.size.x * (ulong)this.size.y;
    }

    public static Rectangle CreateFromPoints(Vector2Int a, Vector2Int b)
    {
        int left = Math.Min(a.x, b.x);
        int width = (Math.Max(a.x, b.x) - left) + 1;
        int top = Math.Min(a.y, b.y);
        int height = (Math.Max(a.y, b.y) - top) + 1;

        if (width < 1 || height < 1)
        {
            throw new InvalidDataException($"width: {width} and hight {height} " +
                                            "should be at least 1.");
        }

        return new Rectangle(left, top, width, height);
    }

    public Vector2Int[] GetCorners()
    {
        Vector2Int[] corners = new Vector2Int[4];
        corners[0] = position;
        corners[2] = position + size - Vector2Int.one;
        corners[1] = new(corners[2].x, position.y);
        corners[3] = new(position.x, corners[2].y);

        return corners;
    }

    public override readonly int GetHashCode()
    {
        return HashCode.Combine(size, position);
    }

    public override readonly bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Rectangle other)
        {
            return false;
        }

        return other.size.Equals(size) && other.position.Equals(position);
    }

    public override readonly string ToString()
    {
        System.Text.StringBuilder sb = new();
        sb.Append("[position: ");
        sb.Append(position);
        sb.Append(" size: ");
        sb.Append(size);
        sb.Append(" area: ");
        sb.Append(area);
        sb.Append("]");

        return sb.ToString();
    }
}