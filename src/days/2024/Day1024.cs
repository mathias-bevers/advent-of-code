using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1024 : IDay
{
    public DateTime date { get; } = new(2024, 12, 10);

    private const int MAX_HEIGHT = 9;

    Grid<int> grid = new(0, 0);


    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new Grid<int>(rows.Length, rows[0].Length);

        for (int y = 0; y < grid.height; ++y)
        {
            for (int x = 0; x < grid.width; ++x)
            {
                grid[x, y] = (int)char.GetNumericValue(rows[y][x]);
            }
        }
    }

    public string SolveStarOne()
    {
        int scoreSum = 0;
        Queue<Vector2Int> pointQueue = new();
        HashSet<Vector2Int> endPoints = [];

        grid.Loop((startingHeight, position) =>
        {
            if (startingHeight > 0)
            {
                return;
            }

            pointQueue.Clear();
            endPoints.Clear();

            pointQueue.Enqueue(position);

            while (pointQueue.Count > 0)
            {
                Vector2Int currentPoint = pointQueue.Dequeue();
                int nextHieght = grid[currentPoint.x, currentPoint.y] + 1;
                Vector2Int[] neighbors = GetNeighbors(currentPoint);

                for (int i = 0; i < neighbors.Length; ++i)
                {
                    Vector2Int neighbor = neighbors[i];
                    int neighborValue = grid[neighbor.x, neighbor.y];

                    if (neighborValue != nextHieght) { continue; }

                    if (neighborValue != MAX_HEIGHT)
                    {
                        pointQueue.Enqueue(neighbor);
                        continue;
                    }

                    endPoints.Add(neighbor);
                }
            }

            scoreSum += endPoints.Count;
        });

        return scoreSum.ToString();
    }

    public string SolveStarTwo()
    {
        int scoreSum = 0;
        Queue<List<Vector2Int>> pathQueue = new();
        List<Vector2Int> path = [];

        grid.Loop((startingHeight, position) =>
        {
            if (startingHeight > 0)
            {
                return;
            }

            pathQueue.Clear();
            path.Clear();

            path.Add(position);
            pathQueue.Enqueue(path);

            while (pathQueue.Count > 0)
            {
                List<Vector2Int> currentPath = pathQueue.Dequeue();
                Vector2Int last = currentPath[^1];

                if (grid[last.x, last.y] == MAX_HEIGHT)
                {
                    ++scoreSum;
                    continue;
                }

                Vector2Int[] neighbors = GetNeighbors(last);
                int nextValue = grid[last.x, last.y] + 1;

                for (int i = 0; i < neighbors.Length; ++i)
                {
                    Vector2Int neighbor = neighbors[i];
                    int neighborValue = grid[neighbor.x, neighbor.y];

                    if (neighborValue != nextValue) { continue; }

                    if (IsPointVisited(neighbor, currentPath)) { continue; }

                    List<Vector2Int> newPath = [.. currentPath, neighbor];
                    pathQueue.Enqueue(newPath);
                }
            }
        });

        return scoreSum.ToString();
    }

    private Vector2Int[] GetNeighbors(Vector2Int point)
    {
        List<Vector2Int> neighbors = new(4);
        Vector2Int dir = new(-1, 0);

        for (int i = 0; i < 4; ++i)
        {
            dir.RotateDegrees(-90);

            Vector2Int neighbor = point + dir;

            if (!grid.InGrid(neighbor.x, neighbor.y)) { continue; }

            neighbors.Add(neighbor);
        }

        return [.. neighbors];
    }

    private static bool IsPointVisited(Vector2Int target, List<Vector2Int> path)
    {
        for (int i = 0; i < path.Count; ++i)
        {
            if (target != path[i]) { continue; }

            return true;
        }

        return false;
    }
}