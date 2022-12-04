using AdventOfCode.Tools;

namespace AdventOfCode._2022
{
	public class DayFour : Day
	{
		public override int DayNumber => 4;
		private List<(ElfAssignment, ElfAssignment)> elfPairs = new();

		public override void Initialize()
		{
			base.Initialize();

			foreach (string line in DataRetriever.AsLines(this))
			{
				string[] assignments = line.Split(',');

				string[] assignmentA = assignments[0].Split('-');
				ElfAssignment a = new(int.Parse(assignmentA[0]), int.Parse(assignmentA[1]));

				string[] assignmentB = assignments[1].Split('-');
				ElfAssignment b = new(int.Parse(assignmentB[0]), int.Parse(assignmentB[1]));

				elfPairs.Add((a, b));
			}
		}

		public override string StarOne()
		{
			int fullyContainedPairs = 0;

			foreach ((ElfAssignment, ElfAssignment) elfPair in elfPairs)
			{
				if (elfPair.Item1.IsFullyContainedBy(elfPair.Item2) ||
				    elfPair.Item2.IsFullyContainedBy(elfPair.Item1)) { ++fullyContainedPairs; }
			}

			return fullyContainedPairs.ToString();
		}

		public override string StarTwo()
		{
			int overlappingPairs = 0;

			foreach ((ElfAssignment, ElfAssignment) elfPair in elfPairs)
			{
				if (elfPair.Item1.IsOverLappingWith(elfPair.Item2))
				{
					++overlappingPairs;
				}
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
}