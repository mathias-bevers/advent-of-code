namespace advent_of_code.days;

public class Day0221 : IDay
{
    public DateTime date {get;} = new(2021, 12, 02);

    private string[] data = [];

    public void PopulateData(string raw)
    {
		data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int depth = 0;
        int horizontalPosition = 0;

        foreach (string line in data)
        {
            string[] splitLine = line.Split(' ');
            string command = splitLine[0];
            int value = int.Parse(splitLine[1]);

            if (command == "forward")
            {
                horizontalPosition += value;
                continue;
            }

            if (command == "down")
            {
                depth += value;
                continue;
            }

            if (command == "up") { depth -= value; }
        }

        return (depth * horizontalPosition).ToString();
    }

    public string SolveStarTwo()
    {
        int depth = 0;
        int horizontalPosition = 0;
        int aim = 0;

        foreach (string line in data)
        {
            string[] splitLine = line.Split(' ');
            string command = splitLine[0];
            int value = int.Parse(splitLine[1]);

            if (command == "forward")
            {
                horizontalPosition += value;
                depth += aim * value;
                continue;
            }

            if (command == "down")
            {
                aim += value;
                continue;
            }

            if (command == "up") { aim -= value; }
        }

        return (depth * horizontalPosition).ToString();
    }
}