using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1024 : IDay
{
    public DateTime date { get; } = new(2024, 12, 10);

    private const int MAX_HEIGHT = 9;

    private int[,] grid = new int[0, 0];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new int[rows.Length, rows[0].Length];

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                grid[x, y] = (int)char.GetNumericValue(rows[y][x]);
            }
        }
    }

    public string SolveStarOne()
    {
        int scoreSum = 0;
        Queue<Vector2Int> pointQueue = new();
        HashSet<Vector2Int> endPoints = new(); ;

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                int startingHeight = grid[x, y];

                if (startingHeight > 0) { continue; }

                pointQueue.Clear();
                endPoints.Clear();

                pointQueue.Enqueue(new Vector2Int(x, y));

                while (pointQueue.Count > 0)
                {
                    Vector2Int currentPoint = pointQueue.Dequeue();
                    int currentHeight = grid[currentPoint.x, currentPoint.y];
                    Vector2Int[] neighbors = GetNeighbors(currentPoint);

                    for (int i = 0; i < neighbors.Length; ++i)
                    {
                        Vector2Int neighbor = neighbors[i];
                        int neighborValue = grid[neighbor.x, neighbor.y];

                        if (neighborValue != (currentHeight + 1)) { continue; }

                        if (neighborValue != MAX_HEIGHT)
                        {
                            pointQueue.Enqueue(neighbor);
                            continue;
                        }

                        endPoints.Add(neighbor);
                    }
                }

                scoreSum += endPoints.Count;
            }
        }

        return scoreSum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private Vector2Int[] GetNeighbors(Vector2Int point)
    {
        List<Vector2Int> neighbors = new(4);
        Vector2Int dir = new(-1, 0);

        for (int i = 0; i < 4; ++i)
        {
            dir.RotateDegrees(-90);

            Vector2Int neighborPoint = point + dir;

            if (neighborPoint.x < 0 || neighborPoint.y < 0) { continue; }

            if (neighborPoint.x >= grid.GetLength(0) || neighborPoint.y >= grid.GetLength(1))
            {
                continue;
            }

            neighbors.Add(neighborPoint);
        }

        return neighbors.ToArray();
    }
}