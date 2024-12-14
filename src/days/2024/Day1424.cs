using System.Drawing;
using advent_of_code.utils;

namespace advent_of_code.days;

internal class Day1424 : IDay
{
    public DateTime date { get; } = new(2024, 12, 14);

    private const byte SIMULATION_DURATION = 100;
    private static readonly Vector2Int exampleSize = new Vector2Int(11, 7);
    private static readonly Vector2Int puzzleSize = new Vector2Int(101, 103);

    private Robot[] robots = [];
    private Vector2Int gridSize = new Vector2Int();

    public void PopulateData(string raw)
    {
        string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        robots = new Robot[lines.Length];

        for (int i = 0; i < robots.Length; ++i)
        {
            string[] instructions = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] p = instructions[0]
                .Replace("p=", string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);
            string[] v = instructions[1]
                .Replace("v=", string.Empty).Split(',', StringSplitOptions.RemoveEmptyEntries);

            Vector2Int position = new Vector2Int(int.Parse(p[0]), int.Parse(p[1]));
            Vector2Int velocity = new Vector2Int(int.Parse(v[0]), int.Parse(v[1]));

            robots[i] = new Robot(position, velocity);
        }

        gridSize = robots.Length <= 12 ? exampleSize : puzzleSize;
    }

    public string SolveStarOne()
    {
        int[,] grid = new int[0, 0];
        grid = new int[gridSize.x, gridSize.y];

        for (int i = 0; i < robots.Length; ++i)
        {
            Robot robot = robots[i];

            for (int ii = 0; ii < SIMULATION_DURATION; ++ii)
            {
                robot.Move(gridSize);
            }

            ++grid[robot.position.x, robot.position.y];
        }

        int halfWidth = gridSize.x / 2;
        int halfHeight = gridSize.y / 2;

        int safetyFactor = 1;

        for (int i = 0; i < 4; ++i)
        {
            int xStart = i % 2 == 0 ? halfWidth + 1 : 0;
            int yStart = i < 2 ? halfHeight + 1 : 0;

            int numberOfRobots = 0;

            for (int y = 0; y < halfHeight; ++y)
            {
                for (int x = 0; x < halfWidth; ++x)
                {
                    numberOfRobots += grid[x + xStart, y + yStart];
                }
            }

            safetyFactor *= numberOfRobots;
        }

        return safetyFactor.ToString();
    }

    public string SolveStarTwo()
    {
        //TODO: see if this is required
        for (int i = 0; i < robots.Length; ++i) { robots[i].ResetPosition(); }

        System.Text.StringBuilder sb = new();
        string outputFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "aoc-mbevers", "day-14-24");
        Directory.CreateDirectory(outputFolderPath);


        int maxValue = 10_000;
        for (int i = 0; i < maxValue; ++i)
        {
            int[,] grid = new int[gridSize.x, gridSize.y];

            for (int ii = 0; ii < robots.Length; ++ii)
            {
                Robot robot = robots[ii];
                robot.Move(gridSize);
                grid[robot.position.x, robot.position.y] = 1;
            }

            sb.AppendLine("P1");
            sb.Append($"{gridSize.x} {gridSize.y}");

            for (int y = 0; y < gridSize.y; ++y)
            {
                sb.AppendLine();
                for (int x = 0; x < gridSize.x; ++x)
                {
                    sb.Append(grid[x, y]);
                }
            }

            File.WriteAllText(Path.Combine(outputFolderPath, $"iteration{i:0000}.pbm"),
                sb.ToString());
            sb.Clear();
        }

        Logger.Info("written images to: " + outputFolderPath);
        return "see output folder!";
    }

    private class Robot(Vector2Int startPosition, Vector2Int velocity)
    {
        public Vector2Int position { get; set; } = startPosition;
        public Vector2Int velocity { get; } = velocity;

        private Vector2Int startPosition = startPosition;

        public void Move(Vector2Int bounds)
        {
            Vector2Int newPos = position + velocity;

            if (newPos.x < 0) { newPos.x = bounds.x + newPos.x; }
            if (newPos.x >= bounds.x) { newPos.x -= bounds.x; }

            if (newPos.y < 0) { newPos.y = bounds.y + newPos.y; }
            if (newPos.y >= bounds.y) { newPos.y -= bounds.y; }

            position = newPos;
        }

        public void ResetPosition() => position = startPosition;

        public override string ToString() => string.Concat(position, ", ", velocity);
    }
}