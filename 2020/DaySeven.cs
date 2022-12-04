using System.Collections.Generic;
using AdventOfCode.Tools;
using System.Linq;
using System.IO;
using System;

namespace AdventOfCode._2020
{
    public class DaySeven : Day
    {
        public override int DayNumber => 7;

        private const string SHINY_GOLD = "shiny gold";
        private const string NO_OTHER = "no other";
        private const string DOT = ".";

        private string[] data;
        private List<Bag> bags = new List<Bag>();

        public override void Initialize()
        {
            base.Initialize();

            data = DataRetriever.AsLines(this);

            foreach (string line in data)
            {
                string[] parts = line.Split(new string[] { "bags", "bag", "contain", "," }, StringSplitOptions.RemoveEmptyEntries);
                bags.Add(new Bag(parts[0].Trim()));
            }

            foreach (string line in data)
            {
                string[] parts = line.Split(new string[] { "bags", "bag", "contain", "," }, StringSplitOptions.RemoveEmptyEntries);
                Bag bag = bags.Find(b => b.Color == parts[0].Trim());

                if (bag == null) Debug.LogError($"Could not find bag with name {parts[0].Trim()}");

                foreach (string part in parts.Skip(2))
                {
                    string trimmedPart = part.Trim();

                    if (trimmedPart == NO_OTHER || trimmedPart == DOT) continue;

                    string carriedColor = trimmedPart.Substring(2);
                    int carryingAmount = int.Parse(trimmedPart.Split(' ')[0].Trim());

                    Bag carried = bags.Find(b => b.Color == carriedColor);

                    if (carried == null)
                    {
                        carried = new Bag(carriedColor);
                    }

                    carried.carriedBy.Add(bag);
                    bag.carryingBags.Add(carried, carryingAmount);
                }
            }
        }

        private class Bag
        {
            public string Color { get; }
            public List<Bag> carriedBy { get; } = new List<Bag>();
            public Dictionary<Bag, int> carryingBags { get; } = new Dictionary<Bag, int>();

            public Bag(string color)
            {
                Color = color;
            }
        }

        public override string StarOne()
        {
            int canCarryGoldCount = 0;
            Queue<Bag> queue = new Queue<Bag>();
            List<Bag> checkedBags = new List<Bag>();

            Bag shinyGold = bags.Find(bag => bag.Color == SHINY_GOLD);

            queue.Enqueue(shinyGold);
            checkedBags.Add(shinyGold);

            while (queue.Count > 0)
            {
                Bag bag = queue.Dequeue();

                foreach (Bag carryingBag in bag.carriedBy)
                {
                    if (checkedBags.Contains(carryingBag)) continue;

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
            Queue<Bag> queue = new Queue<Bag>();
            List<Bag> checkedBags = new List<Bag>();

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

                    for (int i = 0; i < amountCarried; i++)
                    {
                        queue.Enqueue(carriedBag);
                    }
                }
            }

            return carringBags.ToString();
        }
    }
}