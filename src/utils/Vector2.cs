using System.Diagnostics.CodeAnalysis;

namespace advent_of_code.utils;

public struct Vector2
{
    public float x { get; set; }
    public float y { get; set; }

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2(float value) => x = y = value;

    public Vector2() => x = y = 0;
}

public struct Vector2Int
{
    public static readonly Vector2Int negativeInfinity = new(int.MinValue);
    public static readonly Vector2Int positiveInfinity = new(int.MaxValue);

    public int x { get; set; }
    public int y { get; set; }

    public float magnitude => (float)(MathF.Sqrt(x * x + y * y));

    public float angle
    {
        readonly get => (float)Math.Atan2(y, x);
        set
        {
            float length = magnitude;
            x = (int)Math.Round(Math.Cos(value) * length);
            y = (int)Math.Round(Math.Sin(value) * length);
        }
    }

    public Vector2Int(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Int(int value) => x = y = value;

    public Vector2Int() => x = y = 0;

    public void RotateDegrees(float degrees) => angle += (float)(degrees * Math.PI / 180.0f);


    public static Vector2Int operator +(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x + b.x, a.y + b.y);
    }

    public static Vector2Int operator -(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }

    public static Vector2Int operator *(Vector2Int v, int i) => new(v.x * i, v.y * i);

    public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;

    public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;

    public override readonly int GetHashCode() => HashCode.Combine(x, y);

    public override readonly bool Equals(object? obj)
    {
        if (obj is null) { return false; }

        if (obj is not Vector2Int other) { return false; }

        return x == other.x && y == other.y;
    }

    public override readonly string ToString() => string.Concat('[', x, ", ", y, ']');
}

public struct Vector2Long
{
    public static readonly Vector2Long negativeInfinity = new(long.MinValue);
    public static readonly Vector2Long positiveInfinity = new(long.MaxValue);

    public long x { get; set; }
    public long y { get; set; }

    public float magnitude => (float)(MathF.Sqrt(x * x + y * y));

    public float angle
    {
        readonly get => (float)Math.Atan2(y, x);
        set
        {
            float length = magnitude;
            x = (long)Math.Round(Math.Cos(value) * length);
            y = (long)Math.Round(Math.Sin(value) * length);
        }
    }

    public Vector2Long(long x, long y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2Long(long value) => x = y = value;

    public Vector2Long() => x = y = 0;

    public void RotateDegrees(float degrees) => angle += (float)(degrees * Math.PI / 180.0f);

    public readonly long Determinant(Vector2Long other) => x * other.y - y * other.x;

    public static Vector2Long operator +(Vector2Long a, Vector2Long b)
    {
        return new Vector2Long(a.x + b.x, a.y + b.y);
    }

    public static Vector2Long operator -(Vector2Long a, Vector2Long b)
    {
        return new Vector2Long(a.x - b.x, a.y - b.y);
    }

    public static Vector2Long operator *(Vector2Long v, long i) => new(v.x * i, v.y * i);

    public static bool operator ==(Vector2Long a, Vector2Long b) => a.x == b.x && a.y == b.y;

    public static bool operator !=(Vector2Long a, Vector2Long b) => a.x != b.x || a.y != b.y;

    public override readonly int GetHashCode() => HashCode.Combine(x, y);

    public override readonly bool Equals(object? obj)
    {
        if (obj is null) { return false; }

        if (obj is not Vector2Long other) { return false; }

        return x == other.x && y == other.y;
    }

    public override readonly string ToString() => string.Concat('[', x, ", ", y, ']');
}