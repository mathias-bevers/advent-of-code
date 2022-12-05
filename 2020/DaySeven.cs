using AdventOfCode.Tools;

namespace AdventOfCode._2020
{
	public class DaySeven : Day
	{
		private const string DOT = ".";
		private const string NO_OTHER = "no other";
		private const string SHINY_GOLD = "shiny gold";

		private readonly string[] splitWords = { "bags", "bag", "contain", "," };
		public override int DayNumber => 7;
		private List<Bag> bags = new();

		private string[] data;

		public override double Initialize()
		{
			stopwatch.Start();
			base.Initialize();

			data = DataRetriever.AsLines(this);

			foreach (string line in data)
			{
				string[] parts = line.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
				bags.Add(new Bag(parts[0].Trim()));
			}

			foreach (string line in data)
			{
				string[] parts = line.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
				Bag bag = bags.Find(b => b.Color == parts[0].Trim());

				if (bag == null) { Debug.LogError($"Could not find bag with name {parts[0].Trim()}"); }

				foreach (string part in parts.Skip(2))
				{
					string trimmedPart = part.Trim();

					if (trimmedPart == NO_OTHER || trimmedPart == DOT) { continue; }

					string carriedColor = trimmedPart.Substring(2);
					int carryingAmount = int.Parse(trimmedPart.Split(' ')[0].Trim());

					Bag carried = bags.Find(b => b.Color == carriedColor);

					if (carried == null) { carried = new Bag(carriedColor); }

					carried.carriedBy.Add(bag);
					bag.carryingBags.Add(carried, carryingAmount);
				}
			}

			stopwatch.Stop();
			return stopwatch.ElapsedMilliseconds;
		}

		public override string StarOne()
		{
			int canCarryGoldCount = 0;
			Queue<Bag> queue = new();
			List<Bag> checkedBags = new();

			Bag shinyGold = bags.Find(bag => bag.Color == SHINY_GOLD);

			queue.Enqueue(shinyGold);
			checkedBags.Add(shinyGold);

			while (queue.Count > 0)
			{
				Bag bag = queue.Dequeue();

				foreach (Bag carryingBag in bag.carriedBy)
				{
					if (checkedBags.Contains(carryingBag)) { continue; }

					canCarryGoldCount++;
					checkedBags.Add(carryingBag);
					queue.Enqueue(carryingBag);
				}
			}

			return canCarryGoldCount.ToString();
		}

		public override string StarTwo()
		{
			int carringBags = 0;
			Queue<Bag> queue = new();
			List<Bag> checkedBags = new();

			Bag shinyGold = bags.Find(bag => bag.Color == SHINY_GOLD);

			queue.Enqueue(shinyGold);
			checkedBags.Add(shinyGold);

			while (queue.Count > 0)
			{
				Bag bag = queue.Dequeue();

				foreach (Bag carriedBag in bag.carryingBags.Keys)
				{
					int amountCarried = bag.carryingBags[carriedBag];

					carringBags += amountCarried;

					for (int i = 0; i < amountCarried; i++) { queue.Enqueue(carriedBag); }
				}
			}

			return carringBags.ToString();
		}

		private class Bag
		{
			public Dictionary<Bag, int> carryingBags { get; } = new();
			public List<Bag> carriedBy { get; } = new();
			public string Color { get; }

			public Bag(string color) => Color = color;
		}
	}
}