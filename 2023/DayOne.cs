using System.Text.RegularExpressions;
using AdventOfCode.Tools;

namespace AdventOfCode._2023
{
    public class DayOne : Day
    {
        public override int DayNumber => 1;

        private string[] data;
        private readonly Dictionary<string, int> spelledOut = new(){
            {"one", 1},
            {"two", 2},
            {"three",3 },
            {"four", 4},
            {"five", 5},
            {"six", 6},
            {"seven", 7},
            {"eight", 8},
            {"nine", 9}
        };

        public override double Initialize()
        {
            stopwatch.Start();

            data = DataRetriever.AsLines(this);

            stopwatch.Stop();
            return stopwatch.ElapsedMilliseconds;
        }

        public override string StarOne()
        {
            int sum = 0;

            foreach (string line in data)
            {
                string number = string.Empty;

                for (int i = 0; i < line.Length; ++i)
                {
                    if (!char.IsDigit(line[i])) { continue; }

                    number += line[i];
                    break;
                }

                for (int i = line.Length - 1; i >= 0; --i)
                {
                    if (!char.IsDigit(line[i])) { continue; }

                    number += line[i];
                    break;
                }

                try { sum += int.Parse(number); }
                catch (FormatException) { Debug.LogError($"Cannot parse \'{number}\' to int! Sum is unchanged."); }
            }

            return sum.ToString();
        }

        public override string StarTwo()
        {
            int sum = 0;

            foreach (string line in data) { sum += GetNumber(line); }

            return sum.ToString();
        }

        private int GetNumber(string line)
        {
            const string matchString = "[1-9]|one|two|three|four|five|six|seven|eight|nine";
            Regex startToEnd = new(matchString);
            Regex endToStart = new(matchString, RegexOptions.RightToLeft);

            string first = startToEnd.Matches(line).First().Value;
            if (first.Length > 1) { first = spelledOut[first].ToString(); }

            string last = endToStart.Matches(line).First().Value;
            if (last.Length > 1) { last = spelledOut[last].ToString(); }
            
            return int.Parse(first + last);
        }
    }
}