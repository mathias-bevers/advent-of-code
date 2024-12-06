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
    private Vector2Int position = new();
    private Vector2Int direction = new(0, 1);

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

                position.x = x;
                position.y = y;
            }
        }
    }

    public string SolveStarOne()
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        int limit = width * height * 10;
        int n = 0;

        do
        {
            ++n;

            map[position.x, position.y] = VISITED_POINT;

            Vector2Int next = position + new Vector2Int(direction.x, -direction.y);

            if (next.x < 0 || next.x >= width || next.y < 0 || next.y >= height) { break; }

            if (map[next.x, next.y] != OBSTACLE)
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
                if (map[x, y] != VISITED_POINT) { continue; }

                ++visited;
            }
        }

        return visited.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}