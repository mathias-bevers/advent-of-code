using AdventOfCode.Tools;

namespace AdventOfCode._2021
{
	public class DayFive : Day
	{
		public override int DayNumber => 5;

		//private readonly int[,] grid = new int[991, 989];
		private readonly int[,] grid = new int[10, 10];
		private Line[] lines;

		public override double Initialize()
		{
			stopwatch.Start();

			List<Line> lineList = new();

			foreach (string asLine in DataRetriever.AsLines(this))
			{
				string[] lines = asLine.Split(" -> ");
				string[] start = lines[0].Split(',');
				string[] end = lines[1].Split(',');

				Vector2Int startPoint = new (int.Parse(start[0]), int.Parse(start[1]));
				Vector2Int endPoint = new(int.Parse(end[0]), int.Parse(end[1]));
				float differenceAngle = (float)((startPoint - endPoint).angle * 180 / Math.PI);
				Debug.Log(differenceAngle);

				if (startPoint.x == endPoint.x || startPoint.y == endPoint.y)
				{
					lineList.Add(new Line(new Vector2Int(startPoint.x, startPoint.y), new Vector2Int(endPoint.x, endPoint.y), false));
				}
				else if (differenceAngle == 45.0f)
				{
					lineList.Add(new Line(new Vector2Int(startPoint.x, startPoint.y), new Vector2Int(endPoint.x, endPoint.y), true));
				}
			}

			lines = lineList.ToArray();

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
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

		public override string StarTwo()
		{
			 int[,] grid = new int[10, 10];

			foreach (Line line in lines.Where(l => l.Dir == Line.Direction.Diagonal))
			{
				Debug.Log(line);

				for (int y = line.Start.y; y <= line.End.y; ++y)
				for (int x = line.Start.x; x <= line.End.x; ++x)
				{
					if (y != x) { continue; }

					++grid[x, y];
				}
			}

			for (int y = 0; y < grid.GetLength(0); ++y)
			{
				for (int x = 0; x < grid.GetLength(0); ++x)
				{
					Console.Write(grid[x, y]);
				}

				Console.WriteLine();
			}
			

			return grid.Cast<int>().Count(i => i >= 2).ToString();
		}

		private struct Line
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

			public Line(Vector2Int start, Vector2Int end, bool isDiagonal)
			{
				if (isDiagonal)
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
				string[] parts = { $"Line from {Start,-15}", $"to {End,-15}", $"is {Dir}" };

				return string.Concat(parts);
			}
		}
	}
}