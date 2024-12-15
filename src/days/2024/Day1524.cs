using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1524 : IDay
{
    public DateTime date { get; } = new(2024, 12, 15);

    private const char LANTERN_FISH = '@';
    private const char OBSTACLE = '#';
    private const char EMPTY = '.';
    private const char BOX = 'O';
    private static readonly Dictionary<char, Vector2Int> directionMap = new() {
        {'^', new ( 0,-1)},
        {'v', new ( 0, 1)},
        {'<', new (-1, 0)},
        {'>', new ( 1, 0)}
    };

    private char[,] grid = new char[0, 0];
    private Vector2Int[] movements = [];
    private Vector2Int fishPosition = new();

    public void PopulateData(string raw)
    {
        string[] chunks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        string[] gridRows = chunks[0].Split(Utils.NEW_LINES,
            StringSplitOptions.RemoveEmptyEntries);
        grid = new char[gridRows[0].Length, gridRows.Length];


        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                char current = gridRows[y][x];
                grid[x, y] = current;

                if (current != LANTERN_FISH) { continue; }

                fishPosition = new(x, y);
            }
        }

        List<Vector2Int> tmp = [];
        string[] movementsRaw = chunks[1].Split(Utils.NEW_LINES,
            StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < movementsRaw.Length; ++i)
        {
            for (int ii = 0; ii < movementsRaw[i].Length; ++ii)
            {
                tmp.Add(directionMap[movementsRaw[i][ii]]);
            }
        }

        movements = [.. tmp];
    }

    public string SolveStarOne()
    {
        for (int i = 0; i < movements.Length; ++i)
        {
            Vector2Int movement = movements[i];
            Vector2Int nextPos = fishPosition + movement;

            if (grid[nextPos.x, nextPos.y] == OBSTACLE) { continue; }

            Vector2Int? box = FindLastBoxInDirection(fishPosition, movement, out bool canMove);

            if (!canMove) { continue; }

            grid[fishPosition.x, fishPosition.y] = EMPTY;
            grid[nextPos.x, nextPos.y] = LANTERN_FISH;

            fishPosition = nextPos;

            if (!box.HasValue) { continue; }

            box += movement;

            grid[box.Value.x, box.Value.y] = BOX;
        }

        long gpsSum = 0;

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                if (grid[x, y] != BOX) { continue; }

                gpsSum += 100 * y + x;
            }
        }

        return gpsSum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private Vector2Int? FindLastBoxInDirection(Vector2Int start, Vector2Int direction, out bool canMove)
    {
        Vector2Int currentPos = start + direction;
        char nextChar = grid[currentPos.x, currentPos.y];

        while (nextChar == BOX)
        {
            Vector2Int nextPos = currentPos + direction;
            nextChar = grid[nextPos.x, nextPos.y];

            if (nextChar == OBSTACLE)
            {
                canMove = false;
                return null;
            }

            if (nextChar == EMPTY)
            {
                canMove = true;
                return currentPos;
            }

            currentPos = nextPos;
        }

        canMove = true;
        return null;
    }
}