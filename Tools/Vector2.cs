namespace AdventOfCode.Tools
{
	public struct Vector2
	{
		public float x { get; set; }
		public float y { get; set; }

		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2(float value) => x = y = value;

		public Vector2() => x = y = 0;
	}

	public struct Vector2Int
	{
		public int x { get; set; }
		public int y { get; set; }

		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2Int(int value) => x = y = value;

		public Vector2Int() => x = y = 0;
	}
}