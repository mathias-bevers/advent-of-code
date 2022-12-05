using AdventOfCode.Tools;

namespace AdventOfCode._2022
{
	public class DayFive : Day
	{
		private readonly List<Command> commands = new();

		public override int DayNumber => 5;
		public Stack<char>[] crates;

		public override double Initialize()
		{
			stopwatch.Start();

			base.Initialize();

			string[] fullFile = DataRetriever.AsFile(this).Split(new[] { "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string line in fullFile[1].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
			{
				string[] commandParts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				Command command = new(int.Parse(commandParts[1]), int.Parse(commandParts[3]), int.Parse(commandParts[5]));
				commands.Add(command);
			}

			string[] crateTable = fullFile[0].Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
			int maxX = int.Parse(crateTable.Last().Replace(" ", string.Empty).Last().ToString());
			crates = new Stack<char>[maxX];

			for (int y = 0; y < crateTable.Length - 1; ++y)
			{
				string crateLayer = crateTable[y];

				for (int x = 0; x < maxX; ++x)
				{
					int charLocation = (x * 4) + 1;
					char crate = crateLayer[charLocation];

					if (crate == ' ') { continue; }

					crates[x] ??= new Stack<char>();
					crates[x].Push(crate);
				}
			}


			for (int x = 0; x < crates.Length; ++x)
			{
				Stack<char> stack = crates[x];
				Stack<char> reversed = new(stack.Count);

				while (stack.Count > 0) { reversed.Push(stack.Pop()); }

				crates[x] = reversed;
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			Stack<char>[] cratesCopy = new Stack<char>[crates.Length];
			for (int i = 0; i < crates.Length; ++i) { cratesCopy[i] = new Stack<char>(new Stack<char>(crates[i])); }


			foreach (Command command in commands)
			{
				for (int i = 0; i < command.Amount; ++i) { cratesCopy[command.To - 1].Push(cratesCopy[command.From - 1].Pop()); }
			}

			return cratesCopy.Aggregate(new string(""), (current, crateStack) => current + crateStack.Peek());
		}


		public override string StarTwo()
		{
			Stack<char>[] cratesCopy = new Stack<char>[crates.Length];
			for (int i = 0; i < crates.Length; ++i) { cratesCopy[i] = new Stack<char>(new Stack<char>(crates[i])); }

			foreach (Command command in commands)
			{
				char[] toMove = new char[command.Amount];
				for (int i = 0; i < command.Amount; ++i) { toMove[i] = cratesCopy[command.From - 1].Pop(); }

				Array.Reverse(toMove);

				foreach (char crate in toMove) { cratesCopy[command.To - 1].Push(crate); }
			}

			return cratesCopy.Aggregate(new string(""), (current, crateStack) => current + crateStack.Peek());
		}

		internal struct Command
		{
			public int Amount { get; }
			public int From { get; }
			public int To { get; }

			public Command(int amount, int from, int to)
			{
				Amount = amount;
				From = from;
				To = to;
			}
		}
	}
}