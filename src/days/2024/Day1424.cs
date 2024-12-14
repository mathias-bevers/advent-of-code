using System.Numerics;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1424 : IDay
{
    public DateTime date { get; } = new(2024, 12, 14);

    private Robot[] robots = [];

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        robots = new Robot[lines.Length];

        for(int i = 0; i < robots.Length; ++i)
        {
            string[] instructions = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] p = instructions[0]
                .Replace("p=",string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            string[] v = instructions[1]
                .Replace("v=",string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            
            Vector2Int position = new Vector2Int(int.Parse(p[0]), int.Parse(p[1]));
            Vector2Int velocity = new Vector2Int(int.Parse(v[0]), int.Parse(v[1]));
            
            robots[i] = new Robot(position, velocity);
        }
    }

    public string SolveStarOne()
    {
        throw new NotImplementedException();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }

    private class Robot(Vector2Int startPosition, Vector2Int velocity)
    {
        public Vector2Int postion { get; set; } = startPosition;
        public Vector2Int velocity { get; } = velocity;

        public override string ToString() => string.Concat(postion, ", ", velocity);
    }
}