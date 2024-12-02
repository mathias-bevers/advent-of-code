namespace advent_of_code.days;

public class DayTwo : IDay
{
    public DateTime date { get; } = new(2018, 12, 02);

    private readonly string[][] boxIDs = new string[2][];
    bool isExampleMode = false;

    public void PopulateData(string raw)
    {
        string[] chunks = raw.Split(Utils.EXAMPLE_SPLIT);

        boxIDs[0] = chunks[0].Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        if (chunks.Length <= 1) { return; }

        isExampleMode = true;
        boxIDs[1] = chunks[1].Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
    }

    public string SolveStarOne()
    {
        int doubles = 0;
        int triples = 0;

        foreach (string boxID in boxIDs[0])
        {
            Dictionary<char, int> charOccurrences = [];

            foreach (char letter in boxID)
            {
                if (charOccurrences.TryGetValue(letter, out int value))
                {
                    charOccurrences[letter] = ++value;
                    continue;
                }

                charOccurrences.Add(letter, 1);
            }


            bool containsDoubles = false;
            bool containsTriples = false;

            foreach (KeyValuePair<char, int> charOccurrence in charOccurrences)
            {
                switch (charOccurrence.Value)
                {
                    case 2:
                        containsDoubles = true;
                        break;

                    case 3:
                        containsTriples = true;
                        break;

                    default: continue;
                }
            }

            if (containsDoubles) { ++doubles; }

            if (containsTriples) { ++triples; }
        }

        return (doubles * triples).ToString();
    }

    public string SolveStarTwo()
    {
        int arrayIndex = isExampleMode ? 1 : 0;
        int threshhold = boxIDs[arrayIndex][0].Length - 1;

        for (int i = 0; i < boxIDs[arrayIndex].Length; ++i)
        {
            string boxID1 = boxIDs[arrayIndex][i];

            for (int j = 0; j < boxIDs[arrayIndex].Length; ++j)
            {
                if (i == j) { continue; }

                string boxID2 = boxIDs[arrayIndex][j];

                List<char> similarCharacters =
                    boxID1.Where((character, k) => character == boxID2[k]).ToList();

                if (similarCharacters.Count >= threshhold)
                {
                    return string.Join(null, similarCharacters);
                }
            }
        }

        return "FAILED";
    }
}