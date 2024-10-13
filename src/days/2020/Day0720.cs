using advent_of_code.utils;

namespace advent_of_code.days;

public class Day0720 : IDay
{
    public DateTime date { get; } = new(2020, 12, 07);

    private const string DOT = ".";
    private const string NO_OTHER = "no other";
    private const string SHINY_GOLD = "shiny gold";

    private readonly string[] splitWords = ["bags", "bag", "contain", ","];
    private readonly List<Bag> bags = [];

    private string[] data = [];

    public void PopulateData(string raw)
    {
        data = raw.Split(Utils.NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in data)
        {
            string[] parts = line.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
            bags.Add(new Bag(parts[0].Trim()));
        }

        foreach (string line in data)
        {
            string[] parts = line.Split(splitWords, StringSplitOptions.RemoveEmptyEntries);
            Bag? bag = bags.Find(b => b.Color == parts[0].Trim());

            if (bag == null)
            {
                Logger.Error($"Could not find bag with name {parts[0].Trim()}");
                return;
            }

            foreach (string part in parts.Skip(2))
            {
                string trimmedPart = part.Trim();

                if (trimmedPart == NO_OTHER || trimmedPart == DOT) { continue; }

                string carriedColor = trimmedPart[2..];
                int carryingAmount = int.Parse(trimmedPart.Split(' ')[0].Trim());

                Bag carried = bags.Find(b => b.Color == carriedColor) ?? new Bag(carriedColor);
                carried.carriedBy.Add(bag);
                bag.carryingBags.Add(carried, carryingAmount);
            }
        }
    }

    public string SolveStarOne()
    {
        int canCarryGoldCount = 0;
        Queue<Bag> queue = new();
        List<Bag> checkedBags = [];

        Bag? shinyGold = bags.Find(bag => bag.Color == SHINY_GOLD);

        if (shinyGold == null)
        {
            Logger.Error($"Could not find the bag with color: {SHINY_GOLD}");
            return "ERROR";
        }

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

    public string SolveStarTwo()
    {
        int carringBags = 0;
        Queue<Bag> queue = new();
        List<Bag> checkedBags = [];

        Bag? shinyGold = bags.Find(bag => bag.Color == SHINY_GOLD);

        if (shinyGold == null)
        {
            Logger.Error($"Could not find the bag with color: {SHINY_GOLD}");
            return "ERROR";
        }

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

    private class Bag(string color)
    {
        public Dictionary<Bag, int> carryingBags { get; } = [];
        public List<Bag> carriedBy { get; } = [];
        public string Color { get; } = color;
    }
}