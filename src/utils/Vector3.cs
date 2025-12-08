using System.ComponentModel;
using System.IO.Compression;

namespace advent_of_code.utils;

internal struct Vector3Int
{
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

    public Vector3Int()
    {
        x = 0;
        y = 0;
        z = 0;
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

    public override readonly string ToString() => string.Concat('[', x, ", ", y, ", ", z, ']');
}