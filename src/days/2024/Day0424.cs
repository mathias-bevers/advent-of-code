
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0424 : IDay
{
    public DateTime date { get; } = new(2024, 12, 04);

    private Grid<char> grid = new(0, 0);
    private readonly Dictionary<int, char> searchingCharacters =
        new() { { 1, 'M' }, { 2, 'A' }, { 3, 'S' } };
    private readonly Vector2Int[] directions = [
        new ( 0, 1),
        new ( 1, 1),
        new ( 1, 0),
        new ( 1,-1),
        new ( 0,-1),
        new (-1,-1),
        new (-1, 0),
        new (-1, 1)
    ];

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new Grid<char>(rows[0].Length, rows.Length);

        for (int y = 0; y < grid.height; ++y)
        {
            for (int x = 0; x < grid.width; ++x)
            {
                grid[x, y] = rows[y][x];
            }
        }
    }

    public string SolveStarOne()
    {
        int occurance = 0;

        grid.Loop((letter, origin) =>
        {
            if (letter != 'X') { return; }

            for (int i = 0; i < directions.Length; ++i)
            {
                bool isValid = true;

                for (int ii = 1; ii <= 3; ++ii)
                {
                    Vector2Int position = origin + (directions[i] * ii);

                    if (!grid.InGrid(position.x, position.y))
                    {
                        isValid = false;
                        break;
                    }

                    if (grid[position.x, position.y] != searchingCharacters[ii])
                    {
                        isValid = false;
                        break;
                    }
                }

                if (!isValid) { continue; }
                ++occurance;
            }
        });

        return occurance.ToString();
    }

    public string SolveStarTwo()
    {
        int occurance = 0;

        grid.Loop((letter, origin) =>
        {
            if (letter != 'A') { return; }

            string s = string.Empty;

            for (int i = 1; i < directions.Length; i += 2)
            {
                Vector2Int position = origin + directions[i];

                if (!grid.InGrid(position.x, position.y))
                {
                    return;
                }

                char current = grid[position.x, position.y];

                if (current != 'M' && current != 'S') { break; }

                s += current;
            }

            if (s.Length != 4) { return; }

            if (s[0] == s[2] || s[1] == s[3]) { return; }
            
            ++occurance;
        });

        return occurance.ToString();
    }
}