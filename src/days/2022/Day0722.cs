namespace advent_of_code.days;

public class Day0722 : IDay
{
    private readonly List<DirectoryElement> directoryElements = [];

    public DateTime date { get; } = new(2022, 12, 07);

    public void PopulateData(string raw)
    {
        List<CommandAndResult> commands = [];

        string[] dataStrings = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < dataStrings.Length; i++)
        {
            if (!dataStrings[i].StartsWith('$')) { continue; }

            string[] command = dataStrings[i].Split(' ');
            string commandType = command[1];
            string commandArguments = command.Length < 3 ? string.Empty : command[2];
            List<string> output = [];

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
                        if (directoryElements.Any(d => d.ToString() == directoryElement.ToString())) { continue; }

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
    }

    public string SolveStarOne()
    {
        return directoryElements.Where(de => !de.FileSize.HasValue)
            .Where(directoryElement => directoryElement.DirectorySize <= 100000)
            .Sum(directoryElement => directoryElement.DirectorySize)
            .ToString();
    }


    public string SolveStarTwo()
    {
        const long totalDiskSpace = 70000000;
        const long diskSpaceNeeded = 30000000;

        DirectoryElement? rootDirectory = directoryElements.Find(de => de.Name == "/");

        if (rootDirectory is null)
        {
            Logger.Error("Could not find the '/' directory");
            return "null";
        }


        long rootSize = rootDirectory.DirectorySize;
        long freeDiskSpace = totalDiskSpace - rootSize;
        long toClearDiskSpace = diskSpaceNeeded - freeDiskSpace;

        List<DirectoryElement> couldBeDeleted = [];
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

    private readonly struct CommandAndResult(string commandType, string commandArguments, IEnumerable<string> output)
    {
        public List<string> Output { get; } = output.ToList();
        public string CommandArguments { get; } = commandArguments;
        public string CommandType { get; } = commandType;

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

    private class DirectoryElement(string name, string location, int? fileSize)
    {
        public int? FileSize { get; } = fileSize;
        public List<DirectoryElement> Contents { get; } = [];

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

        public string Location { get; } = location;
        public string Name { get; } = name;

        private long directorySize = -1;

        public override string ToString() => $"{Location}{Name}";
    }
}