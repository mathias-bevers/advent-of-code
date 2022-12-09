namespace AdventOfCode.Tools
{
	public struct Vector2
	{
		public float magnitute
		{
			get => (float)Math.Sqrt((x * x) + (y * y));
			set => this *= value;
		}

		public float x { get; set; }
		public float y { get; set; }

		public int angle
		{
			get => (int)Math.Atan2(y, x);
			set
			{
				float m = magnitute;
				x = (int)Math.Round(Math.Cos(value) * m);
				y = (int)Math.Round(Math.Sin(value) * m);
			}
		}


		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2(float value) => x = y = value;

		public Vector2() => x = y = 0;

		public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.x + b.x, a.y + b.y);
		public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.x - b.x, a.y - b.y);
		public static Vector2 operator *(Vector2 a, float scale) => new(a.x * scale, a.y * scale);
		public static Vector2 operator *(float scale, Vector2 a) => new(a.x * scale, a.y * scale);
		public static Vector2 operator /(Vector2 a, float scale) => new(a.x / scale, a.y * scale);
		public static bool operator ==(Vector2 a, Vector2 b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(Vector2 a, Vector2 b) => a.x != b.x || a.y != b.y;

		public static implicit operator Vector2Int(Vector2 v) => new((int)Math.Round(v.x), (int)Math.Round(v.y));

		public override string ToString() => $"({x}, {y})";
	}

	public struct Vector2Int
	{
		public float angle
		{
			get => (float)Math.Atan2(y, x);
			set
			{
				float m = magnitute;
				x = (int)Math.Round(Math.Cos(value) * m);
				y = (int)Math.Round(Math.Sin(value) * m);
			}
		}

		public float magnitute
		{
			get => (float)Math.Sqrt((x * x) + (y * y));
			set => this *= value;
		}

		public int x { get; set; }
		public int y { get; set; }

		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Vector2Int(int value) => x = y = value;

		public Vector2Int() => x = y = 0;


		public static Vector2Int operator +(Vector2Int a, Vector2Int b) => new(a.x + b.x, a.y + b.y);
		public static Vector2Int operator -(Vector2Int a, Vector2Int b) => new(a.x - b.x, a.y - b.y);
		public static Vector2Int operator *(Vector2Int v, float scale) => new((int)(v.x * scale), (int)(v.y * scale));
		public static Vector2Int operator *(float scale, Vector2Int v) => new((int)(v.x * scale), (int)(v.y * scale));
		public static Vector2Int operator /(Vector2Int a, float scale) => new((int)(a.x / scale), (int)(a.y / scale));
		public static bool operator ==(Vector2Int a, Vector2Int b) => a.x == b.x && a.y == b.y;
		public static bool operator !=(Vector2Int a, Vector2Int b) => a.x != b.x || a.y != b.y;

		public static implicit operator Vector2(Vector2Int v) => new(v.x, v.y);

		public override string ToString() => $"({x}, {y})";
	}
}