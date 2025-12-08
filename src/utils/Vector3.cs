using System.ComponentModel;
using System.IO.Compression;

namespace advent_of_code.utils;

internal struct Vector3Int
{
    public static readonly Vector3Int positiveInfinity = new(int.MaxValue);

    public int x { get; set; }
    public int y { get; set; }
    public int z { get; set; }

    public double magnitude => Math.Sqrt(x * x + y * y + z * z);

    public Vector3Int(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public Vector3Int(int v)
    {
        x = v;
        y = v;
        z = v;
    }

    public Vector3Int()
    {
        x = 0;
        y = 0;
        z = 0;
    }

    public static double Distance(Vector3Int a, Vector3Int b)
    {
        double x = a.x - b.x;
        double y = a.y - b.y;
        double z = a.z - b.z;

        return Math.Sqrt(x * x + y * y + z * z);
    }

    public static Vector3Int operator +(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
    }

    public static Vector3Int operator -(Vector3Int a, Vector3Int b)
    {
        return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
    }

    public static Vector3Int operator *(Vector3Int v, int i)
    {
        return new(v.x * i, v.y * i, v.z * i);
    }

    public static bool operator ==(Vector3Int a, Vector3Int b)
    {
        return a.x == b.x && a.y == b.y && a.z == b.z;
    }

    public static bool operator !=(Vector3Int a, Vector3Int b)
    {
        return a.x != b.x || a.y != b.y || a.z != b.z;
    }

    public override readonly int GetHashCode() => HashCode.Combine(x, y, z);

    public override readonly bool Equals(object? obj)
    {
        if (obj is null) { return false; }

        if (obj is not Vector3Int other) { return false; }

        return x == other.x && y == other.y && z == other.z;
    }

    public override readonly string ToString()
    {
        System.Text.StringBuilder sb = new();

        sb.Append('[');
        sb.Append(x.ToString("00000"));
        sb.Append(", ");
        sb.Append(y.ToString("00000"));
        sb.Append(", ");
        sb.Append(z.ToString("00000"));
        sb.Append(']');

        return sb.ToString();
    }
}