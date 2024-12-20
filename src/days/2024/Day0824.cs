
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0824 : IDay
{
    public DateTime date { get; } = new(2024, 12, 08);

    private const char EMPTY = '.';


    private char[,] grid = new char[0, 0];
    Dictionary<char, List<Vector2Int>> antennas = [];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new char[rows.Length, rows[0].Length];

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                char current = rows[y][x];

                grid[x, y] = current;

                if (current == EMPTY) { continue; }

                if (!antennas.TryAdd(current, new List<Vector2Int>() { new(x, y) }))
                {
                    antennas[current].Add(new Vector2Int(x, y));
                }
            }
        }
    }

    public string SolveStarOne()
    {
        HashSet<Vector2Int> antinodes = [];

        for (int i = 0; i < antennas.Count; ++i)
        {
            List<Vector2Int> collection = antennas.ElementAt(i).Value;

            for (int ii = 0; ii < collection.Count; ++ii)
            {
                for (int iii = 0; iii < collection.Count; ++iii)
                {
                    if (iii == ii) continue;

                    Vector2Int possibleLocation = (collection[iii] - collection[ii]) * -1;
                    possibleLocation += collection[ii];

                    if (!IsPointInGrid(possibleLocation)) { continue; }

                    antinodes.Add(possibleLocation);
                }
            }
        }

        return antinodes.Count.ToString();
    }

    public string SolveStarTwo()
    {
        HashSet<Vector2Int> antinodes = [];

        for (int i = 0; i < antennas.Count; ++i)
        {
            List<Vector2Int> collection = antennas.ElementAt(i).Value;

            for (int ii = 0; ii < collection.Count; ++ii)
            {
                for (int iii = 0; iii < collection.Count; ++iii)
                {
                    if (iii == ii) continue;

                    Vector2Int offset = (collection[iii] - collection[ii]) * -1;
                    Vector2Int current = collection[ii];

                    while (IsPointInGrid(current))
                    {
                        antinodes.Add(current);

                        current += offset;
                    }
                }
            }
        }

        return (antinodes.Count).ToString();
    }

    private bool IsPointInGrid(Vector2Int point)
    {
        if (point.x < 0 || point.x >= grid.GetLength(0)) { return false; }

        if (point.y < 0 || point.y >= grid.GetLength(1)) { return false; }

        return true;
    }
}