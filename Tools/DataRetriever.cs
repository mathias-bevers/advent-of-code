namespace AdventOfCode.Tools
{
	public static class DataRetriever
	{
		public static string[] AsLines(Day day) => File.ReadAllLines(GeneratePath(day));

		public static string AsFile(Day day) => File.ReadAllText(GeneratePath(day));

		public static int[] AsIntArray(Day day) => AsLines(day).Select(int.Parse).ToArray();

		private static string GeneratePath(Day day)
		{
			string solutionPath = TryFindSolutionPath();
			string path = solutionPath + $@"{Path.DirectorySeparatorChar}Input{Path.DirectorySeparatorChar}{day.Year}{Path.DirectorySeparatorChar}{day.GetType().Name}.txt";

			if (File.Exists(path)) { return path; }

			Debug.LogWaring($"There is no file for day {day.Year}-{day.DayNumber}, one is now created.");
			var stream = File.Create(path);
			stream.Close();
			return path;
		}

		private static string TryFindSolutionPath(string currentPath = "")
		{
			DirectoryInfo directory = new DirectoryInfo(string.IsNullOrEmpty(currentPath) ? Directory.GetCurrentDirectory() : currentPath);

			while (directory != null)
			{
				if (directory.GetFiles("*.sln").Any())
				{
					return directory.FullName;
				}

				directory = directory.Parent;
			}

			throw new DirectoryNotFoundException("Could not find .sln");
		}
	}
}