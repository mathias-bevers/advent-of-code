
namespace advent_of_code.days;

internal class Day0524 : IDay
{
    public DateTime date { get; } = new(2024, 12, 05);

    private (int before, int after)[] rules = [];
    private int[][] updateSequences = [];

    public void PopulateData(string raw)
    {
        string[] chunks = raw.Split(Utils.NEW_CHUNKS, StringSplitOptions.RemoveEmptyEntries);

        string[] rawRules = chunks[0].Split(Utils.NEW_LINES,
            StringSplitOptions.RemoveEmptyEntries);
        rules = new (int, int)[rawRules.Length];

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

        for (int i = 0; i < updateSequences.Length; ++i)
        {
            bool isValid = true;

            for (int ii = 0; ii < updateSequences[i].Length; ++ii)
            {
                int current = updateSequences[i][ii];
                (int before, int after)[] applyingRules =
                    Array.FindAll(rules, (r => r.before == current));

                for (int iii = 0; iii < applyingRules.Length; ++iii)
                {
                    int index =
                        Array.FindIndex(updateSequences[i], x => x == applyingRules[iii].after);

                    if (index < 0) { continue; }

                    if (index >= ii) { continue; }

                    isValid = false;
                    break;
                }

                if (!isValid) { break; }
            }

            if (!isValid) { continue; }

            middleSum += updateSequences[i][updateSequences[i].Length / 2];
        }

        return middleSum.ToString();
    }

    public string SolveStarTwo()
    {
        throw new NotImplementedException();
    }
}