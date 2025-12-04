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

    private Grid<char> map = new(0, 0);
    private Vector2Int origin = new();

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        map = new Grid<char>(rows.Length, rows[0].Length);

        for (int y = 0; y < map.height; ++y)
        {
            for (int x = 0; x < map.width; ++x)
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
        int limit = map.width * map.height * 10;
        int n = 0;

        Vector2Int position = origin;
        Vector2Int direction = new(0, 1);

        Grid<char> copy = map.Copy();

        do
        {
            ++n;

            copy[position.x, position.y] = VISITED_POINT;

            Vector2Int next = position + new Vector2Int(direction.x, -direction.y);

            if (!copy.InGrid(next.x, next.y)) { break; }

            if (copy[next.x, next.y] != OBSTACLE)
            {
                position = next;
                continue;
            }

            direction.RotateDegrees(-90);
        }
        while (n < limit);

        int visited = 0;

        copy.Loop((c, position) =>
        {
            if (c != VISITED_POINT) { return; }

            ++visited;
        });

        return visited.ToString();
    }

    public string SolveStarTwo()
    {
        int infinites = 0;

        map.Loop((current, position) =>
        {
            if (current == OBSTACLE) { return; }

            map[position.x, position.y] = OBSTACLE;

            if (!IsInfinite())
            {
                map[position.x, position.y] = current;
                return;
            }

            ++infinites;
            map[position.x, position.y] = 'O';
        });

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

                if (!map.InGrid(next.x, next.y))
                {
                    return false;
                }
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
}