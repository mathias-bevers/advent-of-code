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

			List<CommandAndResult> commands = new();

			string[] dataStrings = DataRetriever.AsLines(this);
			for (int i = 0; i < dataStrings.Length; i++)
			{
				if (!dataStrings[i].StartsWith('$')) { continue; }

				string[] command = dataStrings[i].Split(' ');
				string commandType = command[1];
				string commandArguments = command.Length < 3 ? string.Empty : command[2];
				List<string> output = new();

				int j = i + 1;
				while (!(j >= dataStrings.Length || dataStrings[j].StartsWith('$')))
				{
					output.Add(dataStrings[j]);
					++j;
				}

				i += output.Count;
				commands.Add(new CommandAndResult(commandType, commandArguments, output));
			}

			string currentDirectory = string.Empty;
			directoryElements.Add(new DirectoryElement("/", string.Empty, null));
			foreach (CommandAndResult car in commands)
			{
				switch (car.CommandType)
				{
					case "cd":
						switch (car.CommandArguments)
						{
							case "/":
								currentDirectory = string.Empty;
								break;

							case "..":
								int lastIndexOf = currentDirectory.LastIndexOf('/');
								currentDirectory = currentDirectory[..lastIndexOf];
								break;

							default:
								currentDirectory += $"/{car.CommandArguments}";
								break;
						}

						break;

					case "ls":
						foreach (string content in car.Output)
						{
							string[] splitContent = content.Split(' ');
							string elementName = '/' + splitContent[1];
							int? fileSize = int.TryParse(splitContent[0], out int fs) ? fs : null;

							DirectoryElement directoryElement = new(elementName, currentDirectory, fileSize);
							if (directoryElements.Count(d => d.ToString() == directoryElement.ToString()) != 0) { continue; }

							directoryElements.Add(directoryElement);
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
			return directoryElements.Where(de => !de.FileSize.HasValue)
				.Where(directoryElement => directoryElement.DirectorySize <= 100000)
				.Sum(directoryElement => directoryElement.DirectorySize)
				.ToString();
		}


		public override string StarTwo()
		{
			const long totalDiskSpace = 70000000;
			const long diskSpaceNeeded = 30000000;

			long rootSize = directoryElements.Find(de => de.Name == "/").DirectorySize;
			long freeDiskSpace = totalDiskSpace - rootSize;
			long toClearDiskSpace = diskSpaceNeeded - freeDiskSpace;

			List<DirectoryElement> couldBeDeleted = new();
			foreach (DirectoryElement directoryElement in directoryElements.Where(de => !de.FileSize.HasValue))
			{
				if (directoryElement.DirectorySize < toClearDiskSpace)
				{
					continue;
				}

				couldBeDeleted.Add(directoryElement);
			}

			return couldBeDeleted.OrderBy(element => element.DirectorySize).First().DirectorySize.ToString();
		}

		private readonly struct CommandAndResult
		{
			public List<string> Output { get; }
			public string CommandArguments { get; }
			public string CommandType { get; }

			public CommandAndResult(string commandType, string commandArguments, IEnumerable<string> output)
			{
				CommandType = commandType;
				CommandArguments = commandArguments;

				Output = output.ToList();
			}

			public override string ToString()
			{
				string s = $"{CommandType} {CommandArguments} \n";

				for (int i = 0; i < Output.Count; ++i)
				{
					if (i >= Output.Count - 1)
					{
						s += $"\t{Output[i]}";
						break;
					}

					s += $"\t{Output[i]}\n";
				}

				return s;
			}
		}

		private class DirectoryElement
		{
			public int? FileSize { get; }
			public List<DirectoryElement> Contents { get; }

			public long DirectorySize
			{
				get
				{
					if (directorySize == -1)
					{
						long sum = 0;
						foreach (DirectoryElement element in Contents)
						{
							if (element.FileSize.HasValue)
							{
								sum += element.FileSize.Value;
								continue;
							}

							sum += element.DirectorySize;
						}

						directorySize = sum;
					}

					return directorySize;
				}
			}

			public string Location { get; }
			public string Name { get; }

			private long directorySize = -1;

			public DirectoryElement(string name, string location, int? fileSize)
			{
				Name = name;
				Location = location;
				FileSize = fileSize;
				Contents = new List<DirectoryElement>();
			}

			public override string ToString() => $"{Location}{Name}";
		}
	}
}