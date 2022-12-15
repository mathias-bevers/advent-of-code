using AdventOfCode.Tools;

namespace AdventOfCode._2022
{
	public class DaySeven : Day
	{
		private readonly List<DirectoryElement> directoryElements = new();

		public override int DayNumber => 7;

		public override double Initialize()
		{
			stopwatch.Start();
			
			string[] dataStrings = DataRetriever.AsLines(this);
			
			string currentDirectory = string.Empty;
			directoryElements.Add(new DirectoryElement("/", string.Empty, false));

			for (int i = 0; i < dataStrings.Length; i++)
			{
				if (!dataStrings[i].StartsWith('$')) { continue; }

				string[] command = dataStrings[i].Split(' ');
				string commandType = command[1];
				string commandArgument = command.Length < 3 ? string.Empty : command[2];

				switch (commandType)
				{
					case "cd":
						switch (commandArgument)
						{
							case "/":
								currentDirectory = string.Empty;
								break;

							case "..":
								int lastIndexOf = currentDirectory.LastIndexOf('/');
								currentDirectory = currentDirectory[..lastIndexOf];
								break;

							default:
								currentDirectory += $"/{commandArgument}";
								break;
						}

						break;

					case "ls":
						int j = i + 1;
						while (!(j >= dataStrings.Length || dataStrings[j].StartsWith('$')))
						{
							string[] splitContent = dataStrings[j].Split(' ');
							string elementName = '/' + splitContent[1];

							DirectoryElement directoryElement = int.TryParse(splitContent[0], out int fs)
								? new DirectoryElement(elementName, currentDirectory, true, fs)
								: new DirectoryElement(elementName, currentDirectory, false);

							if (directoryElements.Count(d => d.ToString() == directoryElement.ToString()) != 0) { continue; }

							directoryElements.Add(directoryElement);

							++j;
						}

						break;
				}
			}

			foreach (DirectoryElement directoryElement in directoryElements)
			{
				directoryElement.Contents.AddRange(directoryElement.Name == "/"
					? directoryElements.Where(de => string.IsNullOrEmpty(de.Location) && de.Name != directoryElement.Name)
					: directoryElements.Where(de => de.Location == directoryElement.ToString()));
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			return directoryElements.Where(de => !de.IsFile)
				.Where(directoryElement => directoryElement.Size <= 100000)
				.Sum(directoryElement => directoryElement.Size)
				.ToString();
		}


		public override string StarTwo()
		{
			const int totalDiskSpace = 70000000;
			const int diskSpaceNeeded = 30000000;

			DirectoryElement? root = directoryElements.Find(de => de.Name == "/");

			if (root == null)
			{
				Debug.LogError("Could not find directory root");
				return "FAILED";
			}

			int rootSize = root.Size;
			int freeDiskSpace = totalDiskSpace - rootSize;
			int toClearDiskSpace = diskSpaceNeeded - freeDiskSpace;

			List<DirectoryElement> couldBeDeleted = new();
			foreach (DirectoryElement directoryElement in directoryElements.Where(de => !de.IsFile))
			{
				if (directoryElement.Size < toClearDiskSpace) { continue; }

				couldBeDeleted.Add(directoryElement);
			}

			return couldBeDeleted.OrderBy(element => element.Size).First().Size.ToString();
		}

		private class DirectoryElement
		{
			private int size = -1;


			public bool IsFile { get; }
			public List<DirectoryElement> Contents { get; }

			public int Size
			{
				get
				{
					if (IsFile)
					{
						return size;
					}

					if (size != -1)
					{
						return size;
					}

					size = Contents.Sum(directoryElement => directoryElement.Size);
					return size;
				}
			}
			public string Location { get; }
			public string Name { get; }

			public DirectoryElement(string name, string location, bool isFile, int size = -1)
			{
				Name = name;
				Location = location;
				IsFile = isFile;

				if (isFile) { this.size = size; }

				Contents = new List<DirectoryElement>();
			}

			public override string ToString() => $"{Location}{Name}";
		}
	}
}