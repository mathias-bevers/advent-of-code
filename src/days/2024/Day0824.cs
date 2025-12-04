using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0824 : IDay
{
    public DateTime date { get; } = new(2024, 12, 08);

    private const char EMPTY = '.';

    private Grid<char> grid = new(0, 0);
    Dictionary<char, List<Vector2Int>> antennas = [];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        grid = new Grid<char>(rows.Length, rows[0].Length);

        for (int y = 0; y < grid.height; ++y)
        {
            for (int x = 0; x < grid.width; ++x)
            {
                char current = rows[y][x];

                grid[x, y] = current;

                if (current == EMPTY) { continue; }

                if (!antennas.TryAdd(current, [new(x, y)]))
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

                    if (!grid.InGrid(possibleLocation.x, possibleLocation.y))
                    {
                        continue;
                    }

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

                    while (grid.InGrid(current.x, current.y))
                    {
                        antinodes.Add(current);

                        current += offset;
                    }
                }
            }
        }

        return (antinodes.Count).ToString();
    }
}