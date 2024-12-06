using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0624 : IDay
{
    public DateTime date { get; } = new(2024, 12, 06);

    private const char START_POINT = '^';
    private const char VISITED_POINT = 'X';
    private const char OBSTACLE = '#';

    private char[,] map = new char[0, 0];
    private Vector2Int origin = new();

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        map = new char[rows.Length, rows[0].Length];

        for (int y = 0; y < rows.Length; ++y)
        {
            for (int x = 0; x < rows[y].Length; ++x)
            {

                char current = rows[y][x];
                map[x, y] = current;

                if (current != START_POINT) { continue; }

                origin.x = x;
                origin.y = y;
            }
        }
    }

    public string SolveStarOne()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        int limit = width * height * 10;
        int n = 0;

        Vector2Int position = origin;
        Vector2Int direction = new(0, 1);


        if (map.Clone() is not char[,] copy)
        {
            Logger.Error("something went wrong during copying the map");
            return "error";
        }

        do
        {
            ++n;

            copy[position.x, position.y] = VISITED_POINT;

            Vector2Int next = position + new Vector2Int(direction.x, -direction.y);

            if (!IsPointInGrid(next)) { break; }

            if (copy[next.x, next.y] != OBSTACLE)
            {
                position = next;
                continue;
            }

            direction.RotateDegrees(-90);
        }
        while (n < limit);

        int visited = 0;

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                if (copy[x, y] != VISITED_POINT) { continue; }

                ++visited;
            }
        }

        return visited.ToString();
    }

    public string SolveStarTwo()
    {
        int infinites = 0;

        for (int y = origin.y; y < map.GetLength(1); ++y)
        {
            for (int x = 0; x < map.GetLength(0); ++x)
            {
                char current = map[x, y];

                if (current == OBSTACLE) { continue; }

                map[x, y] = OBSTACLE;

                if (!IsInfinite())
                {
                    map[x, y] = current;
                    continue;
                }

                ++infinites;
                map[x, y] = 'O';
            }
        }


        return infinites.ToString();

        bool IsInfinite()
        {
            Vector2Int position = origin;
            Vector2Int direction = new(0, 1);
            HashSet<(Vector2Int, Vector2Int)> path = [(position, direction)];
            int n = 0;

            while (n < 100000)
            {
                n++;
                Vector2Int next = position + new Vector2Int(direction.x, -direction.y);

                if (!IsPointInGrid(next)) { return false; }
                else if (map[next.x, next.y] == OBSTACLE)
                {
                    direction.RotateDegrees(-90);
                }
                else
                {
                    if (!path.Add((next, direction)))
                    {
                        return true;
                    }

                    position = next;
                }
            }

            throw new Exception("fail save has activated");
        }
    }

    private bool IsPointInGrid(Vector2Int point)
    {
        if (point.x < 0 || point.y < 0) { return false; }

        if (point.x >= map.GetLength(0) || point.y >= map.GetLength(1)) { return false; }

        return true;
    }
}