using AdventOfCode.Tools;

namespace AdventOfCode._2021
{
    public class DayTwo : Day
    {
        public override int DayNumber => 2;

        private string[] data = new string[1];

        public override void Initialize()
        {
            base.Initialize();
            data = DataRetriever.AsLines(this);
        }

        public override string StarOne()
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

                if (command == "up")
                {
                    depth -= value;
                    continue;
                }
            }

            return (depth * horizontalPosition).ToString();
        }

        public override string StarTwo()
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

                if (command == "up")
                {
                    aim -= value;
                    continue;
                }
            }

            return (depth * horizontalPosition).ToString();
        }
    }
}