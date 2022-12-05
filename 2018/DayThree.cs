using AdventOfCode.Tools;

namespace AdventOfCode._2018
{
	public class DayThree : Day
	{
		private readonly char[,] grid = new char[999, 997];
		public override int DayNumber => 3;
		private ElfClaim[] elfClaims;

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();
			string[] lines = DataRetriever.AsLines(this);
			elfClaims = new ElfClaim[lines.Length];

			for (int i = 0; i < lines.Length; i++)
			{
				string[] mainParts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);

				int id = int.Parse(mainParts[0].TrimStart('#'));

				string[] positionAsStrings = mainParts[2].TrimEnd(':').Split(',');
				Vector2Int position = new(int.Parse(positionAsStrings[0]), int.Parse(positionAsStrings[1]));

				string[] sizeAsStrings = mainParts[3].Split('x');
				Vector2Int size = new(int.Parse(sizeAsStrings[0]), int.Parse(sizeAsStrings[1]));

				elfClaims[i] = new ElfClaim(id, position, size);
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			foreach (ElfClaim elfClaim in elfClaims)
			{
				for (int x = elfClaim.Position.x; x < elfClaim.Position.x + elfClaim.Size.x; ++x)
				for (int y = elfClaim.Position.y; y < elfClaim.Position.y + elfClaim.Size.y; ++y)
				{
					switch (grid[x, y])
					{
						case 'x': break;
						case '*':
							grid[x, y] = 'x';
							break;
						default:
							grid[x, y] = '*';
							break;
					}
				}
			}

			int occupiedMultipleTimesCount = 0;
			for (int x = 0; x < grid.GetLength(0); ++x)
			{
				for (int y = 0; y < grid.GetLength(1); ++y)
				{
					if (grid[x, y] != 'x') { continue; }

					occupiedMultipleTimesCount++;
				}
			}

			return occupiedMultipleTimesCount.ToString();
		}

		public override string StarTwo()
		{
			foreach (ElfClaim elfClaim in elfClaims)
			{
				if (IsOverlapping(elfClaim)) { continue; }

				return elfClaim.ID.ToString();
			}

			return "FAILED";
		}

		private bool IsOverlapping(ElfClaim elfClaim)
		{
			for (int x = elfClaim.Position.x; x < elfClaim.Position.x + elfClaim.Size.x; ++x)
			for (int y = elfClaim.Position.y; y < elfClaim.Position.y + elfClaim.Size.y; ++y)
			{
				if (grid[x, y] == 'x') { return true; }
			}

			return false;
		}

		private struct ElfClaim
		{
			public int ID { get; }
			public Vector2Int Position { get; }
			public Vector2Int Size { get; }

			public ElfClaim(int id, Vector2Int position, Vector2Int size)
			{
				ID = id;
				Position = position;
				Size = size;
			}
		}
	}
}