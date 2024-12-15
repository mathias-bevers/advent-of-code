using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1524 : IDay
{
    public DateTime date { get; } = new(2024, 12, 15);

    private const char LANTERN_FISH = '@';
    private const char OBSTACLE = '#';
    private const char EMPTY = '.';
    private static readonly Dictionary<char, Vector2Int> directionMap = new() {
        {'^', new ( 0, 1)},
        {'v', new ( 0,-1)},
        {'<', new (-1, 0)},
        {'>', new ( 1, 0)}
    };

    private char[,] grid = new char[0, 0];
    private Vector2Int[] movements = [];
    private Vector2Int fishLocation = new();

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
                grid[x, y] = gridRows[y][x];
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
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}