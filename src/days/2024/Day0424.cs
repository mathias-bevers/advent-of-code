
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0424 : IDay
{
    public DateTime date { get; } = new(2024, 12, 04);

    private char[,] grid = new char[0, 0];
    private readonly Dictionary<int, char> searchingCharacters =
        new() { { 1, 'M' }, { 2, 'A' }, { 3, 'S' } };
    private readonly Vector2Int[] directions = {
        new Vector2Int(0,1),
        new Vector2Int(1,1),
        new Vector2Int(1,0),
        new Vector2Int(1,-1),
        new Vector2Int(0,-1),
        new Vector2Int(-1,-1),
        new Vector2Int(-1,0),
        new Vector2Int(-1,1)
    };

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = new char[rows[0].Length, rows.Length];

        for (int y = 0; y < rows.Length; ++y)
        {
            for (int x = 0; x < rows[y].Length; ++x)
            {
                grid[x, y] = rows[y][x];
            }
        }
    }

    public string SolveStarOne()
    {
        int occurance = 0;

        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                if (grid[x, y] != 'X') { continue; }
                Vector2Int origin = new(x, y);

                for (int i = 0; i < directions.Length; ++i)
                {
                    bool isValid = true;

                    for (int ii = 1; ii <= 3; ++ii)
                    {
                        Vector2Int position = origin + (directions[i] * ii);

                        if (position.x < 0 || position.x >= grid.GetLength(0))
                        {
                            isValid = false;
                            break;
                        }

                        if (position.y < 0 || position.y >= grid.GetLength(1))
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
            }
        }

        return occurance.ToString();
    }

    public string SolveStarTwo()
    {
        int occurance = 0;
        for (int y = 0; y < grid.GetLength(1); ++y)
        {
            for (int x = 0; x < grid.GetLength(0); ++x)
            {
                if (grid[x, y] != 'A') { continue; }

                Vector2Int origin = new(x, y);
                string s = string.Empty;

                for (int i = 1; i < directions.Length; i += 2)
                {
                    Vector2Int position = origin + directions[i];

                    if (position.x < 0 || position.x >= grid.GetLength(0)) { break; }

                    if (position.y < 0 || position.y >= grid.GetLength(1)) { break; }

                    char current = grid[position.x, position.y];

                    if (current != 'M' && current != 'S') { break; }

                    s += current;
                }

                if (s.Length != 4) { continue; }

                if (s[0] == s[2] || s[1] == s[3]) { continue; }

                // if (s.Count(c => c == 'M') != 2 || s.Count(c => c == 'S') != 2) { continue; }

                ++occurance;
            }
        }
        return occurance.ToString();
    }
}