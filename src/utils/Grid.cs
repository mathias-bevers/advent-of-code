namespace advent_of_code.utils;

internal class Grid<T>
{
    private T[,] grid;

    public int width { get; }
    public int height { get; }

    public Grid(int width, int height)
    {
        grid = new T[width, height];
        this.width = width;
        this.height = height;
    }

    public T this[int x, int y]
    {
        get
        {
            if (!InGrid(x, y))
            {
                throw new IndexOutOfRangeException($"[{x},{y}] is not in the bounds of the grid.");
            }

            return grid[x, y];
        }
        set
        {
            if (!InGrid(x, y))
            {
                throw new IndexOutOfRangeException($"[{x},{y}] is not in the bounds of the grid.");
            }

            grid[x, y] = value;
        }
    }

    public bool InGrid(int x, int y)
    {
        if (x < 0 || x >= width)
        {
            return false;
        }

        if (y < 0 || y >= height)
        {
            return false;
        }

        return true;
    }

    public Grid<T> Copy()
    {
        Grid<T> copy = new Grid<T>(width, height);
        copy.grid = (T[,])grid.Clone();

        return copy;
    }

    public void Loop(Action<T, Vector2Int> callback)
    {
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                callback(grid[x, y], new Vector2Int(x, y));
            }
        }
    }

    public IEnumerable<T> As1D()
    {
        return grid.Cast<T>();
    }

    public override string ToString()
    {
        System.Text.StringBuilder sb = new($"Grid[{width},{height}] of {typeof(T).Name}");

        for (int y = 0; y < height; y++)
        {
            sb.AppendLine("\t");
            for (int x = 0; x < width; x++)
            {
                sb.Append(grid[x, y]);
            }
        }

        return sb.ToString();
    }
}