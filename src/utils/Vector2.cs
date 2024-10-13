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
        get => (float)Math.Atan2(y, x);
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

    public static Vector2Int operator -(Vector2Int a, Vector2Int b)
    {
        return new Vector2Int(a.x - b.x, a.y - b.y);
    }
}