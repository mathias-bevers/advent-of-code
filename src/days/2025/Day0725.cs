using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day0725 : IDay
{
    public DateTime date { get; } = new(2025, 12, 07);

    private const char START = 'S';
    private const char SPLITTER = '^';
    private const char BEAM = '|';

    private Grid<char> manifold = new(0, 0);
    private Vector2Int enteringPoint = new(0, 0);

    public void PopulateData(string raw)
    {
        string[] rows = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        manifold = new Grid<char>(rows[0].Length, rows.Length);

        for (int y = 0; y < manifold.height; ++y)
        {
            for (int x = 0; x < manifold.width; x++)
            {
                manifold[x, y] = rows[y][x];

                if (manifold[x, y] != START)
                {
                    continue;
                }

                enteringPoint = new Vector2Int(x, y);
            }
        }
    }

    public string SolveStarOne()
    {
        int timesSplit = 0;
        Queue<Vector2Int> beams = new();
        beams.Enqueue(enteringPoint);

        while (beams.Count > 0)
        {
            Vector2Int beam = beams.Dequeue();

            for (int y = beam.y; y < manifold.height; y++)
            {
                char current = manifold[beam.x, y];

                if (current == SPLITTER)
                {
                    ++timesSplit;
                    beams.Enqueue(new Vector2Int(beam.x - 1, y));
                    beams.Enqueue(new Vector2Int(beam.x + 1, y));
                    break;
                }
                else if (current == BEAM)
                {
                    break;
                }
                else
                {
                    manifold[beam.x, y] = BEAM;
                    continue;
                }
            }
        }
        
        return timesSplit.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

}