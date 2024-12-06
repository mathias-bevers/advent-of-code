using Rule = (int before, int after);

namespace advent_of_code.days;

internal class Day0524 : IDay
{
    public DateTime date { get; } = new(2024, 12, 05);

    private Rule[] rules = [];
    private int[][] updateSequences = [];
    private int[] invalidSequenceIndexes = [];

    public void PopulateData(string raw)
    {
        string[] chunks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        string[] rawRules = chunks[0].Split(Utils.NEW_LINES,
            StringSplitOptions.RemoveEmptyEntries);
        rules = new Rule[rawRules.Length];

        for (int i = 0; i < rules.Length; ++i)
        {
            string[] rawRule = rawRules[i].Split('|');
            rules[i] = (int.Parse(rawRule[0]), int.Parse(rawRule[1]));
        }

        string[] rawSequences = chunks[1].Split(Utils.NEW_LINES,
            StringSplitOptions.RemoveEmptyEntries);
        updateSequences = new int[rawSequences.Length][];
        for (int i = 0; i < updateSequences.Length; ++i)
        {
            string[] rawSequence = rawSequences[i].Split(',',
                StringSplitOptions.RemoveEmptyEntries);
            updateSequences[i] = new int[rawSequence.Length];

            for (int ii = 0; ii < updateSequences[i].Length; ++ii)
            {
                updateSequences[i][ii] = int.Parse(rawSequence[ii]);
            }
        }
    }

    public string SolveStarOne()
    {
        int middleSum = 0;
        List<int> tmp = new(updateSequences.Length);

        for (int i = 0; i < updateSequences.Length; ++i)
        {
            bool isValid = true;

            for (int ii = 0; ii < updateSequences[i].Length; ++ii)
            {
                int current = updateSequences[i][ii];

                for (int iii = 0; iii < rules.Length; ++iii)
                {
                    if (rules[iii].before != current) { continue; }

                    int index = FindInSequence(updateSequences[i], rules[iii].after);

                    if (index < 0) { continue; }

                    if (index >= ii) { continue; }

                    isValid = false;
                    tmp.Add(i);
                    break;
                }

                if (!isValid) { break; }
            }

            if (!isValid) { continue; }

            middleSum += updateSequences[i][updateSequences[i].Length / 2];
        }

        invalidSequenceIndexes = [.. tmp];

        return middleSum.ToString();
    }

    public string SolveStarTwo()
    {
        int middleSum = 0;

        for (int i = 0; i < invalidSequenceIndexes.Length; ++i)
        {
            int[] sequence = updateSequences[invalidSequenceIndexes[i]];
            bool isValid, isChanged;

            do
            {
                isValid = true;
                isChanged = false;

                for (int ii = 0; ii < sequence.Length; ++ii)
                {
                    int current = sequence[ii];

                    for (int iii = 0; iii < rules.Length; ++iii)
                    {
                        if (rules[iii].before != current) { continue; }

                        int index = FindInSequence(sequence, rules[iii].after);

                        if (index < 0) { continue; }

                        if (index >= ii) { continue; }

                        isValid = false;

                        sequence[ii] = sequence[index];
                        sequence[index] = current;

                        isChanged = true;

                        break;
                    }
                }
            }
            while (!isValid && isChanged);

            if (!isValid) { continue; }

            middleSum += sequence[sequence.Length / 2];
        }

        return middleSum.ToString();
    }

    private static int FindInSequence(int[] squence, int toFind)
    {
        for (int i = 0; i < squence.Length; ++i)
        {
            if (squence[i] != toFind) { continue; }

            return i;
        }

        return -1;
    }
}