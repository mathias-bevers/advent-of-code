using System.Diagnostics.CodeAnalysis;

namespace advent_of_code.utils;

internal struct Rectangle
{
    public Vector2Int position { get; }
    public Vector2Int size { get; }

    public readonly int area => size.x * size.y;

    public Rectangle(Vector2Int position, Vector2Int size)
    {
        this.position = position;
        this.size = size;
    }

    public Rectangle(int x, int y, int width, int height)
    {
        position = new Vector2Int(x, y);
        size = new Vector2Int(width, height);
    }

    public static Rectangle CreateFromPoints(Vector2Int a, Vector2Int b)
    {
        int left = Math.Min(a.x, b.x);
        int right = Math.Max(a.x, b.x);
        int top = Math.Min(a.y, b.y);
        int bottom = Math.Max(a.y, b.y);

        return new Rectangle(left, top, (right - left) + 1, (bottom - top) + 1);
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
        sb.Append("]");

        return sb.ToString();
    }
}