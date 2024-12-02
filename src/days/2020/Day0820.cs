using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0820 : IDay
{
    public DateTime date { get; } = new(2020, 12, 08);

    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int accumulator = 0;
        List<int> vistitedOperationsIndex = new();

        for (int i = 0; i < data.Length;)
        {
            if (vistitedOperationsIndex.Contains(i)) { return accumulator.ToString(); }

            string[] operation = data[i].Split(' ');
            vistitedOperationsIndex.Add(i);

            switch (operation[0].Trim())
            {
                case "nop":
                    i++;
                    break;
                case "acc":
                    accumulator += int.Parse(operation[1].Trim());
                    i++;
                    break;
                case "jmp":
                    i += int.Parse(operation[1].Trim());
                    break;
                default:
                    Logger.Error($"{operation[0].Trim()} is not a supported value");
                    break;
            }
        }

        return "NO AWNSER FOUND";
    }

    public string SolveStarTwo()
    {
        int accumulator = 0;

        for (int i = 0; i < data.Length; i++)
        {
            string[] dataCopy = (string[])data.Clone();
            string substring = data[i].Substring(0, 3);

            if (substring == "acc") { continue; }

            dataCopy[i] = substring == "jmp" ? dataCopy[i].Replace("jmp", "nop") : dataCopy[i].Replace("nop", "jmp");

            if (rightReplacement(dataCopy))
            {
                for (int j = 0; j < dataCopy.Length;)
                {
                    string[] operation = dataCopy[j].Split(' ');

                    switch (operation[0].Trim())
                    {
                        case "nop":
                            j++;
                            break;
                        case "acc":
                            accumulator += int.Parse(operation[1].Trim());
                            j++;
                            break;
                        case "jmp":
                            j += int.Parse(operation[1].Trim());
                            break;
                        default:
                            Logger.Error($"{operation[0].Trim()} is not a supported value");
                            break;
                    }
                }

                return accumulator.ToString();
            }
        }

        bool rightReplacement(string[] testProgram)
        {
            List<int> checkedOperations = new();

            for (int i = 0; i < testProgram.Length;)
            {
                if (checkedOperations.Contains(i)) { return false; }

                string[] operation = testProgram[i].Split(' ');
                checkedOperations.Add(i);

                switch (operation[0].Trim())
                {
                    case "nop":
                        i++;
                        break;
                    case "acc":
                        i++;
                        break;
                    case "jmp":
                        i += int.Parse(operation[1].Trim());
                        break;
                    default:
                        Logger.Error($"{operation[0].Trim()} is not a supported value");
                        break;
                }
            }

            return true;
        }

        return "NO AWNSER FOUND";
    }
}