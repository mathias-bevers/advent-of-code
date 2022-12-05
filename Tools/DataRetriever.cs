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
			string path = solutionPath + $@"\Input\{day.Year}\{day.GetType().Name}.txt";

			if (!File.Exists(path)) { Debug.LogError($"There is no file for day {day.Year}-{day.DayNumber}"); }

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