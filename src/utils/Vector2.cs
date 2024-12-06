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

}