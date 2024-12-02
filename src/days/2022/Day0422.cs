namespace advent_of_code;

public class Day0422 : IDay
{
    public DateTime date { get; } = new(2022, 12, 04);
    
    private List<(ElfAssignment, ElfAssignment)> elfPairs = [];

    public void PopulateData(string raw)
    {
		string[] lines = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);
        for(int i = 0; i < lines.Length; ++i)
        {
            string[] assignments = lines[i].Split(',');

            string[] assignmentA = assignments[0].Split('-');
            ElfAssignment a = new(int.Parse(assignmentA[0]), int.Parse(assignmentA[1]));

            string[] assignmentB = assignments[1].Split('-');
            ElfAssignment b = new(int.Parse(assignmentB[0]), int.Parse(assignmentB[1]));

            elfPairs.Add((a, b));
        }
    }

    public string SolveStarOne()
    {
        int fullyContainedPairs = 0;

        foreach ((ElfAssignment, ElfAssignment) elfPair in elfPairs)
        {
            if (elfPair.Item1.IsFullyContainedBy(elfPair.Item2) || elfPair.Item2.IsFullyContainedBy(elfPair.Item1))
            {
                ++fullyContainedPairs;
            }
        }

        return fullyContainedPairs.ToString();
    }

    public string SolveStarTwo()
    {
        int overlappingPairs = 0;

        foreach ((ElfAssignment, ElfAssignment) elfPair in elfPairs)
        {
            if (elfPair.Item1.IsOverLappingWith(elfPair.Item2)) { ++overlappingPairs; }
        }

        return overlappingPairs.ToString();
    }

    private struct ElfAssignment
    {
        public int Begin { get; }
        public int End { get; }

        public ElfAssignment(int begin, int end)
        {
            Begin = begin;
            End = end;
        }

        public bool IsFullyContainedBy(ElfAssignment elfAssignment) => elfAssignment.Begin >= Begin && elfAssignment.End <= End;

        public bool IsOverLappingWith(ElfAssignment elfAssignment) => Begin <= elfAssignment.End && elfAssignment.Begin <= End;
    }
}