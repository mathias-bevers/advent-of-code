using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0521 : IDay
{
    public DateTime date { get; } = new(2021, 12, 05);

    private int[,] grid = new int[0, 0];
    private Line[] lines = [];

    public void PopulateData(string raw)
    {
        List<Line> lineList = [];
        string[] dataLines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        grid = dataLines.Length == 10 ? new int[10, 10] : new int[991, 990];

        foreach (string asLine in dataLines)
        {
            string[] lines = asLine.Split(" -> ");
            string[] start = lines[0].Split(',');
            string[] end = lines[1].Split(',');

            Vector2Int startPoint = new(int.Parse(start[0]), int.Parse(start[1]));
            Vector2Int endPoint = new(int.Parse(end[0]), int.Parse(end[1]));
            double differenceAngle = (endPoint - startPoint).angle * 180 / Math.PI;

            lineList.Add(new Line(new Vector2Int(startPoint.x, startPoint.y),
                    new Vector2Int(endPoint.x, endPoint.y),
                    (int)Math.Round(differenceAngle)));
        }

        lines = [.. lineList];
    }

    public string SolveStarOne()
    {
        foreach (Line line in lines)
        {
            switch (line.Dir)
            {
                case Line.Direction.Horizontal:
                    {
                        for (int x = line.Start.x; x <= line.End.x; ++x) { ++grid[x, line.Start.y]; }
                        break;
                    }
                case Line.Direction.Vertical:
                    {
                        for (int y = line.Start.y; y <= line.End.y; ++y) { ++grid[line.Start.x, y]; }
                        break;
                    }
                case Line.Direction.Diagonal: continue;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        return grid.Cast<int>().Count(i => i >= 2).ToString();
    }

    public string SolveStarTwo()
    {
        foreach (Line line in lines.Where(l => l.Dir == Line.Direction.Diagonal))
        {
            Vector2Int min = new(Math.Min(line.Start.x, line.End.x), Math.Min(line.Start.y, line.End.y));
            Vector2Int max = new(Math.Max(line.Start.x, line.End.x), Math.Max(line.Start.y, line.End.y));

            for (int y = min.y; y <= max.y; ++y)
            {
                for (int x = min.x; x <= max.x; ++x)
                {
                    Vector2Int currentPosition = new(x, y);

                    if(currentPosition == line.Start || currentPosition == line.End) 
                    {
                        ++grid[x,y];
                        continue;
                    }

                    int differenceAngle = (int)Math.Round((currentPosition - line.Start).angle
                        * 180 / Math.PI);

                    if (differenceAngle != line.DifferenceAngle) { continue; }
                    ++grid[x, y];
                }
            }
        }

        return grid.Cast<int>().Count(i => i >= 2).ToString();
    }

    private readonly struct Line
    {
        public enum Direction
        {
            Horizontal,
            Vertical,
            Diagonal
        }

        public Direction Dir { get; }
        public Vector2Int End { get; }
        public Vector2Int Start { get; }
        public int DifferenceAngle { get; }


        public Line(Vector2Int start, Vector2Int end, int differenceAngle)
        {
            DifferenceAngle = differenceAngle;
            int positiveDifferenceAngle = Math.Abs(differenceAngle);
            if (positiveDifferenceAngle == 45 || positiveDifferenceAngle == 135)
            {
                Dir = Direction.Diagonal;

                Start = start;
                End = end;
                return;
            }

            Dir = start.y == end.y ? Direction.Horizontal : Direction.Vertical;

            if (start.x <= end.x && start.y <= end.y)
            {
                Start = start;
                End = end;
            }
            else
            {
                Start = end;
                End = start;
            }
        }

        public override string ToString()
        {
            string[] parts = [$"Line from {Start,-15}", $"to {End,-15}", $"is {Dir}"];

            return string.Concat(parts);
        }
    }
}