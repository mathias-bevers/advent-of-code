using System.Diagnostics;
using System.Security.Cryptography;
using advent_of_code.utils;
using Microsoft.VisualBasic;

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

                    beams.Enqueue(new(beam.x - 1, y));
                    beams.Enqueue(new(beam.x + 1, y));
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
        Dictionary<Vector2Int, long> preCalculated = [];
        long timeLines = GetTimeLines(enteringPoint, ref preCalculated);
        return timeLines.ToString();
    }

    public long GetTimeLines(Vector2Int start, ref Dictionary<Vector2Int, long> preCalculated)
    {
        if (preCalculated.TryGetValue(start, out long value))
        {
            return value;
        }

        for (int y = start.y; y < manifold.height; y++)
        {
            Vector2Int current = new(start.x, y);

            if (manifold[current.x, current.y] != SPLITTER)
            {
                continue;
            }

            long count = GetTimeLines(new Vector2Int(start.x - 1, y), ref preCalculated) +
                         GetTimeLines(new Vector2Int(start.x + 1, y), ref preCalculated);
            preCalculated[start] = count;
            return count;
        }

        return 1;
    }
}